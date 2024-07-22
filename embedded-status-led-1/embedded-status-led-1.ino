#include "StatusLed.h"
#include "SystemStatusId.h"
#include "LedControl.h"

#define LED_PIN 11

enum SystemStatusId _status = SystemStatusId_BOOT1;


void setup() {
    StatusLed_init(LED_PIN);
    Serial.begin(115200);
}


void loop() {
    StatusLed_iterate(millis(), _status);
    delay(15);

    if (Serial.available()) {
        int read_byte = Serial.read();
        bool valid = true;

        switch(read_byte) {
            case '1': _status = SystemStatusId_BOOT1; break;
            case '2': _status = SystemStatusId_BOOT2; break;
            case 'r': _status = SystemStatusId_RUNNING_OK; break;
            case 'w': _status = SystemStatusId_WARNING; break;
            case 'e': _status = SystemStatusId_ERROR; break;

            default:
                valid = false;
                break;
        }

        if (valid) {
            Serial.print("Status: ");
            Serial.println(_status);
        }
    }
}
