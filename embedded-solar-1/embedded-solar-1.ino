#include <assert.h>
#include "SolarUiSm.h"
#include <LiquidCrystal.h>
#include <Bounce2.h>


////////////////////////////////////////////////////////////////////////////////
// global vars
////////////////////////////////////////////////////////////////////////////////

static SolarUiSm solar_ui_sm;

static LiquidCrystal lcd(12, 11, 10, 9, 8, 7);

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

  lcd.begin(16, 2);

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


////////////////////////////////////////////////////////////////////////////////
// methods to be called by state machine

static void show_splash()
{
  lcd.clear();
  lcd.print(F("Solar Charger"));
  lcd.setCursor(0, 1);
  lcd.print(F("version 2.4.0"));
}

static void show_home()
{
  lcd.clear();
  lcd.print(F("STATUS: charging"));
  lcd.setCursor(0, 1);
  lcd.print(F("BATTERY: 80%"));
}

static void show_main_menu1()
{
  lcd.clear();
  lcd.print(F("MAIN MENU 1/2"));
  lcd.setCursor(0, 1);
  lcd.print(F("> solar stats"));
}

static void show_main_menu2()
{
  lcd.clear();
  lcd.print(F("MAIN MENU 2/2"));
  lcd.setCursor(0, 1);
  lcd.print(F("> battery stats"));
}

static void show_solar_stats1()
{
  lcd.clear();
  lcd.print(F("SOLAR STATS 1/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("voltage: 14.5"));
}

static void show_solar_stats2()
{
  lcd.clear();
  lcd.print(F("SOLAR STATS 2/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("amperage: 1.3"));
}

static void show_solar_stats3()
{
  lcd.clear();
  lcd.print(F("SOLAR STATS 3/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("time: 3h 16min"));
}

static void show_battery_stats1()
{
  lcd.clear();
  lcd.print(F("BATTERY STAT 1/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("voltage: 13.1"));
}

static void show_battery_stats2()
{
  lcd.clear();
  lcd.print(F("BATTERY STAT 2/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("amperage: -1.3"));
}

static void show_battery_stats3()
{
  lcd.clear();
  lcd.print(F("BATTERY STAT 3/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("amp hours: 35.1"));
}

/////////////////////////////////////////////////////////////////////////////////////
// state machine source code included here so that it can access the above functions
// Why .hpp file? https://github.com/StateSmith/StateSmith/issues/361
#include "SolarUiSm.hpp"
