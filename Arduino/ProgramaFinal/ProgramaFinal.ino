#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 10
#define RST_PIN 9

int boton = 2;
int EjeX = A0;
int EjeY = A1;

int valorBoton, valorEjeX, valorEjeY;
int buzzPin = 3;
const int onDuration = 10;
const int periodDuration = 2;

unsigned long lastPeriodStart; 
MFRC522 rfid(SS_PIN, RST_PIN); // Instance of the class

MFRC522::MIFARE_Key key; 

// Init array that will store new NUID 
byte nuidPICC[4];

void setup() { 
  Serial.begin(115200);
  SPI.begin(); // Init SPI bus
  rfid.PCD_Init(); // Init MFRC522
  

  for (byte i = 0; i < 6; i++) {
    key.keyByte[i] = 0xFF;
  }
  pinMode(buzzPin, OUTPUT); 
  pinMode(boton, INPUT); 
  pinMode(EjeX, INPUT);
  pinMode(EjeY, INPUT);  
  //Serial.println(F("This code scan the MIFARE Classsic NUID."));
  //Serial.print(F("Using the following key:"));
  //printHex(key.keyByte, MFRC522::MF_KEY_SIZE);
}
 
void loop() {
  rfid.uid.size = 4;
  valorBoton = digitalRead(boton);
  valorEjeX = analogRead(EjeX);
  valorEjeY = analogRead(EjeY);
  
  // Reset the loop if no new card present on the sensor/reader. This saves the entire process when idle.
  if ( ! rfid.PICC_IsNewCardPresent())
  {
    printDec(rfid.uid.uidByte, rfid.uid.size);
    
    Serial.println(valorBoton);
    Serial.println(valorEjeX);
    Serial.println(valorEjeY);
    for (byte i = 0; i < 4; i++) {
      rfid.uid.uidByte[i] = 0;
    }
    return;
  }
  // Verify if the NUID has been readed
  if ( ! rfid.PICC_ReadCardSerial())
    return;

    if (millis() - lastPeriodStart >= periodDuration) {
    lastPeriodStart += periodDuration;
    tone(buzzPin, 550, onDuration);
    }
  //;
}


/**
 * Helper routine to dump a byte array as hex values to Serial. 
 */
void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? "0" : "");
    Serial.print(buffer[i], HEX);
  }
  Serial.println();
}

/**
 * Helper routine to dump a byte array as dec values to Serial.
 */
void printDec(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? "0" : "");
    Serial.print(buffer[i], DEC);
  }
  Serial.println();
}

void beep(){
  for(int i = 0; i<10; i++)
  {
    digitalWrite(buzzPin,HIGH);
    delay(4);
    digitalWrite(buzzPin,LOW);
    delay(4); 
  }
  
}
