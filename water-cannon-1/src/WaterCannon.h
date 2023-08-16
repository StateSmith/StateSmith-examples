// Note that this file is included by WaterCannonSm.
#pragma once
#include <stdbool.h>

void WaterCannon_init();

// int type used instead of WaterCannonSm_EventId_t to avoid circular dependency.
// could solve a number of ways, but I'm being a bit lazy for this example.
void WaterCannon_handle_event(int event);

// int type used instead of WaterCannonSm_StateId_t to avoid circular dependency.
// could solve a number of ways, but I'm being a bit lazy for this example.
int WaterCannon_get_current_state();

// This function is only needed by state machine. Could be moved to a private header.
bool WaterCannon_is_calibrated(void);

// This function is only needed by state machine. Could be moved to a private header.
void WaterCannon_capture_lowered_position(void);

// This function is only needed by state machine. Could be moved to a private header.
void WaterCannon_capture_raised_position(void);
