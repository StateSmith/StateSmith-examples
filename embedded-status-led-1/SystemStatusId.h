#pragma once

enum SystemStatusId
{
    SystemStatusId_BOOT1,      // fast ramp up
    SystemStatusId_BOOT2,      // medium ramp up
    SystemStatusId_RUNNING_OK, // triangle slow
    SystemStatusId_WARNING,    // ramp down, pause
    SystemStatusId_ERROR,      // fast blip, pause
};
