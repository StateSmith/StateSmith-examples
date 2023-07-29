// License MIT: https://opensource.org/licenses/MIT
// Copyright Adam Fraser-Kruck
// From StateSmith examples https://github.com/StateSmith/
// Quickly manually tested. May one day add unit tests.
#pragma once
#include <stdint.h>
#include <stdbool.h>

/**
 * @brief A timer that can be used to track intervals between events.
 * You can use any time units T you want, like ms, seconds, etc.
 */
struct IntervalTimer
{
    /// @brief The desired interval in time units T.
    uint32_t _interval;

    /// @brief Time (units T) since boot when last interval occurred.
    uint32_t _last_interval_time;
};

/**
 * @brief Initializes the timer.
 * 
 * @param timer 
 * @param interval units T
 * @param current_time units T
 * @return true on success, false on fail
 */
bool IntervalTimer_init(struct IntervalTimer * timer, uint32_t interval, uint32_t current_time);

/**
 * @brief Updates the timer and checks if the interval has passed.
 * 
 * @param timer 
 * @param event_pending [out] set to true if the interval has passed since last call to this function.
 * @param current_time [in] units T
 * @return uint32_t time (units T) to wait until next call to this function. You can call earlier safely.
 */
uint32_t IntervalTimer_update(struct IntervalTimer * timer, bool* event_pending, uint32_t current_time);
