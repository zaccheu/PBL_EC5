#include <DHT.h>
#include <Wire.h> // Biblioteca utilizada para fazer a comunicação com o I2C
#include <LiquidCrystal_I2C.h> // Biblioteca utilizada para fazer a comunicação com o display 20x4 
#include <EEPROM.h>
#include <RTClib.h> // Biblioteca para Relógio em Tempo Real

#define EEPROM_ENDERECO 0  // Endereço da EEPROM para armazenar o flag de inicialização
#define col 16 // Serve para definir o numero de colunas do display utilizado
#define lin  2 // Serve para definir o numero de linhas do display utilizado
#define ende  0x27 // Serve para definir o endereço do display.
#define DHTPIN 9 // Pino do DHT
#define DHTTYPE DHT11  // Tipo de sensor DHT
#define LOG_OPTION 1     // Opção para ativar a leitura do log
#define SERIAL_OPTION 0  // Opção de comunicação serial: 0 para desligado, 1 para ligado
#define UTC_OFFSET 0   // Ajuste de fuso horário


DHT dht(DHTPIN, DHTTYPE); // DHT
RTC_DS3231 RTC;
LiquidCrystal_I2C lcd(ende, col, lin); // Chamada da funcação LiquidCrystal para ser usada com o I2C

const int buttonPin = 2; // Pino onde o botão está conectado
bool lastButtonState = HIGH;

float lastAvgTemp; // Armazena a última média de temperatura
float lastAvgHumd; // Armazena a última média de umidade

float tempReadings[10]; // Array para armazenar as últimas 10 leituras de temperatura
float humdReadings[10]; // Array para armazenar as últimas 10 leituras de umidade
int currentIndex = 0; // Índice para rastrear a posição atual no array de leituras

int lightLevel; //Nível de luz
int initialReadingsCount = 0;  // Contador para ignorar as primeiras 9 leituras
const int pinLDR = A2;  // Pino do LED do LDR
const int ledRed = 8;   // Pino do LED vermelho
const int ledYel = 7;   // Pino do LED amarelo
const int ledGre = 6;   // Pino do LED verde
const int pinBUZ = 13;  // Pino do buzzer

const int maxRecords = 100;
const int recordSize = 8; // Tamanho de cada registro em bytes
int startAddress = 0;
int endAddress = maxRecords * recordSize;
int currentAddress = 0;
int lastLoggedMinute = -1;
const unsigned long logInterval = 2000; // 6 segundos
// Triggers de temperatura e umidade
float trigger_t_min = 15.0; // Exemplo: valor mínimo de temperatura
float trigger_t_max = 25.0; // Exemplo: valor máximo de temperatura
float trigger_u_min = 30.0; // Exemplo: valor mínimo de umidade
float trigger_u_max = 50.0; // Exemplo: valor máximo de umidade
float trigger_l_min = 0.0; // Exemplo: valor mínimo de luminosidade
float trigger_l_max = 30.0;

void setup() {
	//Serial.begin(115200); // Inicializa a comunicação serial
	Wire.begin();
	RTC.begin();    // Inicialização do Relógio em Tempo Real
	RTC.adjust(DateTime(F(__DATE__), F(__TIME__)));
	// RTC.adjust(DateTime(2024, 5, 6, 08, 15, 00));  // Ajustar a data e hora apropriadas uma vez inicialmente, depois comentar
	EEPROM.begin();
	Serial.begin(9600);
	while (!Serial);
	Serial.println("\nI2C Scanner");
	dht.begin(); // Inicializa o sensor DHT
	pinMode(buttonPin, INPUT_PULLUP);
	//analogWrite(pinBUZ, OUTPUT); // Inicializa buzzer
	pinMode(pinBUZ, OUTPUT);
	// Inicializa LEDs
	pinMode(ledRed, OUTPUT);
	pinMode(ledYel, OUTPUT);
	pinMode(ledGre, OUTPUT);

	// Inicializa LDR
	pinMode(pinLDR, INPUT);

	lcd.init();       // primeiro inicia o LCD
	lcd.backlight();  // depois liga a luz
	booting();        // por último exibe mensagens no LCD
	delay(300);
	lcd.clear(); // Limpa o LCD
	lcd.setCursor(1, 0);
	lcd.print(" Loading..."); // Exibe uma mensagem de loading para as primeiras 10 leituras
	if (EEPROM.read(EEPROM_ENDERECO) != 0xA5) {
		Serial.println("EEPROM não inicializada. Limpando...");
		for (int i = 0; i < EEPROM.length(); i++) {
			EEPROM.write(i, 0xFF);
		}
		EEPROM.write(EEPROM_ENDERECO, 0xA5); // Marca EEPROM como inicializada
		Serial.println("EEPROM inicializada com sucesso!");
	}
	else {
		Serial.println("EEPROM já está inicializada. Pulando limpeza.");
	}
	delay(1000);
}



