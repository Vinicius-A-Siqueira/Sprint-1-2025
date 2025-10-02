import json
import threading
import time
import paho.mqtt.client as mqtt
from sqlalchemy.orm import Session
from .database import SessionLocal
from . import models

BROKER_HOST = "localhost"
BROKER_PORT = 1883
TOPIC_TELEMETRY = "motos/+/telemetry"
TOPIC_ACTUATOR = "motos/{moto_id}/actuator"

_latest_cache = {}  # in-memory latest by moto

def on_connect(client, userdata, flags, reason_code, properties=None):
    print("MQTT connected:", reason_code)
    client.subscribe(TOPIC_TELEMETRY)

def on_message(client, userdata, msg):
    try:
        payload = json.loads(msg.payload.decode())
        moto_id = payload.get("moto_id")
        db: Session = SessionLocal()
        t = models.Telemetry(
            moto_id=moto_id,
            gps_lat=payload.get("gps_lat"),
            gps_lon=payload.get("gps_lon"),
            battery=payload.get("battery"),
            tilt=payload.get("tilt"),
            speed=payload.get("speed"),
            raw=payload,
        )
        db.add(t)
        db.commit()
        db.refresh(t)
        _latest_cache[moto_id] = {
            "id": t.id,
            "moto_id": t.moto_id,
            "gps_lat": t.gps_lat,
            "gps_lon": t.gps_lon,
            "battery": t.battery,
            "tilt": t.tilt,
            "speed": t.speed,
            "ts": t.ts.isoformat(),
        }
        db.close()
    except Exception as e:
        print("Error processing message:", e)

def start_mqtt_daemon():
    def _run():
        client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
        client.on_connect = on_connect
        client.on_message = on_message
        while True:
            try:
                client.connect(BROKER_HOST, BROKER_PORT, keepalive=60)
                client.loop_forever()
            except Exception as e:
                print("MQTT reconnect in 3s due to:", e)
                time.sleep(3)
    th = threading.Thread(target=_run, daemon=True)
    th.start()

def publish_actuator(moto_id: str, command: str):
    client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2)
    client.connect(BROKER_HOST, BROKER_PORT, 60)
    topic = TOPIC_ACTUATOR.format(moto_id=moto_id)
    client.publish(topic, json.dumps({"command": command, "at": time.time()}))
    client.disconnect()

def get_latest_cache():
    return _latest_cache
