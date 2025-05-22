import cv2
import numpy as np
import os
import time
from ultralytics import YOLO
import matplotlib.pyplot as plt

class MottuMotorcycleDetector:
    def __init__(self, model_path=None):
        """
        Inicializa o detector de motos para o projeto Mottu.
        
        Args:
            model_path: Caminho para o modelo YOLOv8 pré-treinado. Se None, usa o modelo padrão.
        """
        print("Inicializando o detector de motos Mottu...")
        
        # Carregar o modelo YOLOv8
        if model_path:
            self.model = YOLO(model_path)
        else:
            # Usar modelo YOLOv8n pré-treinado
            self.model = YOLO('yolov8n.pt')
        
        # Classe para motos no COCO dataset (ID 3)
        self.motorcycle_class_id = 3
        
        # Dicionário para rastreamento simples
        self.tracked_motorcycles = {}
        self.next_id = 1
        
        print("Detector de motos Mottu inicializado com sucesso!")
    
    def detect_motorcycles(self, image, conf_threshold=0.25):
        """
        Detecta motos na imagem fornecida.
        
        Args:
            image: Imagem de entrada (numpy array)
            conf_threshold: Limiar de confiança para detecções
            
        Returns:
            Imagem com detecções marcadas
            Lista de detecções (x1, y1, x2, y2, conf, class_id)
        """
        # Executar inferência com YOLOv8
        results = self.model(image, conf=conf_threshold)
        
        # Filtrar apenas detecções de motos (classe 3 no COCO)
        detections = []
        
        # Processar resultados
        for r in results:
            boxes = r.boxes
            for box in boxes:
                # Verificar se é uma moto
                cls = int(box.cls[0].item())
                if cls == self.motorcycle_class_id:  # Classe de moto no COCO
                    # Obter coordenadas e confiança
                    x1, y1, x2, y2 = box.xyxy[0].cpu().numpy()
                    conf = box.conf[0].item()
                    
                    # Adicionar à lista de detecções
                    detections.append((int(x1), int(y1), int(x2), int(y2), conf, cls))
        
        return detections
    
    def track_motorcycles(self, image, detections):
        """
        Implementa um rastreador simples para as motos detectadas.
        
        Args:
            image: Imagem de entrada
            detections: Lista de detecções (x1, y1, x2, y2, conf, class_id)
            
        Returns:
            Imagem com rastreamento marcado
            Dicionário de motos rastreadas
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
            # Implementação simplificada baseada em distância entre centros
            
            # Marcar todas as motos como não atualizadas
            for moto_id in self.tracked_motorcycles:
                self.tracked_motorcycles[moto_id]['updated'] = False
            
            # Para cada detecção atual
            for center_x, center_y, x1, y1, x2, y2, conf in current_centers:
                best_match_id = None
                min_distance = float('inf')
                
                # Encontrar a moto rastreada mais próxima
                for moto_id, moto_data in self.tracked_motorcycles.items():
                    if moto_data.get('updated', False):
                        continue  # Já foi associada
                    
                    tracked_center = moto_data['center']
                    distance = np.sqrt((center_x - tracked_center[0])**2 + 
                                      (center_y - tracked_center[1])**2)
                    
                    # Se a distância for menor que um limiar
                    if distance < 100 and distance < min_distance:  # 100 pixels como limiar
                        min_distance = distance
                        best_match_id = moto_id
                
                # Atualizar a moto rastreada ou criar uma nova
                if best_match_id is not None:
                    self.tracked_motorcycles[best_match_id]['center'] = (center_x, center_y)
                    self.tracked_motorcycles[best_match_id]['bbox'] = (x1, y1, x2, y2)
                    self.tracked_motorcycles[best_match_id]['conf'] = conf
                    self.tracked_motorcycles[best_match_id]['frames_tracked'] += 1
                    self.tracked_motorcycles[best_match_id]['last_seen'] = time.time()
                    self.tracked_motorcycles[best_match_id]['updated'] = True
                else:
                    # Nova moto detectada
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
                if current_time - moto_data['last_seen'] > 5.0:  # 5 segundos sem ser vista
                    moto_ids_to_remove.append(moto_id)
            
            for moto_id in moto_ids_to_remove:
                del self.tracked_motorcycles[moto_id]
        
        # Desenhar as motos rastreadas na imagem
        for moto_id, moto_data in self.tracked_motorcycles.items():
            x1, y1, x2, y2 = moto_data['bbox']
            
            # Cor baseada no ID (para diferenciar visualmente)
            color_r = (moto_id * 50) % 255
            color_g = (moto_id * 100) % 255
            color_b = (moto_id * 150) % 255
            color = (color_r, color_g, color_b)
            
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
        
        Args:
            image_path: Caminho para a imagem de entrada
            output_path: Caminho para salvar a imagem de saída. Se None, não salva.
            
        Returns:
            Imagem processada com detecções e rastreamento
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
    
    def process_video(self, video_path, output_path=None, display=False):
        """
        Processa um vídeo para detectar e rastrear motos.
        
        Args:
            video_path: Caminho para o vídeo de entrada
            output_path: Caminho para salvar o vídeo de saída. Se None, não salva.
            display: Se True, exibe o vídeo durante o processamento
            
        Returns:
            True se processado com sucesso, False caso contrário
        """
        # Abrir vídeo
        cap = cv2.VideoCapture(video_path)
        if not cap.isOpened():
            print(f"Erro ao abrir o vídeo: {video_path}")
            return False
        
        # Obter propriedades do vídeo
        width = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
        height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
        fps = cap.get(cv2.CAP_PROP_FPS)
        
        # Configurar gravador de vídeo se necessário
        if output_path:
            fourcc = cv2.VideoWriter_fourcc(*'mp4v')
            out = cv2.VideoWriter(output_path, fourcc, fps, (width, height))
        
        # Processar cada frame
        frame_count = 0
        while True:
            ret, frame = cap.read()
            if not ret:
                break
            
            # Detectar motos
            detections = self.detect_motorcycles(frame)
            
            # Rastrear motos
            output_frame, tracked_motos = self.track_motorcycles(frame, detections)
            
            # Adicionar informações no frame
            cv2.putText(output_frame, f"Frame: {frame_count} | Motos: {len(tracked_motos)}", 
                       (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 2)
            
            # Salvar frame no vídeo de saída
            if output_path:
                out.write(output_frame)
            
            # Exibir frame se solicitado
            if display:
                cv2.imshow('Mottu - Detecção de Motos', output_frame)
                if cv2.waitKey(1) & 0xFF == ord('q'):
                    break
            
            frame_count += 1
        
        # Liberar recursos
        cap.release()
        if output_path:
            out.release()
        if display:
            cv2.destroyAllWindows()
        
        print(f"Processamento de vídeo concluído. {frame_count} frames processados.")
        return True
    
    def process_sample_images(self):
        """
        Processa as imagens de amostra e gera visualizações.
        
        Returns:
            Lista de caminhos para as imagens processadas
        """
        # Diretório de imagens de amostra
        sample_dir = 'images/sample'
        output_dir = 'images/results'
        os.makedirs(output_dir, exist_ok=True)
        
        # Processar cada imagem de amostra
        processed_images = []
        for filename in os.listdir(sample_dir):
            if filename.endswith(('.jpg', '.jpeg', '.png')):
                input_path = os.path.join(sample_dir, filename)
                output_path = os.path.join(output_dir, f"processed_{filename}")
                
                # Processar imagem
                self.process_image(input_path, output_path)
                processed_images.append(output_path)
        
        return processed_images
    
    def visualize_results(self, image_paths):
        """
        Visualiza os resultados do processamento.
        
        Args:
            image_paths: Lista de caminhos para as imagens processadas
            
        Returns:
            Caminho para a imagem de visualização salva
        """
        # Configurar o plot
        n_images = len(image_paths)
        fig, axes = plt.subplots(1, n_images, figsize=(15, 5))
        
        # Se houver apenas uma imagem
        if n_images == 1:
            axes = [axes]
        
        # Plotar cada imagem
        for i, path in enumerate(image_paths):
            img = cv2.imread(path)
            img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)  # Converter BGR para RGB
            axes[i].imshow(img)
            axes[i].set_title(f"Imagem {i+1}")
            axes[i].axis('off')
        
        # Ajustar layout
        plt.tight_layout()
        
        # Salvar visualização
        viz_path = 'images/results/visualization.png'
        plt.savefig(viz_path)
        plt.close()
        
        return viz_path


# Função principal para demonstração
def main():
    # Inicializar detector
    detector = MottuMotorcycleDetector()
    
    # Processar imagens de amostra
    processed_images = detector.process_sample_images()
    
    # Visualizar resultados
    viz_path = detector.visualize_results(processed_images)
    print(f"Visualização salva em: {viz_path}")


if __name__ == "__main__":
    main()