void loop() {
	int valorLDR = analogRead(pinLDR);
	lightLevel = map(valorLDR, 1023, 60, 0, 100); //Utiliza a função map para interpretar os valores do LDR
	lightLevel = constrain(lightLevel, 0, 100);
	float temp = dht.readTemperature(); // Lê a temperatura do sensor DHT
	float humid = dht.readHumidity();   // Lê a umidade do sensor DHT
	bool buttonState = digitalRead(buttonPin);

	if (buttonState == HIGH) { // Se o botão estiver pressionado
		Serial.println("Botão pressionado! Exibindo EEPROM...");
		printEEPROM(0, 1024);
		delay(2000); // Aguarda 1 segundo para evitar muitas leituras repetidas
	}

	verifyLDR(); // Verifica o nível de luz

	// Verifica se há algum LED de 'atenção' ligado e se o ambiente está escuro
	if ((digitalRead(ledYel) == HIGH || digitalRead(ledRed) == HIGH) && lightLevel < trigger_l_max) {
		//Verifica se o LED amarelo pode ser desligado
		if (lastAvgTemp > trigger_t_min && lastAvgTemp < trigger_t_max) {
			digitalWrite(ledYel, LOW);
		}
		//Verifica se o LED vermelho pode ser desligado
		if (lastAvgHumd > trigger_u_min && lastAvgHumd < trigger_u_min) {
			digitalWrite(ledRed, LOW);
		}
		if (lastAvgHumd < trigger_u_min && lastAvgHumd > trigger_u_min && lightLevel < trigger_l_max && lightLevel > trigger_l_min && lastAvgTemp > trigger_t_min && lastAvgTemp > trigger_t_min) {
			digitalWrite(ledGre, HIGH);
		}
	}

	// Verifica se o buzzer pode ser desligado. Exige ambiente escuro e LEDs apagados
	if (digitalRead(ledYel) == LOW && digitalRead(ledRed) == LOW && lightLevel < trigger_l_max) {
		noTone(pinBUZ);
	}


	// Verifica se o buzzer pode ser desligado. Exije ambiente escuro
	if (digitalRead(ledYel) == LOW && digitalRead(ledRed) == LOW && lightLevel > trigger_l_min) {
		noTone(pinBUZ);
	}

	tempReadings[currentIndex] = temp;  // Armazena a leitura atual da temperatura
	humdReadings[currentIndex] = humid;  // Armazena a leitura atual da umidade

	currentIndex = (currentIndex + 1) % 10; // Atualiza o índice para a próxima leitura

	if (currentIndex == 9) {
		tenthRead();
	}
	if (initialReadingsCount < 9) {
		initialReadingsCount++;
		return;  // Sai da função loop() sem registrar no serial ou EEPROM
	}
	// Exibe informações no monitor serial
	if (lastAvgTemp > 1) { serialLog(temp, humid, valorLDR); }

	//DATA LOGGER
	DateTime now = RTC.now();

	// Calculando o deslocamento do fuso horário
	int offsetSeconds = UTC_OFFSET * 3600; // Convertendo horas para segundos
	now = now.unixtime() + offsetSeconds; // Adicionando o deslocamento ao tempo atual

	// Convertendo o novo tempo para DateTime
	DateTime adjustedTime = DateTime(now);

	if (LOG_OPTION) get_log();

	// Verifica se o minuto atual é diferente do minuto do último registro
	if (adjustedTime.minute() != lastLoggedMinute) {
		lastLoggedMinute = adjustedTime.minute();

		digitalWrite(LED_BUILTIN, HIGH);   // Liga o LED
		digitalWrite(LED_BUILTIN, LOW);    // Desliga o LED

		// Ler os valores de temperatura e umidade
		float humid = dht.readHumidity();
		float temp = dht.readTemperature();

		// Verificar se os valores estão fora dos triggers
		if (temp < trigger_t_min || temp > trigger_t_max || humid < trigger_u_min || lightLevel > trigger_l_max || lightLevel < trigger_l_min) {
			// Converter valores para int para armazenamento
			int tempInt = (int)(temp * 100);
			int humiInt = (int)(humid * 100);

			// Escrever dados na EEPROM
			EEPROM.put(currentAddress, now.unixtime());
			EEPROM.put(currentAddress + 4, tempInt);
			EEPROM.put(currentAddress + 6, humiInt);

			// Atualiza o endereço para o próximo registro
			getNextAddress();
		}
	}

	if (SERIAL_OPTION) {
		Serial.print(adjustedTime.day());
		Serial.print("/");
		Serial.print(adjustedTime.month());
		Serial.print("/");
		Serial.print(adjustedTime.year());
		Serial.print(" ");
		Serial.print(adjustedTime.hour() < 10 ? "0" : ""); // Adiciona zero à esquerda se hora for menor que 10
		Serial.print(adjustedTime.hour());
		Serial.print(":");
		Serial.print(adjustedTime.minute() < 10 ? "0" : ""); // Adiciona zero à esquerda se minuto for menor que 10
		Serial.print(adjustedTime.minute());
		Serial.print(":");
		Serial.print(adjustedTime.second() < 10 ? "0" : ""); // Adiciona zero à esquerda se segundo for menor que 10
		Serial.print(adjustedTime.second());
		Serial.print("\n");

	}
}

