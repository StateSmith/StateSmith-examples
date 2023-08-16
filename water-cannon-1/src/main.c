#include <stdio.h>
#include <stdbool.h>
#include "WaterCannonSm.h"

static char read_char_from_line(void);
static void read_input_run_state_machine(WaterCannonSm* sm);

int main(void)
{
    WaterCannonSm sm;
    WaterCannonSm_ctor(&sm);  // construct

    printf("USAGE:\n  Type 'o'<ENTER> for `OK_PRESS` event.\n TODO finish here.\n\n");
    WaterCannonSm_start(&sm);

    printf("Starting in state `%s`\n", WaterCannonSm_state_id_to_string(sm.state_id));

    while (1)
    {
        read_input_run_state_machine(&sm);
    }

    return 0;
}

static void read_input_run_state_machine(WaterCannonSm* sm)
{
    enum WaterCannonSm_EventId event_id;

    char c = read_char_from_line();
    switch (c)
    {
        case 'o': event_id = WaterCannonSm_EventId_OK_PRESS;  break;
        // default:  event_id = WaterCannonSm_EventId_RESET; break;
    }

    WaterCannonSm_StateId prev_state_id = sm->state_id; // used to manually detect when state machine state changes after event dispatch

    printf("Sending `%s` event to sm\n", WaterCannonSm_event_id_to_string(event_id));
    WaterCannonSm_dispatch_event(sm, event_id);

    if (prev_state_id != sm->state_id)
    {
        printf("State changed from `%s` to `%s`\n", WaterCannonSm_state_id_to_string(prev_state_id), WaterCannonSm_state_id_to_string(sm->state_id));
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