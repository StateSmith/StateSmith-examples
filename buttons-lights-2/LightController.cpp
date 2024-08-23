#include "LightController.h"
#include <Arduino.h>
#include "ButtonSm.h"
#include "LightSm.h"


///////////////////////////////////////////// CONSTANTS /////////////////////////////////////////////

#define LED_BLUE    7
#define LED_YELLOW  6
#define LED_RED     5

#define BUTTON_DIM  11
#define BUTTON_INC  10


///////////////////////////////////////////// PROTOTYPES /////////////////////////////////////////////

static void handle_dim_button_events(void);
static void handle_inc_button_events(void);
static void run_button(ButtonSm * button, bool input_active, uint32_t elapsed_time_ms);


///////////////////////////////////////////// LOCAL VARS /////////////////////////////////////////////

static ButtonSm button_dim;
static ButtonSm button_inc;
static LightSm light_sm;
static LightSm_StateId last_state_id = LightSm_StateId_ROOT; // so we can detect state changes

////////////////////////////////////////// PUBLIC FUNCTIONS //////////////////////////////////////////

void LightController_setup()
{
    pinMode(LED_BLUE, OUTPUT);
    pinMode(LED_YELLOW, OUTPUT);
    pinMode(LED_RED, OUTPUT);

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

    if (last_state_id != light_sm.state_id)
    {
        last_state_id = light_sm.state_id;
        Serial.print(F("Light controller state changed to: "));
        Serial.println(LightSm_state_id_to_string(light_sm.state_id));
    }
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
        Serial.println(F("Handling event DIM"));
        LightSm_dispatch_event(&light_sm, LightSm_EventId_DIM);
        button_dim.vars.output_press_event = 0;
    }

    if (button_dim.vars.output_long_event)
    {
        Serial.println(F("Handling event DIM_LONG"));
        LightSm_dispatch_event(&light_sm, LightSm_EventId_DIM_LONG);
        button_dim.vars.output_long_event = 0;
    }
}

static void handle_inc_button_events(void)
{
    if (button_inc.vars.output_press_event)
    {
        Serial.println(F("Handling event INC"));
        LightSm_dispatch_event(&light_sm, LightSm_EventId_INC);
        button_inc.vars.output_press_event = 0;
    }

    if (button_inc.vars.output_long_event)
    {
        Serial.println(F("Handling event INC_LONG"));
        LightSm_dispatch_event(&light_sm, LightSm_EventId_INC_LONG);
        button_inc.vars.output_long_event = 0;
    }
}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////// STATE MACHINE SECTION ////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Use this section to define things you want to provide for the state machine.
// Declare variables, functions, etc. that you want to use in the state machine.
// The beauty of this is that you are using regular C code and can do anything you want.
// You don't need to learn special StateSmith expansions or anything like that.

static void light_off()
{
    digitalWrite(LED_BLUE, LOW);
    digitalWrite(LED_YELLOW, LOW);
    digitalWrite(LED_RED, LOW);
}

static void light_1()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, LOW);
    digitalWrite(LED_RED, LOW);
}

static void light_2()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, HIGH);
    digitalWrite(LED_RED, LOW);
}

static void light_3()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, HIGH);
    digitalWrite(LED_RED, HIGH);
}


//------------------------------------------------------------------------------
// !!!!!!!!!!!!!!!!!! STATE MACHINE INCLUDE HERE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//------------------------------------------------------------------------------
// This would typically happen at the bottom of the file.

#include "LightSm.inc.h"
