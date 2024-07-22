#include "StatusLed.h"
#include "LedControl.h"
#include <stdbool.h>
#include "StatusLedSm.h"

static uint8_t _led_pin;
static uint32_t _cur_time_ms;
static enum SystemStatusId _status_id;

static StatusLedSm _sm;


void StatusLed_init(uint8_t led_pin)
{
    _led_pin = led_pin;
    LedControl_set_as_output(_led_pin);
    
    StatusLedSm_ctor(&_sm);
    StatusLedSm_start(&_sm);
}

void StatusLed_iterate(uint32_t cur_time_ms, enum SystemStatusId status_id)
{
    _cur_time_ms = cur_time_ms;
    _status_id = status_id;

    StatusLedSm_dispatch_event(&_sm, StatusLedSm_EventId_DO);
}

////////////////////////// FSM STUFF BELOW ////////////////////////////

#define FAST 6
#define MEDIUM 4
#define SLOW 2

#define DUTY_MAX 255

#define IS_STATUS(status_name) (_status_id == SystemStatusId_ ## status_name)

static int8_t count;
static bool _ramp_is_done;
static int16_t _ramp_i; // signed to allow for negative values
static int16_t _ramp_bottom = 0;
static uint32_t _timer_start_ms;

static void timer_reset()
{
    _timer_start_ms = _cur_time_ms;
}

static uint32_t timer_get_ms()
{
    return _cur_time_ms - _timer_start_ms;
}

static void set_led_duty(uint8_t duty)
{
    LedControl_set_duty(_led_pin, duty);
}

static void ramp_init(uint8_t starting_duty)
{
    _ramp_is_done = false;
    _ramp_i = starting_duty;
    set_led_duty(_ramp_i);
}

static void ramp_init_up()
{
    ramp_init(_ramp_bottom);
}

static void ramp_init_down()
{
    ramp_init(DUTY_MAX);
}

static bool is_ramp_done()
{
    return _ramp_is_done;
}

static void ramp_up(uint8_t speed)
{
    _ramp_i += speed;
    if (_ramp_i >= 255)
    {
        _ramp_i = 255;
        _ramp_is_done = true;
    }
    set_led_duty(_ramp_i);
}

static void ramp_down(uint8_t speed)
{
    _ramp_i -= speed;
    if (_ramp_i <= _ramp_bottom)
    {
        _ramp_i = _ramp_bottom;
        _ramp_is_done = true;
    }
    set_led_duty(_ramp_i);
}



#include "StatusLedSm.tpp"
