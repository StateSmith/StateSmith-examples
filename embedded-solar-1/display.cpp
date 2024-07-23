#include "display.h"
#include <LiquidCrystal.h>

static LiquidCrystal lcd(12, 11, 10, 9, 8, 7);

void display_init()
{
  lcd.begin(16, 2);
}

void show_splash()
{
  lcd.clear();
  lcd.print(F("Solar Charger"));
  lcd.setCursor(0, 1);
  lcd.print(F("version 2.4.0"));
}

void show_home()
{
  lcd.clear();
  lcd.print(F("STATUS: charging"));
  lcd.setCursor(0, 1);
  lcd.print(F("BATTERY: 80%"));
}

void show_main_menu1()
{
  lcd.clear();
  lcd.print(F("MAIN MENU 1/2"));
  lcd.setCursor(0, 1);
  lcd.print(F("> solar stats"));
}

void show_main_menu2()
{
  lcd.clear();
  lcd.print(F("MAIN MENU 2/2"));
  lcd.setCursor(0, 1);
  lcd.print(F("> battery stats"));
}

void show_solar_stats1()
{
  lcd.clear();
  lcd.print(F("SOLAR STATS 1/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("voltage: 14.5"));
}

void show_solar_stats2()
{
  lcd.clear();
  lcd.print(F("SOLAR STATS 2/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("amperage: 1.3"));
}

void show_solar_stats3()
{
  lcd.clear();
  lcd.print(F("SOLAR STATS 3/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("time: 3h 16min"));
}

void show_battery_stats1()
{
  lcd.clear();
  lcd.print(F("BATTERY STAT 1/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("voltage: 13.1"));
}

void show_battery_stats2()
{
  lcd.clear();
  lcd.print(F("BATTERY STAT 2/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("amperage: -1.3"));
}

void show_battery_stats3()
{
  lcd.clear();
  lcd.print(F("BATTERY STAT 3/3"));
  lcd.setCursor(0, 1);
  lcd.print(F("amp hours: 35.1"));
}
