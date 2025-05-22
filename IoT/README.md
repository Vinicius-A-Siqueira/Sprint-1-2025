# Sprint-1-2025

![image](https://github.com/user-attachments/assets/6335eded-1ce5-41f1-8fbd-7921804f3f67)

## 👥 Integrantes

- **Gabriel Camargo** – RM557879  
- **Kauan Felipe** – RM557954  
- **Vinicius Alves** – RM551939  

---

Este repositório contém a implementação de duas soluções complementares para o mapeamento inteligente e gestão de motos nos pátios da Mottu:

1. **Simulação IoT no Wokwi**: Sistema embarcado para localização e monitoramento de motos no pátio
2. **Script Python de Visão Computacional**: Detecção e rastreamento de motos usando processamento de imagens

## Estrutura do Projeto

```
mottu_project/
├── wokwi_simulation/         # Simulação IoT no Wokwi
│   ├── components.md         # Descrição dos componentes utilizados
│   ├── wokwi_circuit.json    # Esquema do circuito para importação no Wokwi
│   ├── mottu_iot_firmware.ino # Código para ESP32
│   └── README.md             # Documentação da simulação IoT
│
└── python_vision/           # Script de Visão Computacional
    ├── mottu_detector.py    # Implementação inicial com YOLOv8
    ├── mottu_detector_improved.py # Versão melhorada com detecção por cor
    ├── research.md          # Pesquisa sobre modelos de visão computacional
    ├── images/              # Imagens de teste e resultados
    │   ├── sample/          # Imagens simuladas de pátio
    │   └── results/         # Resultados do processamento
    └── README.md            # Documentação do script de visão computacional
```

## Simulação IoT no Wokwi

A simulação IoT implementa um sistema embarcado para localização e monitoramento de motos no pátio da Mottu, utilizando ESP32 como microcontrolador principal.

### Funcionalidades Implementadas

- **Localização de motos**: Coordenadas X e Y simuladas no pátio
- **Visualização em tempo real**: Display OLED com mapa do pátio
- **Status visual**: LEDs coloridos indicando diferentes estados das motos
- **Sistema de alerta**: Buzzer para localização física das motos
- **Comunicação de dados**: Simulação de envio via MQTT para backend

### Como Utilizar

1. Acesse o [Wokwi](https://wokwi.com/)
2. Crie um novo projeto ESP32
3. Importe o arquivo `wokwi_circuit.json` para configurar o circuito
4. Copie o código do arquivo `mottu_iot_firmware.ino` para o editor
5. Execute a simulação

Para mais detalhes, consulte o [README da simulação IoT](wokwi_simulation/README.md).

## Script Python de Visão Computacional

O script de visão computacional implementa um sistema de detecção e rastreamento de motos em imagens de pátio, utilizando YOLOv8 e técnicas de segmentação por cor.

### Funcionalidades Implementadas

- **Detecção de motos**: Utilizando YOLOv8 para imagens reais e segmentação por cor para imagens simuladas
- **Rastreamento**: Algoritmo personalizado para manter IDs consistentes das motos
- **Visualização**: Bounding boxes coloridas e informações de ID/confiança
- **Processamento de imagens e vídeos**: Suporte a diferentes formatos de entrada

### Como Utilizar

1. Instale as dependências:
   ```
   pip install ultralytics opencv-python numpy matplotlib
   ```

2. Execute o script melhorado:
   ```
   python mottu_detector_improved.py
   ```

3. Para processar suas próprias imagens, modifique o script:
   ```python
   detector = MottuMotorcycleDetector()
   detector.process_image('caminho/para/sua/imagem.jpg', 'caminho/para/saida.jpg')
   ```

## Resultados e Demonstração

### Simulação IoT
A simulação IoT no Wokwi demonstra um sistema funcional para localização e monitoramento de motos no pátio, com interface visual e alertas sonoros.

### Visão Computacional
O script de visão computacional foi testado com imagens simuladas de pátio, demonstrando capacidade de detecção e rastreamento de múltiplas motos simultaneamente.

## Limitações e Recomendações

### Simulação IoT
- A simulação atual representa um protótipo; para implementação real, seria necessário utilizar hardware físico e GPS real
- Recomenda-se integração com sistema de backend para armazenamento e análise de dados

### Visão Computacional
- O modelo YOLOv8 pré-treinado funciona bem com imagens reais de motos, mas pode requerer fine-tuning para o contexto específico dos pátios da Mottu
- Para imagens simuladas, foi implementada uma abordagem baseada em segmentação por cor
- Em ambiente de produção, recomenda-se:
  - Coletar dataset específico de motos em pátios da Mottu
  - Realizar fine-tuning do modelo YOLOv8
  - Implementar DeepSORT completo para rastreamento mais robusto

## Próximos Passos

1. Integrar as duas soluções em um sistema unificado
2. Implementar backend para armazenamento e análise de dados
3. Desenvolver interface web/mobile para gestores e operadores
4. Realizar testes em ambiente real com câmeras e motos físicas

## Contribuições

Este projeto foi desenvolvido como prova de conceito para o sistema de mapeamento inteligente do pátio da Mottu. Contribuições são bem-vindas através de pull requests.
