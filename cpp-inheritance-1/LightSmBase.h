#pragma once
#include <stdint.h> // for count var

namespace Light
{

/**
 * This base class is mostly virtual.
 * You could put implementations here, but I wanted to show a situation
 * where the user derived class implements that logic using its own private members.
 * 
 * This base class also makes it easy to test the state machine.
 * 
 * This base class may not be needed once custom state machine constructors are supported.
 * https://github.com/StateSmith/StateSmith/issues/443
 */
class LightSmBase
{
public:
    virtual ~LightSmBase() = default;

    uint16_t count;

    virtual void turnOff() = 0;
    virtual void turnBlue() = 0;
    virtual void turnYellow() = 0;
    virtual void turnRed() = 0;
    virtual void printCount() = 0;
};

} // namespace Light
