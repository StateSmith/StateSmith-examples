#include "Light.hpp"
#include <Arduino.h>


///////////////////////////////////////////// CONSTANTS /////////////////////////////////////////////

#define LED_BLUE    7
#define LED_YELLOW  6
#define LED_RED     5


///////////////////////////////////////////// FUNCTIONS /////////////////////////////////////////////

void Light_setup()
{
    pinMode(LED_BLUE, OUTPUT);
    pinMode(LED_YELLOW, OUTPUT);
    pinMode(LED_RED, OUTPUT);
}

void Light_off()
{
    digitalWrite(LED_BLUE, LOW);
    digitalWrite(LED_YELLOW, LOW);
    digitalWrite(LED_RED, LOW);
}

void Light_1()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, LOW);
    digitalWrite(LED_RED, LOW);
}

void Light_2()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, HIGH);
    digitalWrite(LED_RED, LOW);
}

void Light_3()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, HIGH);
    digitalWrite(LED_RED, HIGH);
}
