#include <iostream>
#include <string>

#include "LightSm.h"

////////////////////////////////////////////////////////////////////////////////
// global vars
////////////////////////////////////////////////////////////////////////////////

static LightSm g_state_machine;


////////////////////////////////////////////////////////////////////////////////
// prototypes
////////////////////////////////////////////////////////////////////////////////

static void read_input_run_state_machine(void);
static char read_char_from_line(void);


////////////////////////////////////////////////////////////////////////////////
// functions
////////////////////////////////////////////////////////////////////////////////

int main(void)
{
    std::cout << "---------------------------------------\n\n";
    std::cout << "USAGE:\n";
    std::cout << "  type i <enter> to send INCREASE event to state machine.\n";
    std::cout << "  type d <enter> to send DIM event to state machine.\n";
    std::cout << "  type o <enter> to send OFF event to state machine.\n";
    std::cout << "\n";
    std::cout << "Hit <enter> to start\n";

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

    char c = read_char_from_line();
    switch (c)
    {
        case 'i': event_id = LightSm_EventId_INCREASE; break;
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
        std::cout << "What you trying to pull!? Bad input.\n";
    }
}

static char read_char_from_line(void)
{
    std::string line;
    std::getline(std::cin, line);

    if (line.length() == 0)
    {
        return '\0';
    }

    return line[0];
}