// Exibe uma mensagem de inicialização no LCD
void booting() {
	lcd.setCursor(3, 1);
	tone(pinBUZ, 400);
	lcd.print(" KEY-TECH"); // Nome da empresa
	noTone(pinBUZ);
	delay(1000);
	lcd.clear(); // Limpa o LCD
	delay(200);
	slogan(); // Exibe um slogan animado no LCD
}


// Exibe um slogan animado no LCD
void slogan() {
	String line = "Key to Success!";
	for (int i = 0; i < line.length(); i++) {

		//Animação de letras caindo
		lcd.setCursor(i + 1, 0);
		lcd.print(line[i]);
		delay(150);
		lcd.setCursor(i + 1, 0);
		lcd.print(" ");
		lcd.setCursor(i + 1, 1);
		lcd.print(line[i]);

		// Caso o char não seja um blankspace, ativa o buzzer
		if (!(isWhitespace(line[i]))) {
			tone(pinBUZ, 250);
			delay(150);
			noTone(pinBUZ);
		}
	}

	animacaoChave();
}

//Animação da chave pós "Key to success!"
void animacaoChave() {
	chave1();
	chave2();
	chave3();
	chave4();
}

void chave1() {
	byte name0x5[] = { B01111, B11000, B10111, B10101, B10101, B10111, B11000, B01111 };
	byte name0x6[] = { B10000, B11000, B01111, B00000, B01111, B01000, B11000, B10000 };
	byte name0x7[] = { B00000, B00000, B11111, B00000, B11110, B00010, B00011, B00000 };
	byte name0x8[] = { B00000, B00000, B11111, B00000, B11110, B10010, B10010, B00011 };
	byte name0x9[] = { B00000, B00000, B11110, B00010, B11100, B10000, B10000, B10000 };

	lcd.begin(16, 2);

	lcd.createChar(0, name0x5);
	lcd.setCursor(5, 0);
	lcd.write(0);

	lcd.createChar(1, name0x6);
	lcd.setCursor(6, 0);
	lcd.write(1);

	lcd.createChar(2, name0x7);
	lcd.setCursor(7, 0);
	lcd.write(2);

	lcd.createChar(3, name0x8);
	lcd.setCursor(8, 0);
	lcd.write(3);

	lcd.createChar(4, name0x9);
	lcd.setCursor(9, 0);
	lcd.write(4);
}
void chave2() {
	byte name0x5[] = { B01111, B11000, B10111, B10101, B10111, B11000, B01111, B00000 };
	byte name0x6[] = { B10000, B11000, B01111, B00000, B01111, B11000, B10000, B00000 };
	byte name0x7[] = { B00000, B00000, B11111, B00000, B11110, B00011, B00000, B00000 };
	byte name0x8[] = { B00000, B00000, B11111, B00000, B11110, B10010, B00011, B00000 };
	byte name0x9[] = { B00000, B00000, B11110, B00010, B11100, B10000, B10000, B00000 };

	lcd.begin(16, 2);

	lcd.createChar(0, name0x5);
	lcd.setCursor(5, 0);
	lcd.write(0);

	lcd.createChar(1, name0x6);
	lcd.setCursor(6, 0);
	lcd.write(1);

	lcd.createChar(2, name0x7);
	lcd.setCursor(7, 0);
	lcd.write(2);

	lcd.createChar(3, name0x8);
	lcd.setCursor(8, 0);
	lcd.write(3);

	lcd.createChar(4, name0x9);
	lcd.setCursor(9, 0);
	lcd.write(4);
}
void chave3() {
	byte name0x5[] = { B00000, B01111, B10101, B10111, B10000, B01111, B00000, B00000 };
	byte name0x6[] = { B00000, B10000, B11111, B00000, B11111, B10000, B00000, B00000 };
	byte name0x7[] = { B00000, B00000, B11111, B00000, B11111, B00000, B00000, B00000 };
	byte name0x8[] = { B00000, B00000, B11111, B00000, B11110, B00011, B00000, B00000 };
	byte name0x9[] = { B00000, B00000, B11110, B00010, B11100, B10000, B00000, B00000 };

	lcd.begin(16, 2);

	lcd.createChar(0, name0x5);
	lcd.setCursor(5, 0);
	lcd.write(0);

	lcd.createChar(1, name0x6);
	lcd.setCursor(6, 0);
	lcd.write(1);

	lcd.createChar(2, name0x7);
	lcd.setCursor(7, 0);
	lcd.write(2);

	lcd.createChar(3, name0x8);
	lcd.setCursor(8, 0);
	lcd.write(3);

	lcd.createChar(4, name0x9);
	lcd.setCursor(9, 0);
	lcd.write(4);
}
void chave4() {
	byte name0x5[] = { B00000, B01111, B10111, B10000, B01111, B00000, B00000, B00000 };
	byte name0x6[] = { B00000, B10000, B01111, B11111, B10000, B00000, B00000, B00000 };
	byte name0x7[] = { B00000, B00000, B11111, B11111, B00000, B00000, B00000, B00000 };
	byte name0x8[] = { B00000, B00000, B11111, B11111, B00000, B00000, B00000, B00000 };
	byte name0x9[] = { B00000, B00000, B11111, B11110, B00000, B00000, B00000, B00000 };

	lcd.begin(16, 2);

	lcd.createChar(0, name0x5);
	lcd.setCursor(5, 0);
	lcd.write(0);

	lcd.createChar(1, name0x6);
	lcd.setCursor(6, 0);
	lcd.write(1);

	lcd.createChar(2, name0x7);
	lcd.setCursor(7, 0);
	lcd.write(2);

	lcd.createChar(3, name0x8);
	lcd.setCursor(8, 0);
	lcd.write(3);

	lcd.createChar(4, name0x9);
	lcd.setCursor(9, 0);
	lcd.write(4);
}

