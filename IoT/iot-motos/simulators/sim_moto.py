import json, random, time, math, argparse
import paho.mqtt.client as mqtt

def simulate(moto_id: str, lat: float, lon: float, battery_start: float):
    client = mqtt.Client(mqtt.CallbackAPIVersion.VERSION2, client_id=f"sim-{moto_id}")
    client.connect("localhost", 1883, 60)
    angle = 0.0
    battery = battery_start
    speed = 30.0
    while True:
        # Lazy random walk
        angle += random.uniform(-0.1, 0.1)
        lat += 0.0002 * math.cos(angle)
        lon += 0.0002 * math.sin(angle)
        tilt = abs(random.gauss(10, 8))
        if random.random() < 0.02:
            tilt = random.uniform(35, 70)  # occasional sharp tilt (alert)
        battery = max(0.0, battery - random.uniform(0.02, 0.2))
        speed = max(0.0, speed + random.uniform(-1, 1))
        payload = {
            "moto_id": moto_id,
            "gps_lat": lat,
            "gps_lon": lon,
            "battery": battery,
            "tilt": tilt,
            "speed": speed,
        }
        topic = f"motos/{moto_id}/telemetry"
        client.publish(topic, json.dumps(payload), qos=0, retain=False)
        time.sleep(1.0)

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--moto", required=True)
    parser.add_argument("--lat", type=float, default=-23.5505)
    parser.add_argument("--lon", type=float, default=-46.6333)
    parser.add_argument("--battery", type=float, default=100.0)
    args = parser.parse_args()
    simulate(args.moto, args.lat, args.lon, args.battery)
