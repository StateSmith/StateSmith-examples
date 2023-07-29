#include "Leds.h"
#include "driver/gpio.h"

#define LED_RED   GPIO_NUM_5
#define LED_GREEN GPIO_NUM_2
#define LED_BLUE  GPIO_NUM_4


void Leds_init(void)
{
    Leds_set(0b000);

    gpio_set_direction(LED_RED,   GPIO_MODE_OUTPUT);
    gpio_set_direction(LED_GREEN, GPIO_MODE_OUTPUT);
    gpio_set_direction(LED_BLUE,  GPIO_MODE_OUTPUT);
}

static void set_gpio(uint8_t gpio_num, uint8_t value)
{
    if (value)
    {
        gpio_set_level(gpio_num, 1);
    }
    else
    {
        gpio_set_level(gpio_num, 0);
    }
}

void Leds_set(uint8_t led_bits)
{
    set_gpio(LED_GREEN, led_bits & 0x01);
    set_gpio(LED_BLUE,  led_bits & 0x02);
    set_gpio(LED_RED,   led_bits & 0x04);
}
