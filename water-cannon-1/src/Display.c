#include "Display.h"
#include "Lcd.h"

static void heading(const char * const heading)
{
    Lcd_header("====== %s ======", heading);
}

static void sub(const char * const message)
{
    printf("%s\n", message);
}

void Display_init(void)
{

}

void Display_show_splash(void)
{
    heading("Screen: Splash");
}

void Display_show_auto(void)
{
    heading("Screen: Auto");
}

void Display_show_cal_required(void)
{
    heading("Screen: Cal Required");
}

void Display_show_cal_lower(void)
{
    heading("Screen: Cal Lower");
    sub("Lower Cannon to bottom position then press OK");
}

void Display_show_cal_raise(void)
{
    heading("Screen: Cal Raise");
    sub("Raise Cannon to top position then press OK");
}

void Display_show_cal_done(void)
{
    heading("Screen: Cal Done");
    sub("Calibration complete");
}

void Display_show_cal_cancelled(void)
{
    heading("Screen: Cal Cancelled");
    sub("Calibration cancelled");
}



