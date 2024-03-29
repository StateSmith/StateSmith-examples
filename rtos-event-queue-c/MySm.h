// Autogenerated with StateSmith 0.9.6-alpha.
// Algorithm: Balanced1. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

#pragma once
#include <stdint.h>

typedef enum MySm_EventId
{
    MySm_EventId_DO = 0, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
    MySm_EventId_NEXT = 1,
    MySm_EventId_RESET = 2,
} MySm_EventId;

enum
{
    MySm_EventIdCount = 3
};

typedef enum MySm_StateId
{
    MySm_StateId_ROOT = 0,
    MySm_StateId_OFF = 1,
    MySm_StateId_ON = 2,
    MySm_StateId_ON1 = 3,
    MySm_StateId_ON2 = 4,
    MySm_StateId_ON3 = 5,
} MySm_StateId;

enum
{
    MySm_StateIdCount = 6
};


// Generated state machine
// forward declaration
typedef struct MySm MySm;

// State machine variables. Can be used for inputs, outputs, user variables...
typedef struct MySm_Vars
{
    uint32_t t1_start_ms;  // ms since boot when timer 1 was started
} MySm_Vars;


// event handler type
typedef void (*MySm_Func)(MySm* sm);

// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void MySm_ctor(MySm* sm);

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void MySm_start(MySm* sm);

// Dispatches an event to the state machine. Not thread safe.
void MySm_dispatch_event(MySm* sm, MySm_EventId event_id);

// Thread safe.
char const * MySm_state_id_to_string(MySm_StateId id);

// Thread safe.
char const * MySm_event_id_to_string(MySm_EventId id);

// Generated state machine
struct MySm
{
    // Used internally by state machine. Feel free to inspect, but don't modify.
    MySm_StateId state_id;
    
    // Used internally by state machine. Don't modify.
    MySm_Func ancestor_event_handler;
    
    // Used internally by state machine. Don't modify.
    MySm_Func current_event_handlers[MySm_EventIdCount];
    
    // Used internally by state machine. Don't modify.
    MySm_Func current_state_exit_handler;
    
    // Variables. Can be used for inputs, outputs, user variables...
    MySm_Vars vars;
};

