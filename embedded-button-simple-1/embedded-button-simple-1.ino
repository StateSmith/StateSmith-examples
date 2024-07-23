#include <assert.h>
#include "ButtonSm.h"

////////////////////////////////////////////////////////////////////////////////
// defines
////////////////////////////////////////////////////////////////////////////////

// https://stackoverflow.com/a/4415646/7331858
#define COUNT_OF(x) ((sizeof(x) / sizeof(0 [x])) / ((size_t)(!(sizeof(x) % sizeof(0 [x])))))

enum ButtonId
{
  BUTTON_LEFT,
  BUTTON_UP,
  BUTTON_DOWN,
  BUTTON_RIGHT,
};

#define BUTTON_COUNT 4


////////////////////////////////////////////////////////////////////////////////
// global vars
////////////////////////////////////////////////////////////////////////////////

// setup input pins for buttons
static const char* g_button_names[] = {"LEFT", "UP", "DOWN", "RIGHT"};
static const uint8_t g_button_pins[] = {7, 6, 5, 4};
static_assert(COUNT_OF(g_button_names) == BUTTON_COUNT, "g_button_names mapping needs updating");
static_assert(COUNT_OF(g_button_pins) == BUTTON_COUNT, "g_button_pins mapping needs updating");

// button state machines
static struct ButtonSm g_buttons[BUTTON_COUNT];


////////////////////////////////////////////////////////////////////////////////
// functions
////////////////////////////////////////////////////////////////////////////////

void setup()
{
  Serial.begin(115200);
  Serial.println("Keyboard keys left, up, down, right have a binding to switches above when simulation pane has focus.");
  Serial.println();

  for (uint8_t i = 0; i < BUTTON_COUNT; i++)
  {
    pinMode(g_button_pins[i], INPUT_PULLUP);
    static_assert(COUNT_OF(g_button_pins) == BUTTON_COUNT, "required for safe array access");

    ButtonSm_ctor(&g_buttons[i]);
    ButtonSm_start(&g_buttons[i]);
    static_assert(COUNT_OF(g_buttons) == BUTTON_COUNT, "required for safe array access");
  }
}

void loop()
{
  const uint8_t loop_delay_ms = 10;  // better: calculate time since last loop.

  for (uint8_t button_index = 0; button_index < BUTTON_COUNT; button_index++)
  {
    ButtonSm &sm = g_buttons[button_index];
    sm.vars.input_active = (digitalRead(g_button_pins[button_index]) == LOW);
    static_assert(COUNT_OF(g_button_pins) == BUTTON_COUNT, "required for safe array access");

    ButtonSm_dispatch_event(&sm, ButtonSm_EventId_DO);

    if (sm.vars.output_press_event)
    {
      sm.vars.output_press_event = false;
      print_button_event(button_index, "press");
    }

    if (sm.vars.output_long_event)
    {
      sm.vars.output_long_event = false;
      print_button_event(button_index, "long");
    }

    if (sm.vars.output_repeat_event)
    {
      sm.vars.output_repeat_event = false;
      print_button_event(button_index, "repeat");
    }

    if (sm.vars.output_release_event)
    {
      sm.vars.output_release_event = false;
      print_button_event(button_index, "release");
      Serial.println();
    }

    sm.vars.timer_ms += loop_delay_ms;
  }

  delay(loop_delay_ms);  // a small delay eases simulation CPU usage.
}

static void print_button_event(uint8_t button_index, const char *event)
{
  const char *button_name;
  
  if (button_index < COUNT_OF(g_button_names))
  {
    button_name = g_button_names[button_index];
  }
  else
  {
    button_name = "UNKNOWN";
  }

  Serial.print(button_name);
  Serial.print(": ");
  Serial.print(event);
  Serial.println("");
}
