#include "LedControl.h"
#include <Arduino.h>

void LedControl_set_as_output(uint8_t pin)
{
    pinMode(pin, OUTPUT);
}

void LedControl_set_duty(uint8_t pin, uint8_t duty)
{
    analogWrite(pin, duty);
}