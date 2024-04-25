#pragma once
#include <Arduino.h>

class Buttons {
private:


public:
    static void setup()
    {

    }

    void light_1()
    {
        digitalWrite(LED_BUILTIN, HIGH);
    }

    void light_1();
    void turnOff();
    void blink();
};
