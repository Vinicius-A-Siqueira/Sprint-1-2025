#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <ArduinoJson.h>

// Definições do display OLED
#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET    -1
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

// Definições dos pinos
#define BUZZER_PIN 13
#define BTN_FIND_PIN 12
#define BTN_STATUS_PIN 14
#define LED_RED_PIN 27
#define LED_GREEN_PIN 26
#define LED_BLUE_PIN 25
#define POT_X_PIN 34
#define POT_Y_PIN 35

// Estrutura para armazenar dados das motos
struct Moto {
  int id;
  float x;
  float y;
  String status;
  bool selected;
};

// Array de motos no pátio (simulação)
#define NUM_MOTOS 5
Moto motos[NUM_MOTOS];

// Variáveis globais
int selectedMotoIndex = 0;
unsigned long lastUpdateTime = 0;
unsigned long lastBlinkTime = 0;
bool ledState = false;
bool buzzerActive = false;
unsigned long buzzerStartTime = 0;

void setup() {
  // Inicializa comunicação serial
  Serial.begin(115200);
  Serial.println("Mottu - Sistema de Mapeamento Inteligente do Pátio");
  
  // Inicializa pinos
  pinMode(BUZZER_PIN, OUTPUT);
  pinMode(BTN_FIND_PIN, INPUT_PULLUP);
  pinMode(BTN_STATUS_PIN, INPUT_PULLUP);
  pinMode(LED_RED_PIN, OUTPUT);
  pinMode(LED_GREEN_PIN, OUTPUT);
  pinMode(LED_BLUE_PIN, OUTPUT);
  
  // Inicializa display OLED
  if(!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    Serial.println(F("Falha ao inicializar o display SSD1306"));
    for(;;);
  }
  display.clearDisplay();
  display.setTextSize(1);
  display.setTextColor(SSD1306_WHITE);
  display.setCursor(0, 0);
  display.println(F("Mottu - Mapeamento"));
  display.println(F("Inteligente do Patio"));
  display.display();
  delay(2000);
  
  // Inicializa dados das motos (simulação)
  inicializaMotos();
}

void loop() {
  // Lê os potenciômetros para simular movimento da moto selecionada
  if (millis() - lastUpdateTime > 500) {
    atualizaPosicaoMoto();
    lastUpdateTime = millis();
  }
  
  // Verifica botões
  verificaBotoes();
  
  // Atualiza display
  atualizaDisplay();
  
  // Controla buzzer para localização
  controlaBuzzer();
  
  // Envia dados para serial (simulando MQTT)
  if (millis() % 5000 == 0) {
    enviaStatusMQTT();
  }
}

void inicializaMotos() {
  // Inicializa motos com posições e status aleatórios
  String statusOptions[] = {"Disponível", "Em uso", "Manutenção", "Reservada"};
  
  for (int i = 0; i < NUM_MOTOS; i++) {
    motos[i].id = 1000 + i;
    motos[i].x = random(10, 90);
    motos[i].y = random(10, 90);
    motos[i].status = statusOptions[random(0, 4)];
    motos[i].selected = (i == 0); // A primeira moto começa selecionada
  }
}

void atualizaPosicaoMoto() {
  // Lê potenciômetros para simular movimento da moto selecionada
  int rawX = analogRead(POT_X_PIN);
  int rawY = analogRead(POT_Y_PIN);
  
  // Mapeia valores para coordenadas do pátio (0-100)
  float newX = map(rawX, 0, 4095, 0, 100);
  float newY = map(rawY, 0, 4095, 0, 100);
  
  // Atualiza posição da moto selecionada
  motos[selectedMotoIndex].x = newX;
  motos[selectedMotoIndex].y = newY;
  
  // Imprime posição no serial
  Serial.print("Moto ID: ");
  Serial.print(motos[selectedMotoIndex].id);
  Serial.print(" - Posição: X=");
  Serial.print(newX);
  Serial.print(", Y=");
  Serial.println(newY);
}

