# Key-Tech_PBL_EC5
Repositório criado para o Project-Based Learning do 5º semestre do curso de Engenharia da Computação da Faculdade Engenheiro Salvador Arena.

## Projeto de Display Animado com Arduino

Este projeto tem como objetivo a criação de um dispositivo dedicado ao monitoramento de condições ambientais em ambientes controlados, com foco em temperatura, umidade relativa do ar e luminosidade. O sistema inclui um **data logger**, responsável por registrar e armazenar os dados coletados desses parâmetros. Além disso, o projeto envolve uma animação personalizada exibida em um display de caracteres, controlada por um microcontrolador Arduino UNO. Para nos auxiliar com a animação, foi utilizado o **Chareditor**, um editor que facilita a criação de personagens personalizados para o Arduino. O circuito do dispositivo integra sensores para monitoramento ambiental, componentes embarcados para a gravação dos dados e a exibição de informações em tempo real. Abaixo, apresentamos a estrutura, requisitos e configurações necessárias para o funcionamento do sistema.

---

## 📋 Componentes do Projeto e suas descrições

| Componente       | Descrição                                                                                              |
|------------------|--------------------------------------------------------------------------------------------------------|
| **ATMEGA 328P**  | Microcontrolador responsável pelo processamento e controle das operações do Arduino.                   |
| **EEPROM**       | Conexão para alimentação do Arduino e comunicação serial com o PC.                                     |
| **RTC**          | Relógio em tempo real, permitindo a marcação de dados com timestamps exatos.                           |
| **LCD I2C**      | Display LCD com comunicação I2C, utilizado para exibir dados na tela (interface).                      |
| **LEDs**         | LEDs usados para indicar o status das condições do dispositivo, como funcionamento normal ou alertas.  |
| **Buzzer**       | Componente responsável por emitir alertas audíveis para notificar o usuário sobre eventos específicos. |
| **DHT11**        | Sensor de temperatura e umidade (DHT11) usado para medir e monitorar as condições ambientais.          |
| **LDR**          | Sensor de luminosidade (LDR), usado para medir e monitorar as condições ambientais.                    |

• Protoboard, jumpers, resistores e o botão completam os componentes e foram necessários para que todo o projeto acontecesse.

---

## 🛠 Configuração do Ambiente

### Software Necessário

1. **Arduino IDE**: Para upload do código ao Arduino UNO ([Download](https://www.arduino.cc/en/software)).
2. **Chareditor**: Ferramenta para criação de animações em displays de caracteres ([Documentação](https://example.com/chareditor-docs)).
3. **Bibliotecas**:
   - `DHT Sensor Library`: usada para ler dados de sensores DHT, como o DHT11 e DHT22, que medem temperatura e umidade. Ela fornece funções para facilitar a leitura dos valores dos sensores e sua conversão para unidades compreensíveis.
   - `LiquidCrystal I2C`: utilizada para facilitar a comunicação com displays LCD que utilizam o protocolo I2C. Ela permite controlar a exibição de informações no display com comandos simples, economizando pinos no Arduino.
   - `RTClib`: destinada ao controle de módulos de Relógio de Tempo Real (RTC), como o DS3231. Ela permite ler e configurar a data e hora atual, além de fornecer funções para armazenar e recuperar esses valores com precisão.

### 💧 Unidades de medida e precisão dos sensores

#### ========= DHT11 (umidade e temperatura) =========

**Temperatura**:
   - Unidade de medida: °C (Celsius)
   - Precisão: ±2°C

**Umidade**:
   - Unidade de medida: % de umidade relativa
   - Precisão: ±5% de umidade relativa

#### ======= LDR (Light Dependent Resistor) =======

**Luminosidade**:
   - Unidade de medida: Lux (lx), que representa a intensidade luminosa
   - Precisão: A precisão do LDR depende da qualidade e características específicas do resistor, mas geralmente o LDR oferece uma resposta linear dentro de um intervalo de intensidade luminosa, embora a precisão possa variar dependendo do circuito utilizado para medições.

---

## 🚩 Manual de uso

• **Ligar o Sistema**:
   - Conecte o Arduino UNO à fonte de alimentação.
   - O display LCD I2C será inicializado, exibindo uma mensagem de boas-vindas.

• **Exibição de Dados no LCD**:
   O display LCD mostrará informações em tempo real sobre:
   - Temperatura (em °C)
   - Umidade (em %)
   - Luminosidade (em Lux)
   - Data e Hora (usando o módulo RTC)

Esses dados serão atualizados a cada intervalo programado, com timestamps precisos.

• **Indicadores de Status**:

   **Alertas visuais - LEDs**:
   - LED Verde: Indica que o sistema está operando normalmente.
   - LED Amarelo: Aparece em caso de alerta, como valores que estão próximos a ficar fora da faixa desejada.
   - LED Vermelho: Aparece em caso de erro, como valores fora da faixa desejada.

   **Alertas Sonoros - Buzzer**:
   - O buzzer emitirá um alerta sonoro se algum parâmetro monitorado estiver fora dos limites pré-configurados (por exemplo, se a temperatura estiver muito alta ou baixa, ou se a umidade estiver fora da faixa ideal).

Segue tabela demonstrativa de como o sistema deve se comportar em cada situação:

| Situação                           | LED Vermelho   | LED Amarelo   | LED Verde   | Buzzer   |
|------------------------------------|----------------|---------------|-------------|----------|
| Temperatura < 15°C ou > 25°C       | ✕              | ✔            | ✕           | 500 Hz   |
| Umidade < 30% ou > 50%             | ✔              | ✕            | ✕           | 400 Hz   |
| Luminosidade muito alta (>= 80)    | ✔              | ✕            | ✕           | 600 Hz   |
| Luminosidade moderada (48-80)      | ✕              | ✔            | ✕           | 400 Hz   |
| Tudo normal                        | ✕              | ✕            | ✔           | Desligado|

• **Input do sistema (botão de ação)**:
   - O botão, quando pressionado, traz os dados que foram gravados no EEPRON. Dados esses que são apresentados no próprio monitor serial.

# Resumo

• **Armazenamento dos Dados (Data Logger)**:
   O sistema armazena os dados coletados na EEPROM. Os dados são registrados com timestamps e podem ser acessados para posterior análise.

• **Desligamento**:
   O sistema pode ser desligado com segurança desconectando a alimentação do Arduino.

---

## Manutenção e Cuidados

- **Calibração do Sensor**: Os sensores, como o DHT11 e o LDR, podem exigir calibração periódica para garantir a precisão das leituras.
- **Limpeza**: Mantenha os sensores e o display LCD limpos para garantir o bom funcionamento. Use um pano seco para limpar.
- **Substituição do Buzzer/LEDs**: Se o buzzer ou os LEDs pararem de funcionar, verifique as conexões e substitua-os, se necessário.

### Solução de Problemas

- **O display LCD não acende**:
   - Verifique se a alimentação do Arduino está correta.
   - Certifique-se de que o cabo I2C está corretamente conectado ao display.

- **Os dados não estão sendo atualizados**:
   - Verifique o código no Arduino para garantir que as leituras dos sensores estão sendo feitas corretamente.

- **Alertas Sonoros constantes**:
   - Verifique se os parâmetros configurados estão dentro da faixa desejada, caso contrário, ajuste-os.

### Instalação e demonstração

- **Link do vídeo demonstrativo no Youtube:**
- https://www.youtube.com/watch?v=nOgnBDDfZSc

```bash
# Clonar repositório (substitua com seu link)
git clone https://github.com/zaccheu/Key-Tech_PBL_EC5.git