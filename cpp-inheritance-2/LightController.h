#pragma once
#include "LightSm.h"
#include <ostream>

namespace Light
{

// Handwritten class that implements virtual methods required by state machine.
// Note non-public inheritance to hide the state machine details from main.cpp.
class LightController : private LightSm
{

// public interface to main.c
public:
    LightController(std::ostream& out_stream)
        : m_out_stream(out_stream)
    {
    }
    
    void init()
    {
        start();
    }

    void commandOff()
    {
        dispatchEvent(EventId::OFF);
    }

    void commandIncrease()
    {
        dispatchEvent(EventId::INC);
    }

    void commandDim()
    {
        dispatchEvent(EventId::DIM);
    }

    void printState()
    {
        m_out_stream << "Current state: " << stateIdToString(stateId) << '\n';
    }

// private members used only by this class:
private:
    std::ostream& m_out_stream;

    // ANSI escape codes for colors
    const std::string RESET = "\033[0m";
    const std::string RED = "\033[0;31m";
    const std::string YELLOW = "\033[0;33m";
    const std::string GREEN = "\033[0;32m";
    const std::string BLUE = "\033[0;34m";

// implement the virtual functions required by state machine:
private:
    void turnOff() override
    {
        m_out_stream << "Light is off\n" << RESET;
    }

    void turnBlue() override
    {
        m_out_stream << BLUE << "Light is blue\n" << RESET;
    }

    void turnYellow() override
    {
        m_out_stream << YELLOW << "Light is yellow\n" << RESET;
    }

    void turnRed() override
    {
        m_out_stream << RED << "Light is red\n" << RESET;
    }

    void printCount() override
    {
        m_out_stream << GREEN << "count var: " << count << '\n' << RESET;
    }
};

} // namespace Light
