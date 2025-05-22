# Componentes para Simulação IoT no Wokwi

Para a simulação do sistema de mapeamento inteligente do pátio e gestão das motos da Mottu, vamos utilizar os seguintes componentes no Wokwi:

## Hardware
1. **ESP32** - Microcontrolador principal que coordenará todo o sistema
2. **Módulo GPS (NEO-6M simulado)** - Para simular a localização das motos no pátio
3. **Display OLED** - Para visualização de informações como coordenadas e status
4. **LEDs RGB** - Para indicação visual do status das motos (disponível, em manutenção, etc.)
5. **Buzzer** - Para localização sonora das motos no pátio
6. **Botões** - Para simular interações como "procurar moto específica"
7. **Potenciómetros** - Para simular movimentação das motos no pátio

## Comunicação
- Protocolo MQTT para envio de dados de localização
- Simulação de comunicação com servidor backend

## Funcionalidades a implementar
1. **Localização das motos** - Simular coordenadas GPS dentro do pátio
2. **Alerta sonoro** - Ativar buzzer para localizar uma moto específica
3. **Visualização de status** - Mostrar no display OLED e LEDs
4. **Dashboard** - Simular envio de dados para dashboard de monitoramento
5. **Gestão de eventos** - Registrar movimentações e alterações de status
