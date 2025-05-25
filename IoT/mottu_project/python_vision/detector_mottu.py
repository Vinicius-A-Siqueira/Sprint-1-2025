import cv2
import numpy as np
import os
import time
import matplotlib.pyplot as plt
import time
import glob
import os

# Função auxiliar para ler imagem com suporte a caracteres especiais no caminho
def imread_special_chars(image_path):
    try:
        n = np.fromfile(image_path, np.uint8)
        img = cv2.imdecode(n, cv2.IMREAD_COLOR)
        if img is None:
            raise ValueError("Falha ao decodificar a imagem - pode estar corrompida ou formato inválido")
        return img
    except Exception as e:
        print(f"Erro ao carregar a imagem com imdecode: {image_path} - {e}")
        return None

# Função auxiliar para salvar imagem com suporte a caracteres especiais no caminho
def imwrite_special_chars(filename, img):
    try:
        ext = os.path.splitext(filename)[1]
        result, buf = cv2.imencode(ext, img)
        if result:
            buf.tofile(filename)
            return True
        else:
            print(f"Erro ao codificar a imagem para salvar: {filename}")
            return False
    except Exception as e:
        print(f"Erro ao salvar a imagem com tofile: {filename} - {e}")
        return False

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
        if image is None:
            print("Erro: Imagem de entrada para detecção está vazia.")
            return []
            
        detections = []
        
        # Converter para HSV para melhor segmentação de cores
        try:
            hsv = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)
        except cv2.error as e:
            print(f"Erro ao converter imagem para HSV: {e}")
            return []
        
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
        if image is None:
            print("Erro: Imagem de entrada para rastreamento está vazia.")
            return None, {}
            
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
            for moto_id in list(self.tracked_motorcycles.keys()): # Usar list() para poder remover itens durante a iteração
                self.tracked_motorcycles[moto_id]['updated'] = False
            
            matched_ids = set()
            for center_x, center_y, x1, y1, x2, y2, conf in current_centers:
                best_match_id = None
                min_distance = float('inf')
                
                for moto_id, moto_data in self.tracked_motorcycles.items():
                    # Não associar uma detecção a uma moto que já foi associada neste frame
                    # ou que não foi atualizada no passo anterior (evita dupla associação)
                    if moto_id in matched_ids or moto_data.get('updated', False):
                        continue
                    
                    tracked_center = moto_data['center']
                    distance = np.sqrt((center_x - tracked_center[0])**2 + 
                                      (center_y - tracked_center[1])**2)
                    
                    # Limiar de distância para associação (ajustável)
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
                    matched_ids.add(best_match_id)
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
                    # Não adicionar a matched_ids aqui, pois é um novo ID
                    self.next_id += 1
            
            # Remover motos que não foram vistas recentemente ou não foram atualizadas
            current_time = time.time()
            moto_ids_to_remove = []
            for moto_id, moto_data in self.tracked_motorcycles.items():
                # Remover se não foi atualizado neste frame OU se não é visto há muito tempo
                if not moto_data.get('updated', False) or (current_time - moto_data['last_seen'] > 5.0):
                     # Adicionar uma verificação extra: só remover se não foi visto por um tempo
                     if current_time - moto_data['last_seen'] > 5.0:
                        moto_ids_to_remove.append(moto_id)
                    
            for moto_id in moto_ids_to_remove:
                if moto_id in self.tracked_motorcycles:
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
        # Carregar imagem usando a função auxiliar
        image = imread_special_chars(image_path)
        if image is None:
            # A função auxiliar já imprime o erro
            return None
        
        # Detectar motos
        detections = self.detect_motorcycles(image)
        
        # Rastrear motos
        output_image, tracked_motos = self.track_motorcycles(image, detections)
        
        if output_image is None:
             print("Erro durante o rastreamento.")
             return None

        # Adicionar informações na imagem
        cv2.putText(output_image, f"Motos detectadas: {len(detections)}", 
                   (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 2)
        
        cv2.putText(output_image, f"Motos rastreadas: {len(tracked_motos)}", 
                   (10, 60), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 255, 0), 2)
        
        # Salvar imagem de saída se especificado, usando a função auxiliar
        if output_path:
            os.makedirs(os.path.dirname(output_path), exist_ok=True)
            if imwrite_special_chars(output_path, output_image):
                print(f"Imagem processada salva em: {output_path}")
            else:
                 print(f"Falha ao salvar a imagem processada em: {output_path}")
        
        return output_image

