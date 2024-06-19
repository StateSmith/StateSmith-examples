// hand written file
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <time.h>
#include "LightController.h"


//------------------------------------------------------------------------------
// prototypes
//------------------------------------------------------------------------------

static char read_char_from_line(void);
static void read_input_run_state_machine(void);
static void print_usage(void);


//------------------------------------------------------------------------------
// functions
//------------------------------------------------------------------------------

int main(void)
{
    printf("---------------------------------------\n\n");
    print_usage();

    printf("Hit <enter> to start\n");
    read_char_from_line();

    // setup and start light controller
    LightController_init();

    while (true)
    {
        read_input_run_state_machine();
    }

    return 0;
}

static void print_usage(void)
{
    printf("USAGE:\n");
    printf("  type i <enter> to send INCREASE event to state machine.\n");
    printf("  type d <enter> to send DIM event to state machine.\n");
    printf("  type t <enter> to send TOGGLE event to state machine.\n");
    printf("\n");
}

static void read_input_run_state_machine(void)
{
    char c = read_char_from_line();
    switch (c)
    {
        case 'i': LightController_inc_press();    break;
        case 'd': LightController_dim_press();    break;
        case 't': LightController_toggle_press(); break;

        default:  
        printf("ERROR: unknown input '%c'\n", c); 
        print_usage();
        break;
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
