from sqlalchemy import Column, Integer, Float, String, DateTime, JSON
from sqlalchemy.sql import func
from .database import Base

class Telemetry(Base):
    __tablename__ = "telemetry"
    id = Column(Integer, primary_key=True, index=True)
    moto_id = Column(String, index=True)
    gps_lat = Column(Float)
    gps_lon = Column(Float)
    battery = Column(Float)  # percent
    tilt = Column(Float)     # degrees
    speed = Column(Float)    # km/h (optional)
    raw = Column(JSON)
    ts = Column(DateTime(timezone=True), server_default=func.now(), index=True)
