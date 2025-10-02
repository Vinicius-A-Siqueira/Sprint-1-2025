from pydantic import BaseModel
from typing import Optional, Any
from datetime import datetime

class TelemetryIn(BaseModel):
    moto_id: str
    gps_lat: float
    gps_lon: float
    battery: float
    tilt: float
    speed: float | None = None
    ts: datetime | None = None

class TelemetryOut(TelemetryIn):
    id: int
    ts: datetime