# Função para criar imagens de amostra
def create_sample_images():
    # Obter o diretório do script atual (.../python_vision)
    script_dir = os.path.dirname(os.path.abspath(__file__))
    # Subir um nível para obter a raiz do projeto (.../mottu_project)
    project_root = os.path.dirname(script_dir)
    # Definir os diretórios de sample e results com base na raiz do projeto
    sample_dir = os.path.join(project_root, 'images', 'sample')
    results_dir = os.path.join(project_root, 'images', 'results')
    
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
        
        # Salvar a imagem usando a função auxiliar
        if imwrite_special_chars(filename, img):
             print(f'Imagem criada: {filename}')
        else:
             print(f'Falha ao criar imagem: {filename}')

    # Criar três imagens de exemplo
    create_sample_image(os.path.join(sample_dir, 'patio_motos_1.jpg'), num_motos=5)
    create_sample_image(os.path.join(sample_dir, 'patio_motos_2.jpg'), num_motos=8)
    create_sample_image(os.path.join(sample_dir, 'patio_motos_3.jpg'), num_motos=3)
    
    print("Criação de imagens de amostra concluída!") # Mensagem ajustada

# Função principal
def main():
    # Criar imagens de amostra
    create_sample_images()
    time.sleep(1) # Manter a pausa por precaução

    # Inicializar detector
    detector = MottuMotorcycleDetector()

    # Processar cada imagem de amostra
    script_dir = os.path.dirname(os.path.abspath(__file__))
    project_root = os.path.dirname(script_dir)
    sample_dir = os.path.join(project_root, 'images', 'sample') 
    results_dir = os.path.join(project_root, 'images', 'results') 

    print(f"Procurando por imagens em: {sample_dir}")
    print(f"Verificando se o diretório existe: {os.path.isdir(sample_dir)}") 
    arquivos_jpg = glob.glob(os.path.join(sample_dir, '*.jpg'))
    print(f"Arquivos .jpg encontrados com glob: {arquivos_jpg}")

    # Verificar se algum arquivo foi encontrado antes de prosseguir
    if not arquivos_jpg:
        print("Nenhum arquivo .jpg encontrado para processar.")
        return # Sair se não houver arquivos

    processed_images_paths = []
    processed_images_count = 0
    for filepath in arquivos_jpg:
        filename = os.path.basename(filepath) 
        print(f"Processando arquivo: {filename}")
        # Não precisa mais do if filename.endswith, glob já filtrou
        # input_path = os.path.join(sample_dir, filename) # Usar filepath diretamente
        output_path = os.path.join(results_dir, f"processed_{filename}")

        # Processar imagem
        processed_image = detector.process_image(filepath, output_path)
        if processed_image is not None:
             processed_images_paths.append(output_path)
             processed_images_count += 1
        else:
             print(f"Falha ao processar {filename}")

    print(f"Processamento concluído! {processed_images_count} imagens processadas com sucesso.")
        
    # Mostrar uma das imagens processadas
    if processed_images_paths:
        # Ler a primeira imagem processada com sucesso para visualização
        first_processed_path = processed_images_paths[0]
        img = imread_special_chars(first_processed_path)
        
        if img is not None:
            try:
                img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
                plt.figure(figsize=(10, 8))
                plt.imshow(img_rgb)
                plt.title("Exemplo de Detecção e Rastreamento de Motos")
                plt.axis('off')
                visualization_path = os.path.join(results_dir, 'visualization.png')
                # Salvar a visualização (matplotlib geralmente lida bem com caminhos)
                plt.savefig(visualization_path)
                print(f"Visualização salva em: {visualization_path}")
            except cv2.error as e:
                 print(f"Erro do OpenCV ao tentar mostrar a visualização: {e}")
            except Exception as e:
                 print(f"Erro ao tentar mostrar a visualização com Matplotlib: {e}")
        else:
            print(f"Não foi possível ler a imagem processada {first_processed_path} para visualização.")
    else:
        print("Nenhuma imagem foi processada com sucesso para gerar visualização.")

if __name__ == "__main__":
    main()