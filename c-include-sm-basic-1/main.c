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
static uint64_t get_ms(void);


//------------------------------------------------------------------------------
// functions
//------------------------------------------------------------------------------

int main(void)
{
    setup_terminal();
    LightController_init();

    while (true)
    {
        uint64_t time = get_ms();
        LightController_step((uint32_t)time);

        if (is_key_pressed()) {
            LightController_key_press();
        }

        // NOTE! Your control loop would normally want some kind of sleep here to avoid excessive CPU usage.
        // I couldn't get WSL2 to work properly with nanosleep(), so omitting for now.
        // sleep_ms(10);
    }

    return 0;
}



//------------------------------------------------------------------------------
// UNIX only! (Linux, macOS).
// If on Windows, use WSL2 or implement equivalent functions.
//------------------------------------------------------------------------------

static uint64_t get_ms(void)
{
    const uint16_t MILLISECONDS_PER_SECOND = 1000;
    return clock() / (CLOCKS_PER_SEC / MILLISECONDS_PER_SECOND);
}

// this allows the terminal to see if a key was pressed without blocking
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
