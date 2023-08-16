// This is a fake Display file for testing purposes.
// It captures the output of Display_header() and Display_sub() in two global variables.
#include "FakeDisplay.hpp"
#include <stdarg.h>

std::string FakeDisplay::header;
std::string FakeDisplay::sub;


void Display_header(const char *format, ...)
{
    char buf[48];

    va_list args;
    va_start(args, format);
    vsnprintf(buf, sizeof(buf), format, args);   // TODO error checking
    va_end(args);

    FakeDisplay::header = buf;
}

void Display_sub(const char *format, ...)
{
    char buf[48];

    va_list args;
    va_start(args, format);
    vsnprintf(buf, sizeof(buf), format, args);   // TODO error checking
    va_end(args);

    FakeDisplay::sub = buf;
}
