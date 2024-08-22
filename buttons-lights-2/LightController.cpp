#include "LightController.hpp"
#include <Arduino.h>
#include "ButtonSm.hpp"
#include "LightSm.hpp"
#include "Light.hpp"

///////////////////////////////////////////// CONSTANTS /////////////////////////////////////////////

static const int BUTTON_DIM = 11;
static const int BUTTON_INC = 10;


///////////////////////////////////////////// PROTOTYPES /////////////////////////////////////////////

static void handle_dim_button_events(void);
static void handle_inc_button_events(void);
static void run_button(ButtonSm * button, bool input_active, uint32_t elapsed_time_ms);


///////////////////////////////////////////// LOCAL VARS /////////////////////////////////////////////

static ButtonSm button_dim;
static ButtonSm button_inc;
static LightSm light_sm;


///////////////////////////////////////////// FUNCTIONS /////////////////////////////////////////////

void LightController_setup()
{
    Light_setup();

    pinMode(BUTTON_DIM, INPUT_PULLUP);
    pinMode(BUTTON_INC, INPUT_PULLUP);

    // Initialize state machines
    ButtonSm_ctor(&button_dim);
    ButtonSm_ctor(&button_inc);
    LightSm_ctor(&light_sm);

    // Start state machines
    ButtonSm_start(&button_dim);
    ButtonSm_start(&button_inc);
    LightSm_start(&light_sm);
}

void LightController_update(uint32_t elapsed_time_ms)
{
    run_button(&button_dim, digitalRead(BUTTON_DIM) == LOW, elapsed_time_ms);
    run_button(&button_inc, digitalRead(BUTTON_INC) == LOW, elapsed_time_ms);

    handle_dim_button_events();
    handle_inc_button_events();
}


///////////////////////////////////////////// PRIVATE FUNCTIONS /////////////////////////////////////////////

static void run_button(ButtonSm * button, bool input_active, uint32_t elapsed_time_ms)
{
    button->vars.input_active = input_active;
    ButtonSm_dispatch_event(button, ButtonSm_EventId_DO);
    button->vars.timer_ms += elapsed_time_ms;
}

static void handle_dim_button_events(void)
{
    if (button_dim.vars.output_press_event)
    {
        Serial.println("handling event DIM");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_DIM);
        button_dim.vars.output_press_event = 0;
    }

    if (button_dim.vars.output_long_event)
    {
        Serial.println("handling event DIM_LONG");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_DIM_LONG);
        button_dim.vars.output_long_event = 0;
    }
}

static void handle_inc_button_events(void)
{
    if (button_inc.vars.output_press_event)
    {
        Serial.println("handling event INC");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_INC);
        button_inc.vars.output_press_event = 0;
    }

    // if (button_inc.vars.output_long_event)
    // {
    //     Serial.println("handling event INC_LONG");
    //     LightSm_dispatch_event(&light_sm, LightSm_EventId_INC_LONG);
    //     button_inc.vars.output_long_event = 0;
    // }
}
