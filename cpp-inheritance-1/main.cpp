#include "LightController.h"
#include "test.h"

#include <iostream>
#include <string>

////////////////////////////////////////////////////////////////////////////////
// prototypes
////////////////////////////////////////////////////////////////////////////////

static void readInputRun(Light::LightController& controller);
static char readCharFromLine(void);


////////////////////////////////////////////////////////////////////////////////
// functions
////////////////////////////////////////////////////////////////////////////////


int main(void)
{
    std::cout << "---------------------------------------\n";
    Test::test1();
    Test::test2();
    std::cout << "Tests all passed!\n";

    Light::LightController light_controller(std::cout);

    std::cout << "---------------------------------------\n\n";
    std::cout << "USAGE:\n";
    std::cout << "  type i <enter> to send INCREASE event to state machine.\n";
    std::cout << "  type d <enter> to send DIM event to state machine.\n";
    std::cout << "  type o <enter> to send OFF event to state machine.\n";
    std::cout << "\n";
    std::cout << "Hit <enter> to start\n";

    readCharFromLine();

    // initialize light controller
    light_controller.init();

    while (true)
    {
        readInputRun(light_controller);
    }

    return 0;
}

static void readInputRun(Light::LightController& controller)
{
    bool isValidInput = true;

    std::cout << "\n";
    controller.printState();
    std::cout << "Please type 'i', 'd', 'o': ";

    char c = readCharFromLine();
    switch (c)
    {
        case 'i': controller.commandIncrease(); break;
        case 'd': controller.commandDim(); break;
        case 'o': controller.commandOff(); break;
        default: isValidInput = false; break;
    }

    if (!isValidInput)
    {
        std::cout << "Invalid input. Ignoring.\n";
    }
}

static char readCharFromLine(void)
{
    std::string line;
    std::getline(std::cin, line);

    if (line.length() == 0)
    {
        return '\0';
    }

    return line[0];
}
