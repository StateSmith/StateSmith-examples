#include <stdio.h>
#include <stdbool.h>
#include "WaterCannon.h"
#include "WaterCannonSm.h"
#include "DebugLog.h"

static char read_char_from_line(void);
static void read_input_dispatch_event(void);

int main(void)
{
    printf("USAGE:\n");
    printf("  Type 'o'<ENTER>, or <ENTER> for `OK_PRESS` event.\n");
    printf("  Type 'b'<ENTER> for `BACK_PRESS` event.\n");
    printf("  Type 'c'<ENTER> for `CAL_PRESS` event (calibration).\n");
    printf("  Type 'a'<ENTER> for `AUTO_PRESS` event.\n");

    WaterCannon_init();

    while (1)
    {
        read_input_dispatch_event();
    }

    return 0;
}

static void read_input_dispatch_event(void)
{
    // Function static var
    static WaterCannonSm_StateId prev_state_id = WaterCannonSm_StateId_ROOT; // not required. just for optional logging.

    int event_id = -1;  // initialize to an invalid value
    char c = read_char_from_line();

    switch (c)
    {
        case '\r':
        case '\n':
        case 'o': event_id = WaterCannonSm_EventId_OK_PRESS;  break;
        case 'b': event_id = WaterCannonSm_EventId_BACK_PRESS;  break;
        case 'c': event_id = WaterCannonSm_EventId_CAL_PRESS;  break;
        case 'a': event_id = WaterCannonSm_EventId_AUTO_PRESS;  break;
    }

    if (event_id == -1)
    {
        DebugLog_warn("Bad input: `%c`", c);
    }
    else
    {
        DebugLog_info("Sending `%s` event to sm", WaterCannonSm_event_id_to_string(event_id));

        WaterCannon_handle_event(event_id);

        // get current state id
        const WaterCannonSm_StateId cur_state_id = WaterCannon_get_current_state();

        // log some info. not required.
        if (prev_state_id != cur_state_id)
        {
            DebugLog_info("State changed from `%s` to `%s`", WaterCannonSm_state_id_to_string(prev_state_id), WaterCannonSm_state_id_to_string(cur_state_id));
            prev_state_id = cur_state_id;
        }
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


