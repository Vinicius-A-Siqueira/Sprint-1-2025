import numpy as np
import cv2
import os

# Criar diretórios necessários
os.makedirs('images/sample', exist_ok=True)
os.makedirs('images/results', exist_ok=True)

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
create_sample_image('images/sample/patio_motos_1.jpg', num_motos=5)
create_sample_image('images/sample/patio_motos_2.jpg', num_motos=8)
create_sample_image('images/sample/patio_motos_3.jpg', num_motos=3)

print("Imagens de amostra criadas com sucesso!")
