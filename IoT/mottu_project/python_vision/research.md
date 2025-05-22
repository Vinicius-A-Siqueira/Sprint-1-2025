# Pesquisa sobre Modelos de Visão Computacional para Detecção de Motos

## YOLOv8 para Detecção de Motos

O YOLOv8 é uma das versões mais recentes e eficientes da família de modelos YOLO (You Only Look Once), desenvolvido pela Ultralytics. Este modelo é ideal para o projeto Mottu pelos seguintes motivos:

### Vantagens do YOLOv8:

1. **Alta precisão**: Oferece excelente equilíbrio entre velocidade e precisão
2. **Processamento em tempo real**: Capaz de processar vídeos em tempo real, essencial para monitoramento de pátios
3. **Detecção de múltiplos objetos**: Pode detectar várias motos simultaneamente em uma única imagem
4. **Facilidade de uso**: API Python simples e bem documentada
5. **Suporte a diferentes tamanhos de modelo**: Nano, Small, Medium, Large e XLarge, permitindo escolher o melhor equilíbrio entre velocidade e precisão
6. **Treinamento personalizável**: Possibilidade de fine-tuning com imagens específicas de motos em pátios

### Implementação com YOLOv8:

Para o projeto Mottu, utilizaremos o YOLOv8 pré-treinado no dataset COCO, que já inclui a classe "motorcycle". Caso necessário, podemos realizar fine-tuning com imagens específicas de pátios da Mottu para melhorar a precisão.

## DeepSORT para Rastreamento

Após a detecção das motos com YOLOv8, utilizaremos o algoritmo DeepSORT (Simple Online and Realtime Tracking with a Deep Association Metric) para rastreamento contínuo das motos no pátio.

### Vantagens do DeepSORT:

1. **Rastreamento consistente**: Mantém IDs consistentes para cada moto mesmo quando há oclusões temporárias
2. **Baseado em aparência**: Utiliza características visuais para melhorar o rastreamento
3. **Eficiente**: Projetado para funcionar em tempo real
4. **Integração com YOLO**: Funciona bem com os outputs do detector YOLOv8

### Implementação com DeepSORT:

O DeepSORT receberá as detecções do YOLOv8 e atribuirá IDs únicos a cada moto, permitindo rastrear sua movimentação pelo pátio ao longo do tempo.

## Requisitos de Hardware e Software

Para implementação eficiente deste sistema, recomendamos:

- **Hardware**: GPU com pelo menos 4GB de VRAM para processamento em tempo real
- **Software**: Python 3.8+, PyTorch, OpenCV, Ultralytics YOLOv8
- **Câmeras**: Posicionadas estrategicamente para cobrir todo o pátio, com resolução mínima de 1080p

## Métricas de Avaliação

Para avaliar o desempenho do sistema, utilizaremos:

- **Precisão de detecção**: Percentual de motos corretamente identificadas
- **Recall**: Percentual de motos existentes que foram detectadas
- **Consistência de rastreamento**: Tempo médio de manutenção do mesmo ID para uma moto
- **Velocidade de processamento**: FPS (frames por segundo) alcançados

## Próximos Passos

1. Instalar as bibliotecas necessárias
2. Obter imagens de teste representativas de pátios da Mottu
3. Implementar script de detecção com YOLOv8
4. Integrar rastreamento com DeepSORT
5. Criar visualização dos resultados
6. Avaliar desempenho e otimizar parâmetros
