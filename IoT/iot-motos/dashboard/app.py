import dash
from dash import Dash, dcc, html, Input, Output, State, dash_table
import dash_bootstrap_components as dbc
import requests
import pandas as pd
from datetime import datetime

BACKEND = "http://127.0.0.1:8000"

app = Dash(__name__, external_stylesheets=[dbc.themes.BOOTSTRAP])
app.title = "Motos IoT - Dashboard"

def card_stat(title, value, subtitle=None, id=None):
    return dbc.Card(
        dbc.CardBody([
            html.H6(title, className="text-muted"),
            html.H2(value, id=id),
            html.Div(subtitle, className="text-secondary small")
        ]), className="shadow-sm h-100"
    )

app.layout = dbc.Container([
    html.H1("Monitoramento de Motos (IoT)", className="my-3"),
    dbc.Row([
        dbc.Col(dbc.Alert("Status em tempo real das motos. Use os botões para acionar atuadores.", color="info"), md=12)
    ]),
    dbc.Row([
        dbc.Col(card_stat("Motos ativas", "0", id="motos-ativas"), md=3),
        dbc.Col(card_stat("Última atualização", "-", id="last-update"), md=3),
        dbc.Col(card_stat("Alertas (tilt > 30°)", "0", id="alert-count"), md=3),
        dbc.Col(card_stat("Baterias < 20%", "0", id="lowbat-count"), md=3),
    ], className="mb-3"),
    dbc.Row(id="motos-cards", className="g-3"),
    html.Hr(),
    html.H5("Histórico (últimos 60 min)"),
    dbc.Row([
        dbc.Col(dcc.Dropdown(id="dd-moto", placeholder="Escolha a moto", clearable=False), md=3),
    ], className="mb-2"),
    dbc.Row([
        dbc.Col(dcc.Graph(id="g-battery"), md=6),
        dbc.Col(dcc.Graph(id="g-tilt"), md=6),
    ]),
    dcc.Interval(id="tick", interval=2000, n_intervals=0)
], fluid=True)

def build_moto_card(m):
    moto_id = m["moto_id"]
    loc = f"{m['gps_lat']:.5f}, {m['gps_lon']:.5f}"
    tilt_alert = m['tilt'] > 30
    battery_low = m['battery'] < 20
    badge = []
    if tilt_alert: badge.append(dbc.Badge("TILT", color="danger", className="ms-1"))
    if battery_low: badge.append(dbc.Badge("LOW BAT", color="warning", className="ms-1"))
    return dbc.Col(
        dbc.Card(dbc.CardBody([
            html.H5(["Moto ", moto_id, " ", *badge]),
            html.Div([html.Strong("Local:"), " ", loc]),
            html.Div([html.Strong("Bateria:"), f" {m['battery']:.1f}%"]),
            html.Div([html.Strong("Inclinação:"), f" {m['tilt']:.1f}°"]),
            html.Div([html.Strong("Velocidade:"), f" {m.get('speed') or 0:.1f} km/h"]),
            dbc.ButtonGroup([
                dbc.Button("Travar", id={"type":"act","moto":moto_id,"cmd":"lock"}, color="secondary", size="sm"),
                dbc.Button("Buzina", id={"type":"act","moto":moto_id,"cmd":"buzzer_on"}, color="primary", size="sm", className="ms-2"),
            ], className="mt-2")
        ])), md=4
    )

@app.callback(
    Output("motos-cards", "children"),
    Output("motos-ativas", "children"),
    Output("last-update", "children"),
    Output("alert-count", "children"),
    Output("lowbat-count", "children"),
    Output("dd-moto", "options"),
    Input("tick", "n_intervals")
)
def refresh(_):
    try:
        latest = requests.get(f"{BACKEND}/api/latest", timeout=1.5).json()
    except Exception:
        latest = {}
    items = list(latest.values())
    cards = [build_moto_card(m) for m in items]
    now = datetime.now().strftime("%H:%M:%S")
    alerts = sum(1 for m in items if m["tilt"] > 30)
    lowbat = sum(1 for m in items if m["battery"] < 20)
    options = [{"label": m["moto_id"], "value": m["moto_id"]} for m in items]
    return cards, str(len(items)), now, str(alerts), str(lowbat), options

@app.callback(
    Output("g-battery", "figure"),
    Output("g-tilt", "figure"),
    Input("dd-moto", "value"),
    Input("tick", "n_intervals")
)
def update_graphs(moto_id, _):
    import plotly.express as px
    if not moto_id:
        return {}, {}
    try:
        data = requests.get(f"{BACKEND}/api/history", params={"moto_id": moto_id, "minutes": 60}, timeout=2.5).json()
        df = pd.DataFrame(data)
        if not df.empty:
            df["ts"] = pd.to_datetime(df["ts"])
    except Exception:
        df = pd.DataFrame()
    if df.empty:
        return {}, {}
    fig_bat = px.line(df, x="ts", y="battery", markers=True, title=f"Bateria - Moto {moto_id}")
    fig_tilt = px.line(df, x="ts", y="tilt", markers=True, title=f"Inclinação - Moto {moto_id}")
    return fig_bat, fig_tilt

@app.callback(
    Output({"type":"act","moto":dash.ALL,"cmd":dash.ALL}, "children"),
    Input({"type":"act","moto":dash.ALL,"cmd":dash.ALL}, "n_clicks"),
    State({"type":"act","moto":dash.ALL,"cmd":dash.ALL}, "id"),
    prevent_initial_call=True
)
def actuate(_clicks, ids):
    if not ids:
        return dash.no_update
    for idobj in ids:
        moto = idobj["moto"]; cmd = idobj["cmd"]
        try:
            requests.post(f"{BACKEND}/api/actuate", json={"moto_id": moto, "command": cmd}, timeout=1.5)
        except Exception:
            pass
    return [html.Span("OK", className="ms-2")] * len(ids)

if __name__ == "__main__":
    app.run_server(debug=True, port=8050)
