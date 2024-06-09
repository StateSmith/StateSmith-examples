// hand written file
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <time.h>
#include "LightController.h"

// terminal stuff
#include <unistd.h>
#include <termios.h>
#include <fcntl.h>


//------------------------------------------------------------------------------
// prototypes
//------------------------------------------------------------------------------

static void setup_terminal(void);
static bool is_key_pressed(void);


//------------------------------------------------------------------------------
// functions
//------------------------------------------------------------------------------

int main(void)
{
    setup_terminal();
    LightController_init();

    while (true)
    {
        uint64_t time = clock() / (CLOCKS_PER_SEC / 1000);
        LightController_step((uint32_t)time);

        if (is_key_pressed()) {
            LightController_key_press();
        }
    }

    return 0;
}


//------------------------------------------------------------------------------
// terminal functions - UNIX only! (Linux, macOS).
// If on Windows, use WSL2 or change the terminal functions.
//------------------------------------------------------------------------------

static void setup_terminal(void)
{
    struct termios ttystate;

    // Get the terminal state
    tcgetattr(STDIN_FILENO, &ttystate);

    // Turn off canonical mode and echo
    ttystate.c_lflag &= ~(ICANON | ECHO);
    ttystate.c_cc[VMIN] = 1;
    ttystate.c_cc[VTIME] = 0;

    // Set the terminal attributes
    tcsetattr(STDIN_FILENO, TCSANOW, &ttystate);

    // Set the file descriptor to non-blocking mode
    int flags = fcntl(STDIN_FILENO, F_GETFL, 0);
    fcntl(STDIN_FILENO, F_SETFL, flags | O_NONBLOCK);
}

static bool is_key_pressed(void)
{
    char ch;
    return read(STDIN_FILENO, &ch, 1) > 0;
}