// Exibe informações no monitor serial
void serialLog(float temp, float humid, int valorLDR) {
	if (lastAvgTemp > 1) {
		Serial.println("Leitura: " + String(currentIndex + 1));
		Serial.println("Temp: " + String(temp) + "°C");
		Serial.println("Last Avarage Temp: " + String(lastAvgTemp) + "°C");
		Serial.println("Humidity: " + String(humid) + " %");
		Serial.println("Last Avarage Humidity: " + String(lastAvgHumd) + " %");
		Serial.println("Luminosidade: " + String(lightLevel) + "%");
		Serial.println("---");
	}

}

// Calcula a média das últimas 10 leituras e exibe as telas de cada campo
void tenthRead() {
	float sumTemp = 0;
	float sumHumd = 0;

	for (int i = 0; i < 10; i++) {
		sumTemp += tempReadings[i];
		sumHumd += humdReadings[i];
	}

	lastAvgTemp = sumTemp / 10;
	lastAvgHumd = sumHumd / 10;

	show_data();
}

// Exibe dados no LCD
void show_data() {
	show_temp();
	delay(3000);
	show_humid();
	delay(3000);
	show_light();
}

// Exibe a temperatura no LCD
void show_temp() {
	if ((lastAvgTemp < 15 || lastAvgTemp > 25) && lastAvgTemp != 0) {
		digitalWrite(ledYel, HIGH);
		digitalWrite(ledGre, LOW);
		tone(pinBUZ, 500);
	}
	lcd.clear(); // Limpa o LCD
	lcd.setCursor(0, 0);
	lcd.print("Temp: " + String(lastAvgTemp, 1) + " C");
	term_image(lastAvgTemp); // Exibe uma imagem correspondente à temperatura no LCD
}

