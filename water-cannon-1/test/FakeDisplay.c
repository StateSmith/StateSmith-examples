// This is a fake Display file for testing purposes.
// It captures the output of Display_header() and Display_sub() in two global variables.
#include "Display.h"
#include <stdarg.h>

char FakeDisplay_header[48];
char FakeDisplay_sub[48];

void Display_header(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    vsnprintf(&Lcd_header, sizeof(Lcd_header), format, args);   // TODO error checking
    va_end(args);
}

void Display_sub(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    vsnprintf(&Lcd_sub, sizeof(Lcd_sub), format, args);   // TODO error checking
    va_end(args);
}
