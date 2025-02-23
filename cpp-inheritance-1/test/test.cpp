#include "test.h"
#include "LightSm.h"
#include <string>
#include <iostream>
#include <assert.h>

// Normally you would use a test framework like Google Test.
// But for this simple example, we will just use assert.

namespace Test
{

/**
 * This is a simple class that allows us to test the state machine
 * independently of the LightController class.
 * It logs all methods that are called by the state machine.
 * 
 * Just showing this an option. You could also test the state machine
 * by using the LightController class directly.
 * 
 * Testing the state machine independently can be useful when you have
 * complex state machines, if LightController is difficult to test,
 * or if LightController is being developed by a different team.
 */
class TestLightSm : public Light::LightSm
{
public:
    std::string m_actions_log;

    void turnOff() { m_actions_log.append("turnOff;"); }
    void turnBlue() { m_actions_log.append("turnBlue;"); }
    void turnYellow() { m_actions_log.append("turnYellow;"); }
    void turnRed() { m_actions_log.append("turnRed;"); }
    void printCount() { m_actions_log.append("printCount;"); }

    void assertActionsLog(const char* expected, const char* file, int line)
    {
        if (m_actions_log != expected)
        {
            std::cerr << "Expected actions log: " << expected << '\n';
            std::cerr << "Actual actions log  : " << m_actions_log << '\n';
            std::cerr << "Trace: " << file << ':' << line << '\n';
            std::cerr << "\n";
            assert(false);
        }
    }

    void assertCountVar(uint16_t expected, const char* file, int line)
    {
        if (count != expected)
        {
            std::cerr << "Expected count: " << expected << '\n';
            std::cerr << "Actual count  : " << count << '\n';
            std::cerr << "Trace: " << file << ':' << line << '\n';
            std::cerr << "\n";
            assert(false);
        }
    }

    void clearActionsLog()
    {
        m_actions_log.clear();
    }
};


void test1()
{
    std::cout << "Running test1\n";
    TestLightSm test_sm;

    // should start in off state
    test_sm.start();
    test_sm.assertActionsLog("turnOff;", __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // already in off. sending OFF event should have no effect
    test_sm.dispatchEvent(Light::LightSm::EventId::OFF);
    test_sm.assertActionsLog("", __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send INC event and expect light to turn blue
    test_sm.dispatchEvent(Light::LightSm::EventId::INC);
    test_sm.assertActionsLog("turnBlue;", __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send DIM event and expect back to off
    test_sm.dispatchEvent(Light::LightSm::EventId::DIM);
    test_sm.assertActionsLog("turnOff;", __FILE__, __LINE__);
    test_sm.clearActionsLog();
}


void test2()
{
    std::cout << "Running test2\n";
    TestLightSm test_sm;

    // should start in off state
    test_sm.start();
    test_sm.assertActionsLog("turnOff;", __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send INC event and expect light to turn blue
    test_sm.dispatchEvent(Light::LightSm::EventId::INC);
    test_sm.assertActionsLog("turnBlue;", __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send INC event and expect light to turn yellow
    test_sm.dispatchEvent(Light::LightSm::EventId::INC);
    test_sm.assertActionsLog("turnYellow;", __FILE__, __LINE__);
    test_sm.assertCountVar(0, __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send INC event and expect count++ and printCount
    test_sm.dispatchEvent(Light::LightSm::EventId::INC);
    test_sm.assertActionsLog("printCount;", __FILE__, __LINE__);
    test_sm.assertCountVar(1, __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send INC event and expect count++ and printCount
    test_sm.dispatchEvent(Light::LightSm::EventId::INC);
    test_sm.assertActionsLog("printCount;", __FILE__, __LINE__);
    test_sm.assertCountVar(2, __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send INC event and we should also see the light turn red
    test_sm.dispatchEvent(Light::LightSm::EventId::INC);
    test_sm.assertActionsLog("printCount;turnRed;", __FILE__, __LINE__);
    test_sm.assertCountVar(3, __FILE__, __LINE__);
    test_sm.clearActionsLog();

    // send OFF event and expect light to turn off
    test_sm.dispatchEvent(Light::LightSm::EventId::OFF);
    test_sm.assertActionsLog("turnOff;", __FILE__, __LINE__);
    test_sm.clearActionsLog();
}

} // namespace Test