// Exibe a umidade no LCD
void show_humid() {
	if ((lastAvgHumd < 30 || lastAvgHumd > 50) && lastAvgHumd != 0) {
		digitalWrite(ledRed, HIGH);
		digitalWrite(ledGre, LOW);
		tone(pinBUZ, 400);
	}
	lcd.clear(); // Limpa o LCD
	lcd.setCursor(0, 0);
	lcd.print("Humid: " + String(lastAvgHumd, 1) + " %");
	humid_image(lastAvgHumd);
}

// Exibe a luminosidade no LCD
void show_light() {
	lcd.clear(); // Limpa o LCD
	lcd.setCursor(0, 0);
	lcd.print("Nivel Luz: " + String(lightLevel) + "%");
	light_image();
}

// Define e exibe uma imagem correspondente à temperatura no LCD
void term_image(float temp) {
	byte termLow2[8] = { B01010, B01010, B01010, B01010, B10001, B10001, B10001, B01110 };
	byte termLow1[8] = { B00100, B01010, B01010, B01010, B01010, B01010, B01010, B01010 };

	byte termMid2[8] = { B01010, B01010, B01110, B01110, B11111, B11111, B11111, B01110 };
	byte termMid1[8] = { B00100, B01010, B01010, B01010, B01010, B01010, B01010, B01010 };

	byte termHig2[8] = { B01110, B01110, B01110, B01110, B11111, B11111, B11111, B01110 };
	byte termHig1[8] = { B00100, B01110, B01110, B01110, B01110, B01110, B01110, B01110 };

	// Cria os caracteres personalizados e os exibe no LCD, dependendo da temperatura
	if (temp < 15) {
		lcd.createChar(9, termLow1);
		lcd.createChar(10, termLow2);
		messageTemplate("Temp. Baixa");

	}
	else if (temp > 25) {
		lcd.createChar(9, termHig1);
		lcd.createChar(10, termHig2);
		messageTemplate("Temp. Alta");

	}
	else { // Temperatura ideal
		lcd.createChar(9, termMid1);
		lcd.createChar(10, termMid2);
		messageTemplate("Temp. OK");
	}
	move_image(14, 0, 9); // Move e exibe a primeira imagem no LCD
	move_image(14, 1, 10); // Move e exibe a segunda imagem no LCD
}

