// hand written file
#include "LightController.h"
#include "LightSm.h"
#include <stdio.h>

static LightSm state_machine;
static uint32_t time_ms;

void LightController_init(void)
{
    LightSm_ctor(&state_machine);
    LightSm_start(&state_machine);
}

void LightController_step(uint32_t time_in_ms)
{
    time_ms = time_in_ms;
    LightSm_dispatch_event(&state_machine, LightSm_EventId_DO);
}

void LightController_key_press(void)
{
    LightSm_dispatch_event(&state_machine, LightSm_EventId_KEY_PRESS);
}



//////////////////////////////////////////// STATE MACHINE SECTION ////////////////////////////////////////////

// Use this section to define things you want to provide for the state machine.
// The beauty of this is that you are using regular C code and can do anything you want.
// You don't need to learn special StateSmith expansions or anything like that.

//------------------------------------------------------------------------------
// defines for the state machine
//------------------------------------------------------------------------------

#define TIMEOUT 1000
#define FAST_TIMEOUT 250


//------------------------------------------------------------------------------
// private vars for the state machine
//------------------------------------------------------------------------------

static int count = 0;
static uint32_t timer_started_at_ms;


//------------------------------------------------------------------------------
// functions for the state machine
//------------------------------------------------------------------------------

static void reset_timer(void)
{
    timer_started_at_ms = time_ms;
}

static uint32_t timer(void)
{
    return time_ms - timer_started_at_ms;
}

static void light_off(void)
{
    printf("[___]\n");
}

static void light_1(void)
{
    printf("[#__]\n");
}

static void light_12(void)
{
    printf("[##_]\n");
}

static void light_123(void)
{
    printf("[###]\n");
}

// i = 0 (off) to 3 (brightest)
static void light_i(uint8_t i)
{
    printf("[");
    for (int j = 1; j < i; j++)
    {
        printf("_");
    }

    if (i > 0)
    {
        printf("#");
    }

    for (int j = i+1; j <= 3; j++)
    {
        printf("_");
    }
    printf("]\n");
}


//------------------------------------------------------------------------------
// !!!!!!!!!!!!!!!!!! STATE MACHINE INCLUDE HERE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//------------------------------------------------------------------------------
// This should happen at bottom of file.
#include "LightSm.inc"
