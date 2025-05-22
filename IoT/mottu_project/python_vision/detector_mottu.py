import cv2
import numpy as np
import os
import time
import matplotlib.pyplot as plt

class MottuMotorcycleDetector:
    def __init__(self):
        """
        Inicializa o detector de motos simplificado para o projeto Mottu.
        """
        print("Inicializando o detector de motos Mottu...")
        
        # Dicionário para rastreamento simples
        self.tracked_motorcycles = {}
        self.next_id = 1
        
        print("Detector de motos Mottu inicializado com sucesso!")
    
    def detect_motorcycles(self, image):
        """
        Detecta motos na imagem fornecida usando segmentação por cor.
        """
        detections = []
        
        # Converter para HSV para melhor segmentação de cores
        hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)
        
        # Definir faixas de cores para detectar (vermelho, verde, azul, amarelo, ciano)
        color_ranges = [
            # Vermelho
            (np.array([0, 100, 100]), np.array([10, 255, 255])),
            # Verde
            (np.array([40, 100, 100]), np.array([80, 255, 255])),
            # Azul
            (np.array([100, 100, 100]), np.array([140, 255, 255])),
            # Amarelo
            (np.array([20, 100, 100]), np.array([35, 255, 255])),
            # Ciano
            (np.array([85, 100, 100]), np.array([95, 255, 255]))
        ]
        
        # Para cada faixa de cor
        for lower, upper in color_ranges:
            # Criar máscara para a cor atual
            mask = cv2.inRange(hsv, lower, upper)
            
            # Encontrar contornos
            contours, _ = cv2.findContours(mask, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            
            # Para cada contorno encontrado
            for contour in contours:
                # Filtrar por área mínima para evitar ruído
                if cv2.contourArea(contour) > 300:
                    # Obter retângulo delimitador
                    x, y, w, h = cv2.boundingRect(contour)
                    
                    # Adicionar à lista de detecções
                    detections.append((x, y, x+w, y+h, 0.95, 3))
        
        return detections
    
    def track_motorcycles(self, image, detections):
        """
        Implementa um rastreador simples para as motos detectadas.
        """
        # Imagem para visualização
        output_image = image.copy()
        
        # Lista de centros das detecções atuais
        current_centers = []
        for x1, y1, x2, y2, conf, cls in detections:
            center_x = (x1 + x2) // 2
            center_y = (y1 + y2) // 2
            current_centers.append((center_x, center_y, x1, y1, x2, y2, conf))
        
        # Se não há motos rastreadas ainda, inicializar com as detecções atuais
        if not self.tracked_motorcycles:
            for center_x, center_y, x1, y1, x2, y2, conf in current_centers:
                self.tracked_motorcycles[self.next_id] = {
                    'center': (center_x, center_y),
                    'bbox': (x1, y1, x2, y2),
                    'conf': conf,
                    'frames_tracked': 1,
                    'last_seen': time.time()
                }
                self.next_id += 1
        else:
            # Associar detecções atuais com motos rastreadas
            for moto_id in self.tracked_motorcycles:
                self.tracked_motorcycles[moto_id]['updated'] = False
            
            for center_x, center_y, x1, y1, x2, y2, conf in current_centers:
                best_match_id = None
                min_distance = float('inf')
                
                for moto_id, moto_data in self.tracked_motorcycles.items():
                    if moto_data.get('updated', False):
                        continue
                    
                    tracked_center = moto_data['center']
                    distance = np.sqrt((center_x - tracked_center[0])**2 + 
                                      (center_y - tracked_center[1])**2)
                    
                    if distance < 100 and distance < min_distance:
                        min_distance = distance
                        best_match_id = moto_id
                
                if best_match_id is not None:
                    self.tracked_motorcycles[best_match_id]['center'] = (center_x, center_y)
                    self.tracked_motorcycles[best_match_id]['bbox'] = (x1, y1, x2, y2)
                    self.tracked_motorcycles[best_match_id]['conf'] = conf
                    self.tracked_motorcycles[best_match_id]['frames_tracked'] += 1
                    self.tracked_motorcycles[best_match_id]['last_seen'] = time.time()
                    self.tracked_motorcycles[best_match_id]['updated'] = True
                else:
                    self.tracked_motorcycles[self.next_id] = {
                        'center': (center_x, center_y),
                        'bbox': (x1, y1, x2, y2),
                        'conf': conf,
                        'frames_tracked': 1,
                        'last_seen': time.time(),
                        'updated': True
                    }
                    self.next_id += 1
            
            # Remover motos que não foram vistas recentemente
            current_time = time.time()
            moto_ids_to_remove = []
            for moto_id, moto_data in self.tracked_motorcycles.items():
                if current_time - moto_data['last_seen'] > 5.0:
                    moto_ids_to_remove.append(moto_id)
            
            for moto_id in moto_ids_to_remove:
                del self.tracked_motorcycles[moto_id]
        
        # Desenhar as motos rastreadas na imagem
        for moto_id, moto_data in self.tracked_motorcycles.items():
            x1, y1, x2, y2 = moto_data['bbox']
            
            # Cor baseada no ID
            color_r = (moto_id * 50) % 255
            color_g = (moto_id * 100) % 255
            color_b = (moto_id * 150) % 255
            color = (color_b, color_g, color_r)  # OpenCV usa BGR
            
            # Desenhar retângulo
            cv2.rectangle(output_image, (x1, y1), (x2, y2), color, 2)
            
            # Desenhar ID e confiança
            label = f"Moto #{moto_id} ({moto_data['conf']:.2f})"
            cv2.putText(output_image, label, (x1, y1-10), 
                       cv2.FONT_HERSHEY_SIMPLEX, 0.5, color, 2)
            
            # Desenhar centro
            center_x, center_y = moto_data['center']
            cv2.circle(output_image, (center_x, center_y), 5, color, -1)
        
        return output_image, self.tracked_motorcycles
    
    def process_image(self, image_path, output_path=None):
        """
        Processa uma imagem para detectar e rastrear motos.
        """
        # Carregar imagem
        image = cv2.imread(image_path)
        if image is None:
            print(f"Erro ao carregar a imagem: {image_path}")
            return None
        
        # Detectar motos
        detections = self.detect_motorcycles(image)
        
        # Rastrear motos
        output_image, tracked_motos = self.track_motorcycles(image, detections)
        
        # Adicionar informações na imagem
        cv2.putText(output_image, f"Motos detectadas: {len(detections)}", 
                   (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 2)
        
        cv2.putText(output_image, f"Motos rastreadas: {len(tracked_motos)}", 
                   (10, 60), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 255, 0), 2)
        
        # Salvar imagem de saída se especificado
        if output_path:
            os.makedirs(os.path.dirname(output_path), exist_ok=True)
            cv2.imwrite(output_path, output_image)
            print(f"Imagem processada salva em: {output_path}")
        
        return output_image

# Função para criar imagens de amostra
def create_sample_images():
    # Criar diretórios necessários
    current_dir = os.path.dirname(os.path.abspath(__file__))
    sample_dir = os.path.join(current_dir, 'images', 'sample')
    results_dir = os.path.join(current_dir, 'images', 'results')
    
    os.makedirs(sample_dir, exist_ok=True)
    os.makedirs(results_dir, exist_ok=True)
    
    # Criar imagens simuladas de pátio com motos
    def create_sample_image(filename, num_motos=5, width=800, height=600):
        # Criar imagem base (pátio)
        img = np.ones((height, width, 3), dtype=np.uint8) * 200  # Fundo cinza claro
        
        # Desenhar linhas de estacionamento
        for i in range(0, width, 100):
            cv2.line(img, (i, 0), (i, height), (150, 150, 150), 2)
        
        for i in range(0, height, 100):
            cv2.line(img, (0, i), (width, i), (150, 150, 150), 2)
        
        # Adicionar motos (retângulos coloridos)
        colors = [(0, 0, 255), (0, 255, 0), (255, 0, 0), (255, 255, 0), (0, 255, 255)]
        
        for i in range(num_motos):
            # Posição aleatória para a moto
            x = np.random.randint(50, width-100)
            y = np.random.randint(50, height-50)
            
            # Tamanho da moto
            w = np.random.randint(40, 80)
            h = np.random.randint(20, 40)
            
            # Cor da moto
            color = colors[i % len(colors)]
            
            # Desenhar a moto (retângulo)
            cv2.rectangle(img, (x, y), (x+w, y+h), color, -1)
            
            # Adicionar detalhes à moto
            cv2.circle(img, (x+10, y+h-10), 8, (0, 0, 0), -1)  # Roda traseira
            cv2.circle(img, (x+w-10, y+h-10), 8, (0, 0, 0), -1)  # Roda dianteira
            
            # Adicionar ID da moto
            cv2.putText(img, f'M{i+1}', (x+w//2-10, y+h//2+5), 
                       cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2)
        
        # Salvar a imagem
        cv2.imwrite(filename, img)
        print(f'Imagem criada: {filename}')
    
    # Criar três imagens de exemplo
    create_sample_image(os.path.join(sample_dir, 'patio_motos_1.jpg'), num_motos=5)
    create_sample_image(os.path.join(sample_dir, 'patio_motos_2.jpg'), num_motos=8)
    create_sample_image(os.path.join(sample_dir, 'patio_motos_3.jpg'), num_motos=3)
    
    print("Imagens de amostra criadas com sucesso!")

# Função principal
def main():
    # Criar imagens de amostra
    create_sample_images()
    
    # Inicializar detector
    detector = MottuMotorcycleDetector()
    
    # Processar cada imagem de amostra
    current_dir = os.path.dirname(os.path.abspath(__file__))
    sample_dir = os.path.join(current_dir, 'images', 'sample')
    results_dir = os.path.join(current_dir, 'images', 'results')
    
    processed_images = []
    for filename in os.listdir(sample_dir):
        if filename.endswith(('.jpg', '.jpeg', '.png')):
            input_path = os.path.join(sample_dir, filename)
            output_path = os.path.join(results_dir, f"processed_{filename}")
            
            # Processar imagem
            detector.process_image(input_path, output_path)
            processed_images.append(output_path)
    
    print(f"Processamento concluído! {len(processed_images)} imagens processadas.")
    
    # Mostrar uma das imagens processadas
    if processed_images:
        img = cv2.imread(processed_images[0])
        img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
        plt.figure(figsize=(10, 8))
        plt.imshow(img_rgb)
        plt.title("Exemplo de Detecção e Rastreamento de Motos")
        plt.axis('off')
        plt.savefig(os.path.join(results_dir, 'visualization.png'))
        print(f"Visualização salva em: {os.path.join(results_dir, 'visualization.png')}")

if __name__ == "__main__":
    main()