// Define e exibe uma imagem correspondente à umidade no LCD
void humid_image(float humid) {
	byte humidLow_1[8] = { B00111, B00011, B00001, B00000, B00000, B00000, B00000, B00000 };
	byte humidLow_2[8] = { B00000, B00000, B00000, B00000, B00001, B00001, B00011, B00111 };
	byte humidLow_3[8] = { B00000, B00000, B00000, B00000, B10000, B10000, B01000, B11100 };
	byte humidLow_4[8] = { B11100, B11000, B10000, B00000, B00000, B00000, B00000, B00000 };

	byte humidMid_1[8] = { B01111, B01111, B00111, B00011, B00000, B00000, B00000, B00000 };
	byte humidMid_2[8] = { B00000, B00000, B00001, B00001, B00011, B00111, B00111, B01111 };
	byte humidMid_3[8] = { B00000, B00000, B10000, B10000, B01000, B00100, B10100, B11110 };
	byte humidMid_4[8] = { B11110, B11110, B11100, B11000, B00000, B00000, B00000, B00000 };


	byte humidHigh_1[8] = { B11111, B11111, B11111, B01111, B00111, B00011, B00000, B00000 };
	byte humidHigh_2[8] = { B00011, B00111, B01111, B01111, B11111, B11111, B11111, B11111 };
	byte humidHigh_3[8] = { B11000, B10100, B10010, B10001, B10001, B11001, B11111, B11111 };
	byte humidHigh_4[8] = { B11111, B11111, B11111, B11110, B11100, B11000, B00000, B00000 };

	if (humid < 30) {
		lcd.createChar(11, humidLow_1);
		lcd.createChar(12, humidLow_2);
		lcd.createChar(13, humidLow_3);
		lcd.createChar(14, humidLow_4);

		messageTemplate("Humid. Baixa");

	}
	else if (humid > 50) {
		lcd.createChar(11, humidHigh_1);
		lcd.createChar(12, humidHigh_2);
		lcd.createChar(13, humidHigh_3);
		lcd.createChar(14, humidHigh_4);

		messageTemplate("Humid. Alta");

	}
	else {
		lcd.createChar(11, humidMid_1);
		lcd.createChar(12, humidMid_2);
		lcd.createChar(13, humidMid_3);
		lcd.createChar(14, humidMid_4);

		messageTemplate("Humid. OK");
	}

	move_image(14, 1, 11);
	move_image(14, 0, 12);
	move_image(15, 0, 13);
	move_image(15, 1, 14);
}

//Define e exibe uma imagem correspondente ao nível de luz no LDR
void light_image() {
	byte lampLow1[8] = { B00100, B00100, B00100, B00100, B01110, B01110, B11111, B10001 };
	byte lampLow2[8] = { B10001, B10001, B10001, B01110, B00000, B00000, B00000, B00000 };

	byte lampMid1[8] = { B00100, B00100, B00100, B00100, B01110, B01110, B11111, B10001 };
	byte lampMid2[8] = { B10001, B11111, B11111, B01110, B00000, B00000, B00000, B00000 };

	byte lampHigh1[8] = { B00100, B00100, B00100, B00100, B01110, B01110, B11111, B11111 };
	byte lampHigh2[8] = { B11111, B11111, B11111, B01110, B00000, B00000, B00000, B00000 };

	if (lightLevel >= 30 && lightLevel < 80) {
		lcd.createChar(10, lampLow1);
		lcd.createChar(11, lampLow2);
		messageTemplate("Meia-Luz");

	}
	else if (lightLevel >= 80) {
		lcd.createChar(10, lampHigh1);
		lcd.createChar(11, lampHigh2);
		messageTemplate("Muito Claro");

	}
	else {
		lcd.createChar(10, lampMid1);
		lcd.createChar(11, lampMid2);
		messageTemplate("Escuro");
	}

	move_image(15, 0, 10);
	move_image(15, 1, 11);
}

