#pragma once
#include <stdint.h>

#ifdef __cplusplus
extern "C" {
#endif

void LedControl_set_as_output(uint8_t pin);
void LedControl_set_duty(uint8_t pin, uint8_t duty);

#ifdef __cplusplus
}
#endif