from fastapi import FastAPI, Depends
from fastapi.middleware.cors import CORSMiddleware
from sqlalchemy.orm import Session
from datetime import datetime, timedelta
from typing import List, Dict, Any
from .database import Base, engine, SessionLocal
from . import models, schemas
from .ingestor import start_mqtt_daemon, publish_actuator, get_latest_cache

app = FastAPI(title="IoT Motos Backend", version="0.1")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

Base.metadata.create_all(bind=engine)
start_mqtt_daemon()

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@app.get("/api/health")
def health():
    return {"ok": True}

@app.get("/api/latest")
def latest():
    return get_latest_cache()

@app.get("/api/history")
def history(moto_id: str, minutes: int = 60, db: Session = Depends(get_db)):
    since = datetime.utcnow() - timedelta(minutes=minutes)
    q = db.query(models.Telemetry).filter(
        models.Telemetry.moto_id == moto_id,
        models.Telemetry.ts >= since
    ).order_by(models.Telemetry.ts.asc()).all()
    return [
        {
            "ts": t.ts.isoformat(),
            "gps_lat": t.gps_lat,
            "gps_lon": t.gps_lon,
            "battery": t.battery,
            "tilt": t.tilt,
            "speed": t.speed,
        } for t in q
    ]

@app.post("/api/actuate")
def actuate(payload: Dict[str, Any]):
    moto_id = payload.get("moto_id")
    command = payload.get("command")
    if not moto_id or not command:
        return {"ok": False, "error": "moto_id and command required"}
    publish_actuator(moto_id, command)
    return {"ok": True}
