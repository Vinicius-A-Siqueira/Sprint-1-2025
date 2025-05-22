# Documentação da Simulação IoT para Projeto Mottu

## Visão Geral
Esta simulação representa um sistema de mapeamento inteligente do pátio e gestão das motos da Mottu, implementado no Wokwi. O sistema permite localizar motos no pátio, visualizar seu status e posição, e ativar um alerta sonoro para facilitar a localização física.

## Componentes Utilizados
- **ESP32**: Microcontrolador principal
- **Display OLED**: Visualização de informações e mapa do pátio
- **LEDs RGB**: Indicação visual do status das motos
- **Buzzer**: Alerta sonoro para localização
- **Botões**: Interação com o sistema
- **Potenciómetros**: Simulação de movimento das motos no pátio

## Funcionalidades Implementadas

### 1. Localização de Motos
- Cada moto possui coordenadas X e Y dentro do pátio
- Visualização da posição no mapa simplificado no display OLED
- Simulação de movimento através dos potenciómetros

### 2. Gestão de Status
- Cada moto possui um status: Disponível, Em uso, Manutenção ou Reservada
- Indicação visual através de LEDs coloridos:
  - Verde: Disponível
  - Azul: Em uso
  - Vermelho: Manutenção
  - Amarelo piscante: Reservada

### 3. Sistema de Localização
- Botão para ativar buzzer da moto selecionada
- Padrão sonoro para facilitar localização física no pátio

### 4. Comunicação de Dados
- Simulação de envio de dados via MQTT
- Formato JSON com informações de ID, posição e status

## Como Utilizar a Simulação
1. Pressione o botão verde para alternar entre as motos disponíveis
2. Use os potenciómetros para simular o movimento da moto selecionada
3. Pressione o botão vermelho para ativar o buzzer e localizar a moto
4. Observe o display OLED para informações detalhadas e mapa
5. Verifique os LEDs para identificação rápida do status

## Integração com Backend
A simulação envia dados no formato JSON via Serial (simulando MQTT), que podem ser capturados por um backend para:
- Atualização de dashboard em tempo real
- Registro de histórico de movimentação
- Análise de utilização do pátio
- Alertas de manutenção

## Expansão Futura
Esta simulação pode ser expandida para incluir:
- Múltiplos ESP32 representando diferentes áreas do pátio
- Sensores reais de GPS para localização precisa
- Integração com sistema de câmeras e visão computacional
- Aplicativo mobile para gestores e operadores de pátio
