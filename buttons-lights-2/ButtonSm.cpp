// Autogenerated with StateSmith 0.12.0-alpha+99dbe30d387ec88bb0e251bea536ac8980235987.
// Algorithm: Balanced1. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

#include "ButtonSm.h"
#include <stdbool.h> // required for `consume_event` flag
#include <string.h> // for memset

// This function is used when StateSmith doesn't know what the active leaf state is at
// compile time due to sub states or when multiple states need to be exited.
static void exit_up_to_state_handler(ButtonSm* sm, ButtonSm_Func desired_state_exit_handler);

static void ROOT_enter(ButtonSm* sm);

static void ROOT_exit(ButtonSm* sm);

static void PRESS_DEBOUNCE_enter(ButtonSm* sm);

static void PRESS_DEBOUNCE_exit(ButtonSm* sm);

static void PRESS_DEBOUNCE_do(ButtonSm* sm);

static void PRESSED_enter(ButtonSm* sm);

static void PRESSED_exit(ButtonSm* sm);

static void PRESSED_do(ButtonSm* sm);

static void CONFIRMING_LONG_enter(ButtonSm* sm);

static void CONFIRMING_LONG_exit(ButtonSm* sm);

static void CONFIRMING_LONG_do(ButtonSm* sm);

static void LONG_PRESS_enter(ButtonSm* sm);

static void LONG_PRESS_exit(ButtonSm* sm);

static void RELEASE_DEBOUNCE_enter(ButtonSm* sm);

static void RELEASE_DEBOUNCE_exit(ButtonSm* sm);

static void RELEASE_DEBOUNCE_do(ButtonSm* sm);

static void RELEASED_enter(ButtonSm* sm);

static void RELEASED_exit(ButtonSm* sm);

static void RELEASED_do(ButtonSm* sm);


// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void ButtonSm_ctor(ButtonSm* sm)
{
    memset(sm, 0, sizeof(*sm));
}

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void ButtonSm_start(ButtonSm* sm)
{
    ROOT_enter(sm);
    // ROOT behavior
    // uml: TransitionTo(ROOT.<InitialState>)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ROOT.<InitialState>`.
        // ROOT.<InitialState> is a pseudo state and cannot have an `enter` trigger.
        
        // ROOT.<InitialState> behavior
        // uml: TransitionTo(RELEASE_DEBOUNCE)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `RELEASE_DEBOUNCE`.
            RELEASE_DEBOUNCE_enter(sm);
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            sm->state_id = ButtonSm_StateId_RELEASE_DEBOUNCE;
            // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
            return;
        } // end of behavior for ROOT.<InitialState>
    } // end of behavior for ROOT
}

// Dispatches an event to the state machine. Not thread safe.
void ButtonSm_dispatch_event(ButtonSm* sm, ButtonSm_EventId event_id)
{
    ButtonSm_Func behavior_func = sm->current_event_handlers[event_id];
    
    while (behavior_func != NULL)
    {
        sm->ancestor_event_handler = NULL;
        behavior_func(sm);
        behavior_func = sm->ancestor_event_handler;
    }
}

// This function is used when StateSmith doesn't know what the active leaf state is at
// compile time due to sub states or when multiple states need to be exited.
static void exit_up_to_state_handler(ButtonSm* sm, ButtonSm_Func desired_state_exit_handler)
{
    while (sm->current_state_exit_handler != desired_state_exit_handler)
    {
        sm->current_state_exit_handler(sm);
    }
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ROOT
////////////////////////////////////////////////////////////////////////////////

static void ROOT_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = ROOT_exit;
}

static void ROOT_exit(ButtonSm* sm)
{
    // State machine root is a special case. It cannot be exited. Mark as unused.
    (void)sm;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state PRESS_DEBOUNCE
////////////////////////////////////////////////////////////////////////////////

static void PRESS_DEBOUNCE_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = PRESS_DEBOUNCE_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = PRESS_DEBOUNCE_do;
    
    // PRESS_DEBOUNCE behavior
    // uml: enter / { timer_ms = 0;\noutput_press = true;\noutput_press_event = true; }
    {
        // Step 1: execute action `timer_ms = 0;\noutput_press = true;\noutput_press_event = true;`
        sm->vars.timer_ms = 0;
        sm->vars.output_press = true;
        sm->vars.output_press_event = true;
    } // end of behavior for PRESS_DEBOUNCE
}

static void PRESS_DEBOUNCE_exit(ButtonSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ROOT_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = NULL;  // no ancestor listens to this event
}

static void PRESS_DEBOUNCE_do(ButtonSm* sm)
{
    // No ancestor state handles `do` event.
    
    // PRESS_DEBOUNCE behavior
    // uml: do [timer_ms > 50] TransitionTo(PRESSED)
    if (sm->vars.timer_ms > 50)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        PRESS_DEBOUNCE_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `PRESSED`.
        PRESSED_enter(sm);
        
        // PRESSED.<InitialState> behavior
        // uml: TransitionTo(CONFIRMING_LONG)
        {
            // Step 1: Exit states until we reach `PRESSED` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `CONFIRMING_LONG`.
            CONFIRMING_LONG_enter(sm);
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            sm->state_id = ButtonSm_StateId_CONFIRMING_LONG;
            // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
            return;
        } // end of behavior for PRESSED.<InitialState>
    } // end of behavior for PRESS_DEBOUNCE
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state PRESSED
////////////////////////////////////////////////////////////////////////////////

static void PRESSED_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = PRESSED_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = PRESSED_do;
}

