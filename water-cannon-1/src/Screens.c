#include "Screens.h"
#include "Display.h"

static void heading(const char * const heading)
{
    Display_header("====== %s ======\n", heading);
}

static void sub(const char * const message)
{
    Display_sub("%s\n", message);
}

void Screens_show_splash(void)
{
    heading("Screen: Splash");
    sub("Press OK to continue");
}

void Screens_show_home(void)
{
    heading("Screen: Home");
    sub("Press CAL or AUTO");
}

void Screens_show_auto(void)
{
    heading("Screen: Auto");
    sub("Searching for water cannon targets...");
}

void Screens_show_cal_required(void)
{
    heading("Screen: Cal Required");
    sub("Press OK to continue");
}

void Screens_show_cal_lower(void)
{
    heading("Screen: Cal Lower");
    sub("Lower Cannon to bottom position then press OK");
}

void Screens_show_cal_raise(void)
{
    heading("Screen: Cal Raise");
    sub("Raise Cannon to top position then press OK");
}

void Screens_show_cal_done(void)
{
    heading("Screen: Calibration Success");
    sub("Press OK to continue");
}

void Screens_show_cal_cancelled(void)
{
    heading("Screen: Cal Cancelled");
    sub("Press OK to continue");
}



