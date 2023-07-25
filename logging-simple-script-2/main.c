#include <stdio.h>
#include <stdbool.h>
#include "MySm.h"

static char read_char_from_line(void);
static void read_input_run_state_machine(MySm* sm);

int main(void)
{
    MySm sm;
    MySm_ctor(&sm);  // construct

    printf("USAGE:\n  Type 'n'<ENTER> for `NEXT` event.\n  Type anything else <ENTER> for `RESET` event.\n\n");
    MySm_start(&sm);

    while (1)
    {
        read_input_run_state_machine(&sm);
    }

    return 0;
}

static void read_input_run_state_machine(MySm* sm)
{
    enum MySm_EventId event_id;

    char c = read_char_from_line();
    switch (c)
    {
        case 'n': event_id = MySm_EventId_NEXT;  break;
        default:  event_id = MySm_EventId_RESET; break;
    }

    MySm_StateId prev_state_id = sm->state_id; // used to manually detect when state machine state changes after event dispatch

    printf("Sending `%s` event to sm\n", MySm_event_id_to_string(event_id));
    MySm_dispatch_event(sm, event_id);
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
