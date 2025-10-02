# Sprint-1-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879  
- **Kauan Felipe** – RM557954  
- **Vinicius Alves** – RM551939  

---

# IoT Motos – MQTT + FastAPI + Dash

Projeto de demonstração **funcional** para o desafio IoT/Visão, cobrindo:
- Comunicação em tempo real (MQTT) entre *dispositivos* e backend.
- Coleta, tratamento e **persistência** (SQLite via SQLAlchemy).
- **Dashboard** em tempo real (Dash) com métricas e controle de atuadores.
- Cenários de teste: *moto desaparecida*, *local errado*, *inclinação anormal*, *bateria baixa*.
- (Opcional) Roteiro para **Visão Computacional**.

## Arquitetura

```
[Simuladores paho-mqtt]  -->  [Broker Mosquitto]  -->  [FastAPI ingestor + DB SQLite]  -->  [Dash Dashboard]
                                   ^    |                                              ^             |
                                   |    +-- comandos atuadores (MQTT) -----------------+-------------+
```

## Como executar (local)

1) **Broker MQTT**
```bash
cd iot-motos
docker compose up -d
```
(ou instale Mosquitto localmente e rode na porta 1883)

2) **Backend (FastAPI)**
```bash
cd backend
uvicorn main:app --reload --port 8000
```

3) **Dashboard (Dash)**
```bash
cd ../dashboard
python app.py
```
Abra http://127.0.0.1:8050

4) **Simuladores** (3 dispositivos em paralelo)
```bash
cd ../simulators
python sim_moto.py --moto M1 --lat -23.55 --lon -46.63 --battery 85
python sim_moto.py --moto M2 --lat -23.56 --lon -46.62 --battery 60
python sim_moto.py --moto M3 --lat -23.54 --lon -46.64 --battery 35
```

Você verá as 3 motos no dashboard. Use os botões **Travar** e **Buzina** para publicar comandos MQTT (atuadores).

## Endpoints úteis
- `GET /api/health` – status do backend
- `GET /api/latest` – último registro por moto (para dashboards)
- `GET /api/history?moto_id=M1&minutes=60` – série temporal para gráficos
- `POST /api/actuate` `{ "moto_id": "M1", "command": "lock" }` – publica comando via MQTT

## Estrutura de dados (SQLite)
Tabela `telemetry` com colunas: `moto_id, gps_lat, gps_lon, battery, tilt, speed, ts` + `raw` (JSON).

## Casos de teste (para vídeo)
- **Inclinação anormal**: aguarde evento (tilt > 30°) no simulador → dashboard marca *TILT*.
- **Bateria baixa**: inicialize M3 com 35% → dashboard marca *LOW BAT* quando < 20%.
- **Local errado**: compare `gps_lat/lon` com área esperada (pode validar no backend e acionar alerta visual).
- **Moto desaparecida**: mate um simulador → contador de motos ativas diminui (e timestamp congela).

## Organização e Documentação
- Código separado em `backend/`, `dashboard/`, `simulators/`.
- Comentários claros e requisitos em `requirements.txt`.
- Adicione ao README: **vídeo (YouTube não listado)** e **link do repositório GitHub** na entrega.