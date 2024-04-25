#include "LightController.hpp"

void setup() {
  Serial.begin(115200);
  Serial.println("starting!");
  LightController::setup();
}

void loop() {
  LightController::step();
  delay(5); // needs to be small to detect button presses
}
