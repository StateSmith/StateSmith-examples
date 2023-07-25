#include "Leds.h"
#include <stdio.h>

void Leds_set(uint8_t led_bits)
{
    const int LED_COUNT = 3;

    printf("LEDS: ");
    for (int i = 0; i < LED_COUNT; i++)
    {
        int led_state = led_bits & 0b1;
        printf("%i", led_state);
        led_bits >>= 1;
    }
    
    printf("\n");
}