// Verifica o nível de luz do ambiente
void verifyLDR() {
	// Em um ambiente muito claro, liga a luz vermelha
	if (lightLevel >= 80) {
		digitalWrite(ledRed, HIGH);
		digitalWrite(ledGre, LOW);

		//Caso a temperatura tenha sido normalizada, desliga a luz de alerta da temperatura e seu som no buzzer
		if (lastAvgTemp >= 15 && lastAvgTemp <= 25) {
			digitalWrite(ledYel, LOW);
			noTone(pinBUZ);
		}
		tone(pinBUZ, 600);

		// Em um ambiente um pouco claro, liga a luz amarela
	}
	else if (lightLevel < 0 && lightLevel >= 30) {
		digitalWrite(ledYel, HIGH);
		digitalWrite(ledGre, LOW);

		//Caso a umidade tenha sido normalizada, desliga a luz de alerta da umidade e seu som no buzzer
		if (lastAvgHumd >= 30 && lastAvgHumd <= 50) {
			digitalWrite(ledRed, LOW);
			noTone(pinBUZ);
		}
		tone(pinBUZ, 400);
	}
	else {
		// Caso todos os dados estejam normalizados e o ambiente esteja escuro, desliga todos os LEDs acesos, desliga o buzzer e liga o LED verde
		if (lastAvgHumd >= 30 && lastAvgHumd <= 50 && lastAvgTemp >= 15 && lastAvgTemp <= 25) {
			digitalWrite(ledGre, HIGH);
			digitalWrite(ledRed, LOW);
			digitalWrite(ledYel, LOW);
			noTone(pinBUZ);
		}

	}
}

// Move uma imagem no LCD
void move_image(int x, int y, int byt) {
	lcd.setCursor(x, y); // Define a posição do cursor no LCD
	lcd.write(byte(byt)); // Exibe o caractere personalizado no LCD
}

void messageTemplate(String msg) {
	lcd.setCursor(0, 1);
	lcd.print(msg);
}

void getNextAddress() {
	if (currentAddress + recordSize < endAddress) {
		currentAddress += recordSize; // Avança para o próximo registro
	}
	else {
		Serial.println("EEPROM cheia! Não gravando novos dados.");
		// Você pode implementar uma lógica para apagar os dados antigos ou parar a gravação.
	}
}

void printEEPROM(int start, int end) {
	Serial.println("Conteúdo da EEPROM:");
	Serial.println("Timestamp\t\tTemp (°C)\tUmidade (%)");

	for (int address = start; address < end; address += recordSize) {
		long timeStamp;
		int tempInt, humiInt;

		// Ler dados da EEPROM
		EEPROM.get(address, timeStamp);
		EEPROM.get(address + 4, tempInt);
		EEPROM.get(address + 6, humiInt);

		// Converter os valores
		float temp = tempInt / 100.0;
		float humid = humiInt / 100.0;

		// Verificar se os dados são válidos antes de imprimir
		if (timeStamp != 0xFFFFFFFF && timeStamp != 0 && temp > -50 && temp < 100 && humid >= 0 && humid <= 100) {
			DateTime dt = DateTime(timeStamp); // NÃO aplicar UTC_OFFSET aqui!

			Serial.print(dt.timestamp(DateTime::TIMESTAMP_FULL));
			Serial.print("\t");
			Serial.print(temp);
			Serial.print("°C\t\t");
			Serial.print(humid);
			Serial.println("%");
		}
	}
}


void get_log() {
	static unsigned long lastLogTime = 0;
	unsigned long currentMillis = millis();

	if (currentMillis - lastLogTime >= logInterval) {
		lastLogTime = currentMillis;

		Serial.println("Data stored in EEPROM:");
		Serial.println("Timestamp\t\tTemp\tHumid");

		for (int address = startAddress; address < endAddress; address += recordSize) {
			long timeStamp;
			int tempInt, humiInt;

			EEPROM.get(address, timeStamp);
			EEPROM.get(address + 4, tempInt);
			EEPROM.get(address + 6, humiInt);

			float temp = tempInt / 100.0;
			float humid = humiInt / 100.0;

			if (timeStamp != 0xFFFFFFFF) {
				DateTime dt = DateTime(timeStamp);
				Serial.print(dt.timestamp(DateTime::TIMESTAMP_FULL));
				Serial.print("\t");
				Serial.print(temp);
				Serial.print(" C\t\t");
				Serial.print(humid);
				Serial.println(" %");
			}
		}
	}
}
