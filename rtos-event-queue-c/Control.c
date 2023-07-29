#include "Control.h"
#include "IntervalTimer.h"
#include "Leds.h"

#include <stdio.h>
#include <inttypes.h>
#include "freertos/FreeRTOS.h"
#include "freertos/queue.h"
#include "freertos/task.h"

//////////////////////// DEFINES ////////////////////////

#define QUEUE_LENGTH    10
#define DO_EVENT_INTERVAL_MS  400 // target milliseconds between DO events dispatched to state machine


//////////////////////// PROTOTYPES ////////////////////////

static void task_setup();
static void control_task(MySm *sm);
static void send_events_to_state_machine();
static void dispatch_event(enum MySm_EventId event_id);
static uint32_t get_ms_since_boot();


//////////////////////// VARS ////////////////////////

static QueueHandle_t event_queue;
static struct IntervalTimer timer;
static MySm sm;

//////////////////////// PUBLIC FUNCTIONS ////////////////////////

/**
 * @brief Initialize the control module.
 */
bool Control_init() {
    event_queue = xQueueCreate(QUEUE_LENGTH, sizeof(int));              // TODO should check result
    xTaskCreate(control_task, "control task", 2048, NULL, 5, NULL);     // TODO should check result
    Leds_init();
    MySm_ctor(&sm);
    MySm_start(&sm);
}

/**
 * @brief Queue an event to be processed by the control module. Thread safe.
 * @param event_id 
 */
void Control_queue_event_ts(enum MySm_EventId event_id) {
    xQueueSend(event_queue, &event_id, portMAX_DELAY);   // TODO should check result
}


//////////////////////// PRIVATE FUNCTIONS ////////////////////////

static void task_setup() {
    IntervalTimer_init(&timer, DO_EVENT_INTERVAL_MS, get_ms_since_boot()); // TODO should check result
}

static void control_task(MySm *sm) {
    task_setup();

    while (true) {
        send_events_to_state_machine();
    }
}

static void send_events_to_state_machine() {
    bool is_do_event_pending;
    const uint32_t ms_to_next_do_event_interval = IntervalTimer_update(&timer, &is_do_event_pending, get_ms_since_boot());
    // To be extra safe, you could clamp this value to something sane and ensure it can't accidentally be a special
    // RTOS value like `portMAX_DELAY`.

    if (is_do_event_pending) {
        dispatch_event(MySm_EventId_DO);
    }

    // This task doesn't need an explicit sleep because it will block on the queue receive.
    // It will unblock when an event is added to the queue or when the timeout expires.
    // The timeout is set so that it corresponds to the next interval for a DO event to be dispatched.
    enum MySm_EventId event_id;
    if (xQueueReceive(event_queue, &event_id, pdMS_TO_TICKS(ms_to_next_do_event_interval)) == pdTRUE) {
        dispatch_event(event_id);
    }
}

static void dispatch_event(enum MySm_EventId event_id) {
    printf("Sending `%s` event to sm. ms: %" PRIu32 "\n", MySm_event_id_to_string(event_id), get_ms_since_boot());
    MySm_dispatch_event(&sm, event_id);
}

static uint32_t get_ms_since_boot() {
    return (uint32_t) (esp_timer_get_time() / 1000);
}
