#include "gtest/gtest.h"
#include "FakeDisplay.hpp"

extern "C" {
    // Including inside an extern "C" block is not recommended if your production code is a mix of c/c++.
    // Fine if your production code is C only though and you want to use C++ for testing.
    #include "../src/WaterCannon.h"
    #include "../src/WaterCannonSm.h"
}



#define RETURN_VOID_IF_HAS_FATAL_FAILURE()  if (Test::HasFatalFailure()) return
#define RETURN_VOID_IF_HAS_FAILURE()  if (Test::HasFailure()) return

#define ENABLE_TRACE_FUNCTIONS

#ifdef ENABLE_TRACE_FUNCTIONS
#define TRACE_FUNC_ARGS_0()                              SCOPED_TRACE(__func__)
#define TRACE_FUNC_ARGS_1(arg1)                          SCOPED_TRACE(::testing::Message() << __func__ << "(" #arg1 ":" << (arg1) << ")")
#define TRACE_FUNC_ARGS_2(arg1, arg2)                    SCOPED_TRACE(::testing::Message() << __func__ << "(" #arg1 ":" << (arg1) << ", " #arg2 ":" << (arg2) << ")")
#else
#define TRACE_FUNC_ARGS_0()
#define TRACE_FUNC_ARGS_1(arg1)
#define TRACE_FUNC_ARGS_2(arg1, arg2)
#endif




/**
 * Google Test fixture for testing water cannon
 */
class WaterCannonTest : public ::testing::Test {
public:
    virtual ~WaterCannonTest() {}

protected:
    virtual void SetUp() {
        // Code here will be called immediately after the constructor (right before each test).
        FakeDisplay::reset();
        WaterCannon_init();
    }

    void ExpectDisplay(std::string expected_header, std::string expected_sub)
    {
        TRACE_FUNC_ARGS_2(expected_header, expected_sub);

        // if you want test to continue even if there is a failure, use EXPECT_*
        ASSERT_EQ(FakeDisplay::header, expected_header + "\n");
        ASSERT_EQ(FakeDisplay::sub, expected_sub + "\n");
    }

    void ExpectState(WaterCannonSm_StateId state)
    {
        TRACE_FUNC_ARGS_1(state);

        ASSERT_EQ(WaterCannon_get_current_state(), state);
    }

    void SendEvent(WaterCannonSm_EventId event)
    {
        TRACE_FUNC_ARGS_1(event);
        WaterCannon_handle_event(event);
    }

    void StartToSplash()
    {
        TRACE_FUNC_ARGS_0();

        ExpectDisplay("====== Screen: Splash ======", "Press OK to continue");
        ExpectState(WaterCannonSm_StateId_SPLASH_SCREEN);        
    }

    void StartToHome()
    {
        TRACE_FUNC_ARGS_0();

        StartToSplash();
        RETURN_VOID_IF_HAS_FAILURE();

        SendEvent(WaterCannonSm_EventId_OK_PRESS);
        ExpectAtHome();
    }

    void ExpectAtHome()
    {
        TRACE_FUNC_ARGS_0();

        ExpectDisplay("====== Screen: Home ======", "Press CAL or AUTO");
        ExpectState(WaterCannonSm_StateId_HOME);
    }

    void StartToCalRequired()
    {
        TRACE_FUNC_ARGS_0();

        StartToHome();
        RETURN_VOID_IF_HAS_FAILURE();

        SendEvent(WaterCannonSm_EventId_AUTO_PRESS);
        ExpectDisplay("====== Screen: Cal Required ======", "Press OK to continue");
        ExpectState(WaterCannonSm_StateId_CALIBRATION_REQUIRED);
    }

    void StartToCalLower()
    {
        TRACE_FUNC_ARGS_0();

        StartToHome();
        RETURN_VOID_IF_HAS_FAILURE();

        SendEvent(WaterCannonSm_EventId_CAL_PRESS);
        ExpectDisplay("====== Screen: Cal Lower ======", "Lower Cannon to bottom position then press OK");
        ExpectState(WaterCannonSm_StateId_LOWER);
    }
};

// NOTE! we use TEST_F() here instead of TEST() because we are using a fixture
TEST_F(WaterCannonTest, start_to_splash)
{
    StartToSplash();
}

TEST_F(WaterCannonTest, start_to_home)
{
    StartToHome();
}

TEST_F(WaterCannonTest, start_to_cal_required)
{
    StartToCalRequired();
}

TEST_F(WaterCannonTest, start_to_cal_lower)
{
    StartToCalLower();
}

TEST_F(WaterCannonTest, cal_required_back_to_home)
{
    StartToCalRequired();
    RETURN_VOID_IF_HAS_FAILURE();

    SendEvent(WaterCannonSm_EventId_BACK_PRESS);
    ExpectAtHome();
}
