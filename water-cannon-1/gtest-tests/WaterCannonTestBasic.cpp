#include "gtest/gtest.h"
#include "FakeDisplay.hpp"

extern "C" {
    // Including inside an extern "C" block is not recommended if your production code is a mix of c/c++.
    // Fine if your production code is C only though and you want to use C++ for testing.
    #include "../src/WaterCannon.h"
    #include "../src/WaterCannonSm.h"
}


TEST(Simple, startup)
{
    FakeDisplay::reset();
    WaterCannon_init();

    EXPECT_EQ(FakeDisplay::header, "====== Screen: Splash ======\n");
    EXPECT_EQ(FakeDisplay::sub, "Press OK to continue\n");
    EXPECT_EQ(WaterCannonSm_StateId_SPLASH_SCREEN, WaterCannon_get_current_state());
}

TEST(Simple, startup_to_home)
{
    FakeDisplay::reset();
    WaterCannon_init();

    WaterCannon_handle_event(WaterCannonSm_EventId_OK_PRESS);
    EXPECT_EQ(FakeDisplay::header, "====== Screen: Home ======\n");
    EXPECT_EQ(FakeDisplay::sub, "Press CAL or AUTO\n");
    EXPECT_EQ(WaterCannonSm_StateId_HOME, WaterCannon_get_current_state());
}


TEST(Simple, startup_to_cal_required)
{
    FakeDisplay::reset();
    WaterCannon_init();

    WaterCannon_handle_event(WaterCannonSm_EventId_OK_PRESS);
    WaterCannon_handle_event(WaterCannonSm_EventId_AUTO_PRESS);
    EXPECT_EQ(FakeDisplay::header, "====== Screen: Cal Required ======\n");
    EXPECT_EQ(FakeDisplay::sub, "Press OK to continue\n");
    EXPECT_EQ(WaterCannonSm_StateId_CALIBRATION_REQUIRED, WaterCannon_get_current_state());
}


TEST(Simple, startup_to_cal)
{
    FakeDisplay::reset();
    WaterCannon_init();

    WaterCannon_handle_event(WaterCannonSm_EventId_OK_PRESS);
    WaterCannon_handle_event(WaterCannonSm_EventId_CAL_PRESS);
    EXPECT_EQ(FakeDisplay::header, "====== Screen: Cal Lower ======\n");
    EXPECT_EQ(FakeDisplay::sub, "Lower Cannon to bottom position then press OK\n");
    EXPECT_EQ(WaterCannonSm_StateId_LOWER, WaterCannon_get_current_state());
}

