#include <stdio.h>

#include "freertos/FreeRTOS.h"
#include "freertos/queue.h"
#include "freertos/task.h"

#include "Control.h"


//////////////////////// PROTOTYPES ////////////////////////

static void input_iterate(void);
static void input_task(void *_unused);
static char read_char_blocking(void);


//////////////////////// FUNCTIONS ////////////////////////

// extern "C" required for ESP-IDF and wokwi/arduino
extern "C" void app_main() {
    printf("\n\nUSAGE: Type 'n'<ENTER> for `NEXT` event, 'r'<ENTER> for `RESET` event.\n");
    printf("Experiment with sending multiple events in a row like 'nnrrnn'<ENTER>.\n");
    printf("Type 'n'<ENTER> to start.\n\n");
    while (read_char_blocking() != 'n') { }

    Control_init();  // TODO should check result
    xTaskCreate(input_task, "Input Task", 2048, NULL, 5, NULL);  // TODO should check result

    Control_queue_event_ts(MySm_EventId_NEXT); // Start the LED show
}

static void input_task(void *_unused) {
    while (true) {
        input_iterate();
    }
}

// This function could be written to be non-blocking, but we want some simple blocking for this example.
static void input_iterate(void) {
    int event_id;
    char c = read_char_blocking();

    switch (c) {
        case 'n':
            event_id = MySm_EventId_NEXT;
            break;
        case 'r':
            event_id = MySm_EventId_RESET;
            break;
        default:
            event_id = -1;
            break;
    }

    if (event_id == -1) {
        return;
    }

    Control_queue_event_ts((MySm_EventId)event_id);
}

// This function could be written to be non-blocking, but we want some simple blocking for this example.
static char read_char_blocking(void) {
    while (true) {
        char c = fgetc(stdin);
        if (c != EOF) {
            return c;
        }

        vTaskDelay(pdMS_TO_TICKS(10));  // wait for input to be available
    }
}