void verificaBotoes() {
  // Botão para encontrar moto (ativa buzzer)
  if (digitalRead(BTN_FIND_PIN) == LOW) {
    buzzerActive = true;
    buzzerStartTime = millis();
    Serial.print("Localizando moto ID: ");
    Serial.println(motos[selectedMotoIndex].id);
  }
  
  // Botão para mudar moto selecionada
  if (digitalRead(BTN_STATUS_PIN) == LOW) {
    delay(200); // Debounce
    selectedMotoIndex = (selectedMotoIndex + 1) % NUM_MOTOS;
    Serial.print("Moto selecionada: ID ");
    Serial.println(motos[selectedMotoIndex].id);
  }
}

void atualizaDisplay() {
  display.clearDisplay();
  
  // Título
  display.setTextSize(1);
  display.setCursor(0, 0);
  display.println("Mottu - Mapeamento");
  
  // Informações da moto selecionada
  display.setCursor(0, 16);
  display.print("Moto ID: ");
  display.println(motos[selectedMotoIndex].id);
  
  display.setCursor(0, 26);
  display.print("Status: ");
  display.println(motos[selectedMotoIndex].status);
  
  display.setCursor(0, 36);
  display.print("Pos: X=");
  display.print(motos[selectedMotoIndex].x, 1);
  display.print(", Y=");
  display.println(motos[selectedMotoIndex].y, 1);
  
  // Desenha mapa simplificado do pátio
  display.drawRect(80, 16, 48, 48, SSD1306_WHITE);
  
  // Desenha posição da moto no mapa
  int posX = map(motos[selectedMotoIndex].x, 0, 100, 80, 128);
  int posY = map(motos[selectedMotoIndex].y, 0, 100, 16, 64);
  display.fillCircle(posX, posY, 3, SSD1306_WHITE);
  
  display.display();
  
  // Atualiza LEDs baseado no status
  atualizaLEDs();
}

void atualizaLEDs() {
  // Configura LEDs baseado no status da moto
  if (motos[selectedMotoIndex].status == "Disponível") {
    digitalWrite(LED_RED_PIN, LOW);
    digitalWrite(LED_GREEN_PIN, HIGH);
    digitalWrite(LED_BLUE_PIN, LOW);
  } 
  else if (motos[selectedMotoIndex].status == "Em uso") {
    digitalWrite(LED_RED_PIN, LOW);
    digitalWrite(LED_GREEN_PIN, LOW);
    digitalWrite(LED_BLUE_PIN, HIGH);
  }
  else if (motos[selectedMotoIndex].status == "Manutenção") {
    digitalWrite(LED_RED_PIN, HIGH);
    digitalWrite(LED_GREEN_PIN, LOW);
    digitalWrite(LED_BLUE_PIN, LOW);
  }
  else if (motos[selectedMotoIndex].status == "Reservada") {
    // Pisca LED amarelo (vermelho + verde)
    if (millis() - lastBlinkTime > 500) {
      ledState = !ledState;
      lastBlinkTime = millis();
    }
    digitalWrite(LED_RED_PIN, ledState);
    digitalWrite(LED_GREEN_PIN, ledState);
    digitalWrite(LED_BLUE_PIN, LOW);
  }
}

void controlaBuzzer() {
  // Controla buzzer para localização da moto
  if (buzzerActive) {
    // Padrão de beeps para localização
    int beepPattern = (millis() / 200) % 3;
    if (beepPattern == 0) {
      tone(BUZZER_PIN, 2000);
    } else {
      noTone(BUZZER_PIN);
    }
    
    // Desativa buzzer após 5 segundos
    if (millis() - buzzerStartTime > 5000) {
      buzzerActive = false;
      noTone(BUZZER_PIN);
    }
  }
}

void enviaStatusMQTT() {
  // Simula envio de dados via MQTT
  StaticJsonDocument<256> doc;
  
  doc["device_id"] = "ESP32_PATIO_01";
  doc["timestamp"] = millis();
  
  JsonObject motoData = doc.createNestedObject("moto");
  motoData["id"] = motos[selectedMotoIndex].id;
  motoData["x"] = motos[selectedMotoIndex].x;
  motoData["y"] = motos[selectedMotoIndex].y;
  motoData["status"] = motos[selectedMotoIndex].status;
  
  // Serializa JSON para string
  String jsonOutput;
  serializeJson(doc, jsonOutput);
  
  // Envia para serial (simulando MQTT)
  Serial.print("MQTT > ");
  Serial.println(jsonOutput);
}
