// This is a fake Lcd.c file for testing purposes

#include "Lcd.h"
#include <stdarg.h>

char Lcd_header[48];
char Lcd_sub[48];

void Lcd_header(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    vsnprintf(&Lcd_header, sizeof(Lcd_header), format, args);   // TODO error checking
    va_end(args);
}

void Lcd_sub(const char *format, ...)
{
    va_list args;
    va_start(args, format);
    vsnprintf(&Lcd_sub, sizeof(Lcd_sub), format, args);   // TODO error checking
    va_end(args);
}