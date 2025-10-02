import random
import datetime
import pandas as pd
from dash import Dash, dcc, html
from dash.dependencies import Input, Output
import plotly.express as px

# Criando app Dash
app = Dash(__name__)

# Dados simulados
historico = pd.DataFrame(columns=["tempo", "sensor1", "sensor2", "sensor3"])

# Layout do Dashboard
app.layout = html.Div([
    html.H1("📊 Dashboard IoT - Monitoramento de Motos", style={"textAlign": "center"}),

    dcc.Interval(
        id="intervalo",
        interval=2000,  # atualização a cada 2 segundos
        n_intervals=0
    ),

    html.Div([
        html.Div([
            html.H3("Sensor 1 - Temperatura"),
            dcc.Graph(id="grafico1")
        ], style={"width": "33%", "display": "inline-block"}),

        html.Div([
            html.H3("Sensor 2 - Bateria"),
            dcc.Graph(id="grafico2")
        ], style={"width": "33%", "display": "inline-block"}),

        html.Div([
            html.H3("Sensor 3 - Localização (X,Y)"),
            dcc.Graph(id="grafico3")
        ], style={"width": "33%", "display": "inline-block"}),
    ])
])

# Atualização em tempo real
@app.callback(
    [Output("grafico1", "figure"),
     Output("grafico2", "figure"),
     Output("grafico3", "figure")],
    [Input("intervalo", "n_intervals")]
)
def atualizar(n):
    global historico
    tempo = datetime.datetime.now()

    # Simulação de dados dos sensores
    s1 = random.randint(20, 40)   # temperatura
    s2 = random.randint(10, 100)  # nível bateria
    s3x = random.uniform(0, 10)   # posição X
    s3y = random.uniform(0, 10)   # posição Y

    novo = pd.DataFrame([[tempo, s1, s2, (s3x, s3y)]],
                        columns=["tempo", "sensor1", "sensor2", "sensor3"])
    historico = pd.concat([historico, novo]).tail(50)  # mantém últimas 50 leituras

    # Gráficos
    fig1 = px.line(historico, x="tempo", y="sensor1", title="Temperatura das Motos (°C)")
    fig2 = px.line(historico, x="tempo", y="sensor2", title="Bateria (%)")
    fig3 = px.scatter(x=[p[0] for p in historico["sensor3"]],
                      y=[p[1] for p in historico["sensor3"]],
                      title="Mapa do Pátio - Posição das Motos")

    return fig1, fig2, fig3


if __name__ == "__main__":
    app.run(debug=True)
