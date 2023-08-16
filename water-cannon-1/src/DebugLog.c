#include "DebugLog.h"
#include <stdio.h>
#include <stdarg.h>
#include <stdbool.h>

static bool info_enabled = false;
static bool warn_enabled = true;

void DebugLog_info(const char* format, ...)
{
    if (!info_enabled)
        return;

    va_list args;
    va_start(args, format);
    printf(">>> LOG INFO: ");
    vprintf(format, args);   // TODO error checking
    va_end(args);
}

void DebugLog_warn(const char* format, ...)
{
    if (!warn_enabled)
        return;

    va_list args;
    va_start(args, format);
    printf(">>> LOG WARN: ");
    vprintf(format, args);   // TODO error checking
    va_end(args);
}
