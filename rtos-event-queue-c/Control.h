#ifdef __cplusplus
extern "C" {
#endif

#pragma once
#include <stdbool.h>
#include "MySm.h"

/**
 * @brief Initialize the control module.
 */
bool Control_init();

/**
 * @brief Queue an event to be processed by the control module. Thread safe.
 * @param event_id 
 */
void Control_queue_event_ts(enum MySm_EventId event_id);

#ifdef __cplusplus
}
#endif