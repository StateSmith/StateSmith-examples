// Autogenerated with StateSmith 0.9.10-alpha+1f83cb59adcabe0a5a4c8d3e0421027761c28974.
// Algorithm: Balanced1. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

#include "LightSm.hpp"
#include "LightController.hpp"
#include <stdbool.h> // required for `consume_event` flag
#include <string.h> // for memset

// This function is used when StateSmith doesn't know what the active leaf state is at
// compile time due to sub states or when multiple states need to be exited.
static void exit_up_to_state_handler(LightSm* sm, LightSm_Func desired_state_exit_handler);

static void ROOT_enter(LightSm* sm);

static void ROOT_exit(LightSm* sm);

static void OFF_enter(LightSm* sm);

static void OFF_exit(LightSm* sm);

static void OFF_inc(LightSm* sm);

static void ON_GROUP_enter(LightSm* sm);

static void ON_GROUP_exit(LightSm* sm);

static void ON_GROUP_dim_long_press(LightSm* sm);

static void ON1_enter(LightSm* sm);

static void ON1_exit(LightSm* sm);

static void ON1_dim(LightSm* sm);

static void ON1_inc(LightSm* sm);

static void ON1_inc_long_press(LightSm* sm);

static void ON2_enter(LightSm* sm);

static void ON2_exit(LightSm* sm);

static void ON2_dim(LightSm* sm);

static void ON2_inc(LightSm* sm);

static void ON2_inc_long_press(LightSm* sm);

static void ON3_enter(LightSm* sm);

static void ON3_exit(LightSm* sm);

static void ON3_dim(LightSm* sm);


// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void LightSm_ctor(LightSm* sm)
{
    memset(sm, 0, sizeof(*sm));
}

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void LightSm_start(LightSm* sm)
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
        // uml: TransitionTo(OFF)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `OFF`.
            OFF_enter(sm);
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            sm->state_id = LightSm_StateId_OFF;
            // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
            return;
        } // end of behavior for ROOT.<InitialState>
    } // end of behavior for ROOT
}

// Dispatches an event to the state machine. Not thread safe.
void LightSm_dispatch_event(LightSm* sm, LightSm_EventId event_id)
{
    LightSm_Func behavior_func = sm->current_event_handlers[event_id];
    
    while (behavior_func != NULL)
    {
        sm->ancestor_event_handler = NULL;
        behavior_func(sm);
        behavior_func = sm->ancestor_event_handler;
    }
}

