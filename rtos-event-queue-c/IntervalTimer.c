// License MIT: https://opensource.org/licenses/MIT
// Copyright Adam Fraser-Kruck
// From StateSmith examples https://github.com/StateSmith/
// Quickly manually tested. May one day add unit tests.
#include "IntervalTimer.h"

/**
 * @brief Initializes the timer.
 * 
 * @param timer 
 * @param interval units T
 * @param current_time units T
 * @return true on success, false on fail
 */
bool IntervalTimer_init(struct IntervalTimer * timer, uint32_t interval, uint32_t current_time)
{
    if (interval == 0)
    {
        timer->_interval = UINT32_MAX;
        return false;
    }

    timer->_interval = interval;
    timer->_last_interval_time = current_time;
    return true;    
}

/**
 * @brief Updates the timer and checks if the interval has passed.
 * 
 * @param timer 
 * @param event_pending [out] set to true if the interval has passed since last call to this function.
 * @param current_time [in] units T
 * @return uint32_t time (units T) to wait until next call to this function. You can call earlier safely.
 */
uint32_t IntervalTimer_update(struct IntervalTimer * timer, bool* event_pending, uint32_t current_time) {

    const uint32_t time_since_event = current_time - timer->_last_interval_time;
    const uint32_t interval = timer->_interval;
    
    uint32_t time_to_next_interval;

    if (time_since_event < interval)
    {
        // Not enough time has passed since the last event.
        *event_pending = false;
        time_to_next_interval = interval - time_since_event;
    }
    else
    {
        // We know that `time_since_event >= interval`.
        // It is past time to dispatch event.
        *event_pending = true;
        const uint32_t time_past_interval = (time_since_event % interval); // we use modulus to avoid events piling up if RTOS was busy and missed a few intervals.
        time_to_next_interval = interval - time_past_interval;
        timer->_last_interval_time = current_time - time_past_interval;
    }

    return time_to_next_interval;
}
