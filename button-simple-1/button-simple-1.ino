#include <assert.h>
#include "ButtonSm.h"


////////////////////////////////////////////////////////////////////////////////
// global vars
////////////////////////////////////////////////////////////////////////////////

// button state machines
static ButtonSm left_button;
static ButtonSm right_button;

// Millisecond time when loop() was ran last.
// This is used to calculate the elapsed time between loops.
// Tracking this value here instead of inside of each button state machine
// allows us to save some RAM. Important if you have lots of buttons.
// You could easily add this to the button state machine if you prefer.
static uint32_t last_loop_ms = 0;


////////////////////////////////////////////////////////////////////////////////
// functions
////////////////////////////////////////////////////////////////////////////////

void setup()
{
  Serial.begin(115200);
  Serial.println(F("Keyboard keys left/right have a binding to switches above when simulation pane has focus."));
  Serial.println(F("NOTE! up and down buttons are not yet wired up."));
  Serial.println(F("See if you can modify the code to make them work!"));
  Serial.println();

  setup_button(&left_button, 7);
  setup_button(&right_button, 4);
}


/**
 * Setup the button state machine and pin.
 */
static void setup_button(ButtonSm * button_sm, uint8_t pin)
{
  pinMode(pin, INPUT_PULLUP);

  ButtonSm_ctor(button_sm);  // construct button state machine
  button_sm->vars.pin = pin;
  ButtonSm_start(button_sm);
}


void loop()
{
  // calculate milliseconds since the last time our loop ran.
  const uint32_t now_ms = millis();
  const uint32_t elapsed_time_ms = now_ms - last_loop_ms;
  last_loop_ms = now_ms;

  // update buttons
  update_button(&left_button, elapsed_time_ms);
  update_button(&right_button, elapsed_time_ms);

  // check for and print button events
  print_button_events(&left_button, "left");
  print_button_events(&right_button, "right");

  delay(10);  // a small delay eases simulation CPU usage.
}


/**
 * Reads button pin, updates button state machine and timer.
 */
static void update_button(ButtonSm * button_sm, uint32_t elapsed_time_ms)
{
  // read pin status and set input to state machine
  const uint8_t pin = button_sm->vars.pin;
  button_sm->vars.input_active = (digitalRead(pin) == LOW);

  // update button timer
  button_sm->vars.timer_ms += elapsed_time_ms;

  // run state machine
  ButtonSm_dispatch_event(button_sm, ButtonSm_EventId_DO);
}


/**
 * Checks if a button event occurred, clears the event and prints it to serial.
 */
static void print_button_events(ButtonSm * sm, const char *button_name)
{
  if (sm->vars.output_press_event)
  {
    sm->vars.output_press_event = false;
    print_button_event(button_name, "press");
  }

  if (sm->vars.output_long_event)
  {
    sm->vars.output_long_event = false;
    print_button_event(button_name, "long");
  }

  if (sm->vars.output_repeat_event)
  {
    sm->vars.output_repeat_event = false;
    print_button_event(button_name, "repeat");
  }

  if (sm->vars.output_release_event)
  {
    sm->vars.output_release_event = false;
    print_button_event(button_name, "release");
    Serial.println();
  }
}


/**
 * Simply prints button and event to serial.
 */
static void print_button_event(const char *button_name, const char *event)
{
  Serial.print(button_name);
  Serial.print(": ");
  Serial.print(event);
  Serial.println("");
}