// This function is used when StateSmith doesn't know what the active leaf state is at
// compile time due to sub states or when multiple states need to be exited.
static void exit_up_to_state_handler(LightSm* sm, LightSm_Func desired_state_exit_handler)
{
    while (sm->current_state_exit_handler != desired_state_exit_handler)
    {
        sm->current_state_exit_handler(sm);
    }
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ROOT
////////////////////////////////////////////////////////////////////////////////

static void ROOT_enter(LightSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = ROOT_exit;
}

static void ROOT_exit(LightSm* sm)
{
    // State machine root is a special case. It cannot be exited. Mark as unused.
    (void)sm;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state OFF
////////////////////////////////////////////////////////////////////////////////

static void OFF_enter(LightSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = OFF_exit;
    sm->current_event_handlers[LightSm_EventId_INC] = OFF_inc;
    
    // OFF behavior
    // uml: enter / { light_off(); }
    {
        // Step 1: execute action `light_off();`
        LightController::light_off();
    } // end of behavior for OFF
}

static void OFF_exit(LightSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ROOT_exit;
    sm->current_event_handlers[LightSm_EventId_INC] = NULL;  // no ancestor listens to this event
}

static void OFF_inc(LightSm* sm)
{
    // No ancestor state handles `inc` event.
    
    // OFF behavior
    // uml: INC TransitionTo(ON_GROUP)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        OFF_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON_GROUP`.
        ON_GROUP_enter(sm);
        
        // ON_GROUP.<InitialState> behavior
        // uml: TransitionTo(ON1)
        {
            // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `ON1`.
            ON1_enter(sm);
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            sm->state_id = LightSm_StateId_ON1;
            // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
            return;
        } // end of behavior for ON_GROUP.<InitialState>
    } // end of behavior for OFF
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ON_GROUP
////////////////////////////////////////////////////////////////////////////////

static void ON_GROUP_enter(LightSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = ON_GROUP_exit;
    sm->current_event_handlers[LightSm_EventId_DIM_LONG_PRESS] = ON_GROUP_dim_long_press;
}

static void ON_GROUP_exit(LightSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ROOT_exit;
    sm->current_event_handlers[LightSm_EventId_DIM_LONG_PRESS] = NULL;  // no ancestor listens to this event
}

static void ON_GROUP_dim_long_press(LightSm* sm)
{
    // No ancestor state handles `dim_long_press` event.
    
    // ON_GROUP behavior
    // uml: DIM_LONG_PRESS TransitionTo(OFF)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        exit_up_to_state_handler(sm, ROOT_exit);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `OFF`.
        OFF_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_OFF;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON_GROUP
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ON1
////////////////////////////////////////////////////////////////////////////////

static void ON1_enter(LightSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = ON1_exit;
    sm->current_event_handlers[LightSm_EventId_DIM] = ON1_dim;
    sm->current_event_handlers[LightSm_EventId_INC] = ON1_inc;
    sm->current_event_handlers[LightSm_EventId_INC_LONG_PRESS] = ON1_inc_long_press;
    
    // ON1 behavior
    // uml: enter / { light_1(); }
    {
        // Step 1: execute action `light_1();`
        LightController::light_1();
    } // end of behavior for ON1
}

static void ON1_exit(LightSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ON_GROUP_exit;
    sm->current_event_handlers[LightSm_EventId_DIM] = NULL;  // no ancestor listens to this event
    sm->current_event_handlers[LightSm_EventId_INC] = NULL;  // no ancestor listens to this event
    sm->current_event_handlers[LightSm_EventId_INC_LONG_PRESS] = NULL;  // no ancestor listens to this event
}

static void ON1_dim(LightSm* sm)
{
    // No ancestor state handles `dim` event.
    
    // ON1 behavior
    // uml: DIM TransitionTo(OFF)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        exit_up_to_state_handler(sm, ROOT_exit);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `OFF`.
        OFF_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_OFF;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON1
}

static void ON1_inc(LightSm* sm)
{
    // No ancestor state handles `inc` event.
    
    // ON1 behavior
    // uml: INC TransitionTo(ON2)
    {
        // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition).
        ON1_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON2`.
        ON2_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_ON2;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON1
}

static void ON1_inc_long_press(LightSm* sm)
{
    // No ancestor state handles `inc_long_press` event.
    
    // ON1 behavior
    // uml: INC_LONG_PRESS TransitionTo(ON3)
    {
        // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition).
        ON1_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON3`.
        ON3_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_ON3;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON1
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ON2
////////////////////////////////////////////////////////////////////////////////

static void ON2_enter(LightSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = ON2_exit;
    sm->current_event_handlers[LightSm_EventId_DIM] = ON2_dim;
    sm->current_event_handlers[LightSm_EventId_INC] = ON2_inc;
    sm->current_event_handlers[LightSm_EventId_INC_LONG_PRESS] = ON2_inc_long_press;
    
    // ON2 behavior
    // uml: enter / { light_2(); }
    {
        // Step 1: execute action `light_2();`
        LightController::light_2();
    } // end of behavior for ON2
}

static void ON2_exit(LightSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ON_GROUP_exit;
    sm->current_event_handlers[LightSm_EventId_DIM] = NULL;  // no ancestor listens to this event
    sm->current_event_handlers[LightSm_EventId_INC] = NULL;  // no ancestor listens to this event
    sm->current_event_handlers[LightSm_EventId_INC_LONG_PRESS] = NULL;  // no ancestor listens to this event
}

static void ON2_dim(LightSm* sm)
{
    // No ancestor state handles `dim` event.
    
    // ON2 behavior
    // uml: DIM TransitionTo(ON1)
    {
        // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition).
        ON2_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON1`.
        ON1_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_ON1;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON2
}

static void ON2_inc(LightSm* sm)
{
    // No ancestor state handles `inc` event.
    
    // ON2 behavior
    // uml: (INC, INC_LONG_PRESS) TransitionTo(ON3)
    {
        // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition).
        ON2_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON3`.
        ON3_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_ON3;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON2
}

static void ON2_inc_long_press(LightSm* sm)
{
    // No ancestor state handles `inc_long_press` event.
    
    // ON2 behavior
    // uml: (INC, INC_LONG_PRESS) TransitionTo(ON3)
    {
        // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition).
        ON2_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON3`.
        ON3_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_ON3;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON2
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ON3
////////////////////////////////////////////////////////////////////////////////

static void ON3_enter(LightSm* sm)
{
    // setup trigger/event handlers
    sm->current_state_exit_handler = ON3_exit;
    sm->current_event_handlers[LightSm_EventId_DIM] = ON3_dim;
    
    // ON3 behavior
    // uml: enter / { light_3(); }
    {
        // Step 1: execute action `light_3();`
        LightController::light_3();
    } // end of behavior for ON3
}

static void ON3_exit(LightSm* sm)
{
    // adjust function pointers for this state's exit
    sm->current_state_exit_handler = ON_GROUP_exit;
    sm->current_event_handlers[LightSm_EventId_DIM] = NULL;  // no ancestor listens to this event
}

static void ON3_dim(LightSm* sm)
{
    // No ancestor state handles `dim` event.
    
    // ON3 behavior
    // uml: DIM TransitionTo(ON2)
    {
        // Step 1: Exit states until we reach `ON_GROUP` state (Least Common Ancestor for transition).
        ON3_exit(sm);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ON2`.
        ON2_enter(sm);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        sm->state_id = LightSm_StateId_ON2;
        // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
        return;
    } // end of behavior for ON3
}

// Thread safe.
char const * LightSm_state_id_to_string(LightSm_StateId id)
{
    switch (id)
    {
        case LightSm_StateId_ROOT: return "ROOT";
        case LightSm_StateId_OFF: return "OFF";
        case LightSm_StateId_ON_GROUP: return "ON_GROUP";
        case LightSm_StateId_ON1: return "ON1";
        case LightSm_StateId_ON2: return "ON2";
        case LightSm_StateId_ON3: return "ON3";
        default: return "?";
    }
}

// Thread safe.
char const * LightSm_event_id_to_string(LightSm_EventId id)
{
    switch (id)
    {
        case LightSm_EventId_DIM: return "DIM";
        case LightSm_EventId_DIM_LONG_PRESS: return "DIM_LONG_PRESS";
        case LightSm_EventId_INC: return "INC";
        case LightSm_EventId_INC_LONG_PRESS: return "INC_LONG_PRESS";
        default: return "?";
    }
}
