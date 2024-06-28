// hand written file
#include "LightController.h"
// #include "LightSm.h"  // TODO uncomment when LightSm is ready
#include <stdio.h>

// spell-checker:ignore leds

//------------------------------------------------------------------------------
// prototypes
//------------------------------------------------------------------------------

static void leds_off(void);
static void leds_1(void);
static void leds_12(void);
static void leds_123(void);


//------------------------------------------------------------------------------
// module private vars
//------------------------------------------------------------------------------

// static LightSm state_machine;  // TODO uncomment when LightSm is ready


//------------------------------------------------------------------------------
// public functions
//------------------------------------------------------------------------------

void LightController_init(void)
{
    // LightSm_ctor(&state_machine);  // TODO uncomment when LightSm is ready
    // LightSm_start(&state_machine);  // TODO uncomment when LightSm is ready
    leds_off();  // TODO remove when LightSm is ready
}

void LightController_inc_press(void)
{
    // LightSm_dispatch_event(&state_machine, LightSm_EventId_INC);  // TODO uncomment when LightSm is ready
    leds_12();  // TODO remove when LightSm is ready
}

void LightController_dim_press(void)
{
    // LightSm_dispatch_event(&state_machine, LightSm_EventId_DIM);  // TODO uncomment when LightSm is ready
    leds_1();  // TODO remove when LightSm is ready
}

void LightController_toggle_press(void)
{
    // LightSm_dispatch_event(&state_machine, LightSm_EventId_TOGGLE);  // TODO uncomment when LightSm is ready
    leds_off();  // TODO remove when LightSm is ready
}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////// STATE MACHINE SECTION ////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Use this section to define things you want to provide for the state machine.
// The beauty of this is that you are using regular C code and can do anything you want.
// You don't need to learn special StateSmith expansions or anything like that.

//------------------------------------------------------------------------------
// private vars for the state machine
//------------------------------------------------------------------------------

static int count = 0;


//------------------------------------------------------------------------------
// functions for the state machine
//------------------------------------------------------------------------------

static void leds_off(void)
{
    printf("[___]\n");
}

static void leds_1(void)
{
    printf("[#__]\n");
}

static void leds_12(void)
{
    printf("[##_]\n");
}

static void leds_123(void)
{
    printf("[###]\n");
}

// i = 0 (off) to 3 (brightest)
static void leds(uint8_t i)
{
    switch (i)
    {
        default:
        case 0: leds_off(); break;
        case 1: leds_1(); break;
        case 2: leds_12(); break;
        case 3: leds_123(); break;
    }
}


//------------------------------------------------------------------------------
// !!!!!!!!!!!!!!!!!! STATE MACHINE INCLUDE HERE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//------------------------------------------------------------------------------
// This should typically happen at bottom of file.
// #include "LightSm.inc"     // TODO uncomment when LightSm is ready
