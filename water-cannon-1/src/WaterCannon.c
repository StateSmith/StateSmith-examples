#include "WaterCannon.h"
#include "WaterCannonSm.h"
#include "DebugLog.h"

static WaterCannonSm sm;
static bool is_calibrated = false;

void WaterCannon_init()
{
    // normally not needed, but required for unit tests as they run multiple times.
    // sometimes helpful to wrap all vars in a struct and then memset to zero instead of having a bunch of loose vars.
    is_calibrated = false;

    WaterCannonSm_ctor(&sm);
    WaterCannonSm_start(&sm);
}

void WaterCannon_handle_event(int event)
{
    // state machine requires a valid event, so we make sure to check here.
    if (event < 0 || event >= WaterCannonSm_EventIdCount)
    {
        DebugLog_warn("Invalid event: %d", event);
        return;
    }

    WaterCannonSm_dispatch_event(&sm, (WaterCannonSm_EventId)event);
}

bool WaterCannon_is_calibrated(void)
{
    return is_calibrated;
}

void WaterCannon_capture_lowered_position(void)
{
    // do stuff... would normally store height in a variable
    is_calibrated = false;
    DebugLog_info("Calibration started...");
}

void WaterCannon_capture_raised_position(void)
{
    // do stuff... would normally store height in a variable
    is_calibrated = true;
    DebugLog_info("Calibration finished!");
}

int WaterCannon_get_current_state(void)
{
    return sm.state_id;
}

void WaterCannon_enable_auto_stuff(void)
{
    // do stuff
}

