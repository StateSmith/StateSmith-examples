#include <stdio.h>
#include <stdbool.h>
#include "WaterCannon.h"
#include <stdarg.h> // for debug_log

static char read_char_from_line(void);
static void read_input_dispatch_event(void);
static void debug_log(const char *format, ...);


int main(void)
{
    printf("USAGE:\n  Type 'o'<ENTER> for `OK_PRESS` event.\n TODO finish here.\n\n");

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
        case 'o': event_id = WaterCannonSm_EventId_OK_PRESS;  break;
        case 'b': event_id = WaterCannonSm_EventId_BACK_PRESS;  break;
        case 'c': event_id = WaterCannonSm_EventId_CAL_PRESS;  break;
        case 'a': event_id = WaterCannonSm_EventId_AUTO_PRESS;  break;
    }

    if (event_id == -1)
    {
        debug_log("Bad input: `%c`\n", c);
    }
    else
    {
        debug_log("Sending `%s` event to sm\n", WaterCannonSm_event_id_to_string(event_id));

        WaterCannon_handle_event((enum WaterCannonSm_EventId)event_id); // cast OK because of test above

        // get current state id
        const WaterCannonSm_StateId cur_state_id = WaterCannon_get_current_state();

        // log some info. not required.
        if (prev_state_id != cur_state_id)
        {
            debug_log("State changed from `%s` to `%s`\n", WaterCannonSm_state_id_to_string(prev_state_id), WaterCannonSm_state_id_to_string(cur_state_id));
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

static void debug_log(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    printf("  LOG: ");
    vprintf(format, args);   // TODO error checking
    va_end(args);
}
