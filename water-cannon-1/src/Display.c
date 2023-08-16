// The display could be an LCD, a serial port, or a console window.
// In this case, it is a console window for ease of use.
#include "Display.h"
#include <stdarg.h>
#include <stdio.h>

void Display_header(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    printf("\n\n");
    vprintf(format, args);   // TODO error checking
    va_end(args);
}

void Display_sub(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    vprintf(format, args);   // TODO error checking
    va_end(args);
}