static void PRESSED_exit(ButtonSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ROOT_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = NULL;  // no ancestor listens to this event
}

static void PRESSED_do(ButtonSm* sm)
{
    // No ancestor state handles `do` event.
    
    // PRESSED behavior
    // uml: do [!input_active] / { output_release_event = true; } TransitionTo(RELEASE_DEBOUNCE)
    if (!sm->vars.input_active)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        exit_up_to_state_handler(sm, ROOT_exit);
        
        // Step 2: Transition action: `output_release_event = true;`.
        sm->vars.output_release_event = true;
        
        // Step 3: Enter/move towards transition target `RELEASE_DEBOUNCE`.
        RELEASE_DEBOUNCE_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = ButtonSm_StateId_RELEASE_DEBOUNCE;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for PRESSED
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state CONFIRMING_LONG
////////////////////////////////////////////////////////////////////////////////

static void CONFIRMING_LONG_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = CONFIRMING_LONG_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = CONFIRMING_LONG_do;
}

static void CONFIRMING_LONG_exit(ButtonSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = PRESSED_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = PRESSED_do;  // the next ancestor that handles this event is PRESSED
}

static void CONFIRMING_LONG_do(ButtonSm* sm)
{
    // Setup handler for next ancestor that listens to `do` event.
    sm->ancestor_event_handler = PRESSED_do;
    
    // CONFIRMING_LONG behavior
    // uml: do [timer_ms > 500] TransitionTo(LONG_PRESS)
    if (sm->vars.timer_ms > 500)
    {
        // Step 1: Exit states until we reach `PRESSED` state (Least Common Ancestor for transition).
        CONFIRMING_LONG_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `LONG_PRESS`.
        LONG_PRESS_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = ButtonSm_StateId_LONG_PRESS;
        sm->ancestor_event_handler = NULL;
        return;
    } // end of behavior for CONFIRMING_LONG
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state LONG_PRESS
////////////////////////////////////////////////////////////////////////////////

static void LONG_PRESS_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = LONG_PRESS_exit;
    
    // LONG_PRESS behavior
    // uml: enter / { output_long_event = true; }
    {
        // Step 1: execute action `output_long_event = true;`
        sm->vars.output_long_event = true;
    } // end of behavior for LONG_PRESS
}

static void LONG_PRESS_exit(ButtonSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = PRESSED_exit;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state RELEASE_DEBOUNCE
////////////////////////////////////////////////////////////////////////////////

static void RELEASE_DEBOUNCE_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = RELEASE_DEBOUNCE_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = RELEASE_DEBOUNCE_do;
    
    // RELEASE_DEBOUNCE behavior
    // uml: enter / { timer_ms = 0; }
    {
        // Step 1: execute action `timer_ms = 0;`
        sm->vars.timer_ms = 0;
    } // end of behavior for RELEASE_DEBOUNCE
    
    // RELEASE_DEBOUNCE behavior
    // uml: enter / { output_press = false; }
    {
        // Step 1: execute action `output_press = false;`
        sm->vars.output_press = false;
    } // end of behavior for RELEASE_DEBOUNCE
}

static void RELEASE_DEBOUNCE_exit(ButtonSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ROOT_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = NULL;  // no ancestor listens to this event
}

static void RELEASE_DEBOUNCE_do(ButtonSm* sm)
{
    // No ancestor state handles `do` event.
    
    // RELEASE_DEBOUNCE behavior
    // uml: do [timer_ms > 50] TransitionTo(RELEASED)
    if (sm->vars.timer_ms > 50)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        RELEASE_DEBOUNCE_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `RELEASED`.
        RELEASED_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = ButtonSm_StateId_RELEASED;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for RELEASE_DEBOUNCE
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state RELEASED
////////////////////////////////////////////////////////////////////////////////

static void RELEASED_enter(ButtonSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = RELEASED_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = RELEASED_do;
}

static void RELEASED_exit(ButtonSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ROOT_exit;
    sm->current_event_handlers[ButtonSm_EventId_DO] = NULL;  // no ancestor listens to this event
}

static void RELEASED_do(ButtonSm* sm)
{
    // No ancestor state handles `do` event.
    
    // RELEASED behavior
    // uml: do [input_active] TransitionTo(PRESS_DEBOUNCE)
    if (sm->vars.input_active)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        RELEASED_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `PRESS_DEBOUNCE`.
        PRESS_DEBOUNCE_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = ButtonSm_StateId_PRESS_DEBOUNCE;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for RELEASED
}

// Thread safe.
char const * ButtonSm_state_id_to_string(ButtonSm_StateId id)
{
    switch (id)
    {
        case ButtonSm_StateId_ROOT: return "ROOT";
        case ButtonSm_StateId_PRESS_DEBOUNCE: return "PRESS_DEBOUNCE";
        case ButtonSm_StateId_PRESSED: return "PRESSED";
        case ButtonSm_StateId_CONFIRMING_LONG: return "CONFIRMING_LONG";
        case ButtonSm_StateId_LONG_PRESS: return "LONG_PRESS";
        case ButtonSm_StateId_RELEASE_DEBOUNCE: return "RELEASE_DEBOUNCE";
        case ButtonSm_StateId_RELEASED: return "RELEASED";
        default: return "?";
    }
}

// Thread safe.
char const * ButtonSm_event_id_to_string(ButtonSm_EventId id)
{
    switch (id)
    {
        case ButtonSm_EventId_DO: return "DO";
        default: return "?";
    }
}
