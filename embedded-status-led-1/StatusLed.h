#pragma once
#include <stdint.h>
#include "SystemStatusId.h"

#ifdef __cplusplus
extern "C" {
#endif

void StatusLed_init(uint8_t led_pin);
void StatusLed_iterate(uint32_t cur_time_ms, enum SystemStatusId status_id);

#ifdef __cplusplus
}
#endif
