#include "LightController.hpp"
#include <Arduino.h>
#include "ButtonSm.hpp"
#include "LightSm.hpp"

///////////////////////////////////////////// CONSTANTS /////////////////////////////////////////////

static const int LED_BLUE = 7;
static const int LED_YELLOW = 6;
static const int LED_RED = 5;

static const int BUTTON_DIM = 11;
static const int BUTTON_INC = 10;


///////////////////////////////////////////// PROTOTYPES /////////////////////////////////////////////

static void handle_dim_button_events(void);
static void handle_inc_button_events(void);


///////////////////////////////////////////// LOCAL VARS /////////////////////////////////////////////

static ButtonSm button_dim;
static ButtonSm button_inc;
static LightSm light_sm;


///////////////////////////////////////////// FUNCTIONS /////////////////////////////////////////////

void LightController::setup()
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

void LightController::step()
{
    button_dim.vars.input_is_pressed = digitalRead(BUTTON_DIM) == LOW;
    ButtonSm_dispatch_event(&button_dim, ButtonSm_EventId_DO);
    
    button_inc.vars.input_is_pressed = digitalRead(BUTTON_INC) == LOW;
    ButtonSm_dispatch_event(&button_inc, ButtonSm_EventId_DO);

    handle_dim_button_events();
    handle_inc_button_events();
}

void LightController::light_off()
{
    digitalWrite(LED_BLUE, LOW);
    digitalWrite(LED_YELLOW, LOW);
    digitalWrite(LED_RED, LOW);
}

void LightController::light_1()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, LOW);
    digitalWrite(LED_RED, LOW);
}

void LightController::light_2()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, HIGH);
    digitalWrite(LED_RED, LOW);
}

void LightController::light_3()
{
    digitalWrite(LED_BLUE, HIGH);
    digitalWrite(LED_YELLOW, HIGH);
    digitalWrite(LED_RED, HIGH);
}


static void handle_dim_button_events(void)
{
    if (button_dim.vars.output_event_press)
    {
        Serial.println("handling event DIM");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_DIM);
        button_dim.vars.output_event_press = 0;
    }

    if (button_dim.vars.output_event_long)
    {
        Serial.println("handling event DIM_LONG_PRESS");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_DIM_LONG_PRESS);
        button_dim.vars.output_event_long = 0;
    }
}

static void handle_inc_button_events(void)
{
    if (button_inc.vars.output_event_press)
    {
        Serial.println("handling event INC");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_INC);
        button_inc.vars.output_event_press = 0;
    }

    if (button_inc.vars.output_event_long)
    {
        Serial.println("handling event INC_LONG_PRESS");
        LightSm_dispatch_event(&light_sm, LightSm_EventId_INC_LONG_PRESS);
        button_inc.vars.output_event_long = 0;
    }
}
