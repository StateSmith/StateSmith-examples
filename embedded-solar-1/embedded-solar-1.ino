#include <assert.h>
#include "display.h"
#include "SolarUiSm.h"
#include <Bounce2.h>


////////////////////////////////////////////////////////////////////////////////
// global vars
////////////////////////////////////////////////////////////////////////////////

static SolarUiSm solar_ui_sm;

static Bounce2::Button left_button = Bounce2::Button();
static Bounce2::Button right_button = Bounce2::Button();
static Bounce2::Button up_button = Bounce2::Button();
static Bounce2::Button down_button = Bounce2::Button();

////////////////////////////////////////////////////////////////////////////////
// functions
////////////////////////////////////////////////////////////////////////////////

void setup()
{
  Serial.begin(115200);
  Serial.println(F("Keyboard arrow keys control buttons when simulation pane has focus."));

  display_init();

  // left button
  left_button.attach(5, INPUT_PULLUP);
  left_button.interval(5);
  left_button.setPressedState(LOW);
  // right button
  right_button.attach(2, INPUT_PULLUP);
  right_button.interval(5);
  right_button.setPressedState(LOW);
  // up button
  up_button.attach(4, INPUT_PULLUP);
  up_button.interval(5);
  up_button.setPressedState(LOW);
  // down button
  down_button.attach(3, INPUT_PULLUP);
  down_button.interval(5);
  down_button.setPressedState(LOW);

  SolarUiSm_ctor(&solar_ui_sm);
  SolarUiSm_start(&solar_ui_sm);
}

void loop()
{
  // update buttons
  left_button.update();
  right_button.update();
  up_button.update();
  down_button.update();

  // dispatch button events to state machine

  if (left_button.pressed()) // true if button state changed  to pressed
  {
    SolarUiSm_dispatch_event(&solar_ui_sm, SolarUiSm_EventId_LEFT);
  }

  if (right_button.pressed()) // true if button state changed  to pressed
  {
    SolarUiSm_dispatch_event(&solar_ui_sm, SolarUiSm_EventId_RIGHT);
  }

  if (up_button.pressed()) // true if button state changed  to pressed
  {
    SolarUiSm_dispatch_event(&solar_ui_sm, SolarUiSm_EventId_UP);
  }

  if (down_button.pressed()) // true if button state changed  to pressed
  {
    SolarUiSm_dispatch_event(&solar_ui_sm, SolarUiSm_EventId_DOWN);
  }

  delay(10);  // a small delay eases simulation CPU usage.
}
