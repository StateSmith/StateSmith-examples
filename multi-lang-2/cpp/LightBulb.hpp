#pragma once
#include <iostream>
#include <stdint.h> // for count var

class LightBulb
{
public:
    uint16_t count;

    void set(bool desired_status)
    {
        if (!desired_status)
        {
            std::cout << BLUE << "Light is: OFF\n" << RESET;
        }
        else
        {
            std::cout << YELLOW << "Light is: ON\n" << RESET;
        }
    }

private:
    // ANSI escape codes for colors
    const std::string RESET = "\033[0m";
    const std::string RED = "\033[0;31m";
    const std::string YELLOW = "\033[0;33m";
    const std::string BLUE = "\033[0;34m";
};
