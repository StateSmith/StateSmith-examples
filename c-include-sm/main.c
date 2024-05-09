#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include "LightSm.h"    // note sm include

//------------------------------------------------------------------------------
// types
//------------------------------------------------------------------------------

enum LightId {
    LightId_OFF,
    LightId_ON1,
    LightId_ON2,
    LightId_ON3,
};


//------------------------------------------------------------------------------
// private vars
//------------------------------------------------------------------------------

static LightSm g_state_machine; // note sm variable
static enum LightId light_id;   // accessed by sm

//------------------------------------------------------------------------------
// prototypes
//------------------------------------------------------------------------------

static void read_input_run_state_machine(void);
static char read_char_from_line(void);


//------------------------------------------------------------------------------
// functions
//------------------------------------------------------------------------------

int main(void)
{
    printf("---------------------------------------\n\n");

    printf("USAGE:\n");
    printf("  type i <enter> to send INCREASE event to state machine.\n");
    printf("  type d <enter> to send DIM event to state machine.\n");
    printf("  type o <enter> to send OFF event to state machine.\n");
    printf("\n");
    printf("Hit <enter> to start\n");
    read_char_from_line();

    // setup and start state machine
    LightSm_ctor(&g_state_machine);
    LightSm_start(&g_state_machine);

    while (true)
    {
        read_input_run_state_machine();
    }

    return 0;
}


static void read_input_run_state_machine(void)
{
    bool valid_input = true;
    enum LightSm_EventId event_id = LightSm_EventId_OFF;

    printf("\nCurrent state: %i\n", light_id);
    char c = read_char_from_line();
    switch (c)
    {
        case 'i': event_id = LightSm_EventId_INC; break;
        case 'd': event_id = LightSm_EventId_DIM; break;
        case 'o': event_id = LightSm_EventId_OFF; break;
        default: valid_input = false; break;
    }

    if (valid_input)
    {
        LightSm_dispatch_event(&g_state_machine, event_id);
    }
    else
    {
        printf("What you trying to pull!? Bad input.\n");
    }
}

// blocks while waiting for input
static char read_char_from_line(void)
{
    static char s_buf[100];
    char* c_ptr = fgets(s_buf, sizeof(s_buf), stdin);

    if (c_ptr == NULL)
    {
        return '\0';
    }

    return *c_ptr;
}



//#########################################################################################
//#################### STATE MACHINE INTEGRATION SECTION ##################################
//#########################################################################################
// State machine c code is included at the bottom of this file

//------------------------------------------------------------------------------
// includes needed by state machine
//------------------------------------------------------------------------------
// you might choose to put these includes at the top of this file instead
#include <stdint.h>



//------------------------------------------------------------------------------
// #defines for state machine
// these basically replace StateSmith expansions
//------------------------------------------------------------------------------

#define saturate_inc_u8(arg) 

// below macro is like a StateSmith expansion equivalent. You can write `set_id(OFF)` and
// it will expand to `light_id = LightId_OFF`. See diagram usage of it.
#define set_id(postfix)   light_id = CONCAT(LightId_, postfix)
// downside to above macro is that vscode won't find this usage of `light_id` with "Find All References".
// if you wanted that, you could use an actual function to set the `light_id` variable.

// old c preprocessor tricks for concatenating symbols
#define	CONCAT1(x,y)	x ## y
#define	CONCAT(x,y)	    CONCAT1(x, y)


//------------------------------------------------------------------------------
// private vars just for state machine
//------------------------------------------------------------------------------

static uint8_t count;


//------------------------------------------------------------------------------
// private functions for state machine
//------------------------------------------------------------------------------

static void blue(void)
{
    printf("BLUE\n");
}

static void yellow(void)
{
    printf("YELLOW\n");
}

static void red(void)
{
    printf("RED count: %i\n", count);
}


//##############################################################################
// >>> State machine source include <<<
//##############################################################################
// This should happen at bottom of file.
#include "LightSm.inc"
