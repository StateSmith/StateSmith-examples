#pragma once
#include "WaterCannonSm.h"
#include <stdbool.h>

void WaterCannon_init();

void WaterCannon_handle_event(WaterCannonSm_EventId event);

WaterCannonSm_StateId WaterCannon_get_current_state();

bool WaterCannon_is_calibrated(void);

void WaterCannon_capture_lowered_position(void);
void WaterCannon_capture_raised_position(void);
