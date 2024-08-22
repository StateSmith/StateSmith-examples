#include <stdint.h>
#include "LightController.hpp"

// Millisecond time when loop() was ran last.
// This is used to calculate the elapsed time between loops.
// Tracking this value here instead of inside of each button state machine
// allows us to save some RAM. Important if you have lots of buttons.
// You could easily add this to the button state machine if you prefer.
static uint32_t last_loop_ms = 0;

void setup() {
  Serial.begin(115200);
  Serial.println("starting!");
  LightController_setup();
}

void loop() {
  // calculate milliseconds since the last time our loop ran.
  const uint32_t now_ms = millis();
  const uint32_t elapsed_time_ms = now_ms - last_loop_ms;
  last_loop_ms = now_ms;

  LightController_update(elapsed_time_ms);
  delay(5);   // decrease wokwi simulation CPU usage
}
