// Generated state machine
class OnOffSm
{
    static EventId = 
    {
        DIM : 0,
        INCREASE : 1,
    }
    static { Object.freeze(this.EventId); }
    
    static EventIdCount = 2;
    static { Object.freeze(this.EventIdCount); }
    
    static StateId = 
    {
        ROOT : 0,
        OFF : 1,
        ON1 : 2,
    }
    static { Object.freeze(this.StateId); }
    
    static StateIdCount = 3;
    static { Object.freeze(this.StateIdCount); }
    
    // Used internally by state machine. Feel free to inspect, but don't modify.
    stateId;
    
    // Used internally by state machine. Don't modify.
    #ancestorEventHandler;
    
    // Used internally by state machine. Don't modify.
    #currentEventHandlers = Array(OnOffSm.EventIdCount).fill(undefined);
    
    // Used internally by state machine. Don't modify.
    #currentStateExitHandler;
    
    // Starts the state machine. Must be called before dispatching events. Not thread safe.
    start()
    {
        this.#ROOT_enter();
        // ROOT behavior
        // uml: TransitionTo(ROOT.InitialState)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `ROOT.InitialState`.
            // ROOT.InitialState is a pseudo state and cannot have an `enter` trigger.
            
            // ROOT.InitialState behavior
            // uml: / { trace("Transition action `` for ROOT.InitialState to OFF."); } TransitionTo(OFF)
            {
                // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                
                // Step 2: Transition action: `trace("Transition action `` for ROOT.InitialState to OFF.");`.
                trace("Transition action `` for ROOT.InitialState to OFF.");
                
                // Step 3: Enter/move towards transition target `OFF`.
                this.#OFF_enter();
                
                // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                this.stateId = OnOffSm.StateId.OFF;
                // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
                return;
            } // end of behavior for ROOT.InitialState
        } // end of behavior for ROOT
    }
    
    // Dispatches an event to the state machine. Not thread safe.
    dispatchEvent(/** @type {number} */ eventId)
    {
        let behaviorFunc = this.#currentEventHandlers[eventId];
        
        while (behaviorFunc != null)
        {
            this.#ancestorEventHandler = null;
            behaviorFunc.call(this);
            behaviorFunc = this.#ancestorEventHandler;
        }
    }
    
    // This function is used when StateSmith doesn't know what the active leaf state is at
    // compile time due to sub states or when multiple states need to be exited.
    #exitUpToStateHandler(/** @type {function} */ desiredStateExitHandler)
    {
        while (this.#currentStateExitHandler != desiredStateExitHandler)
        {
            this.#currentStateExitHandler.call(this);
        }
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state ROOT
    ////////////////////////////////////////////////////////////////////////////////
    
    #ROOT_enter()
    {
        // setup trigger/event handlers
        this.#currentStateExitHandler = this.#ROOT_exit;
        
        // ROOT behavior
        // uml: enter / { trace("Enter OnOffSm."); }
        {
            // Step 1: execute action `trace("Enter OnOffSm.");`
            trace("Enter OnOffSm.");
        } // end of behavior for ROOT
    }
    
    #ROOT_exit()
    {
        // ROOT behavior
        // uml: exit / { trace("Exit OnOffSm."); }
        {
            // Step 1: execute action `trace("Exit OnOffSm.");`
            trace("Exit OnOffSm.");
        } // end of behavior for ROOT
;
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state OFF
    ////////////////////////////////////////////////////////////////////////////////
    
    #OFF_enter()
    {
        // setup trigger/event handlers
        this.#currentStateExitHandler = this.#OFF_exit;
        this.#currentEventHandlers[OnOffSm.EventId.INCREASE] = this.#OFF_increase;
        
        // OFF behavior
        // uml: enter / { trace("Enter OFF."); }
        {
            // Step 1: execute action `trace("Enter OFF.");`
            trace("Enter OFF.");
        } // end of behavior for OFF
        
        // OFF behavior
        // uml: enter [trace_guard("State OFF: check behavior `enter / { light_off(); }`.", true)] / { light_off(); }
        if (trace_guard("State OFF: check behavior `enter / { light_off(); }`.", true))
        {
            // Step 1: execute action `light_off();`
            console.log('light is off');
        } // end of behavior for OFF
    }
    
    #OFF_exit()
    {
        // OFF behavior
        // uml: exit / { trace("Exit OFF."); }
        {
            // Step 1: execute action `trace("Exit OFF.");`
            trace("Exit OFF.");
        } // end of behavior for OFF
        
        // adjust function pointers for this state's exit
        this.#currentStateExitHandler = this.#ROOT_exit;
        this.#currentEventHandlers[OnOffSm.EventId.INCREASE] = null;  // no ancestor listens to this event
    }
    
    #OFF_increase()
    {
        // No ancestor state handles `increase` event.
        
        // OFF behavior
        // uml: INCREASE [trace_guard("State OFF: check behavior `INCREASE TransitionTo(ON1)`.", true)] / { trace("Transition action `` for OFF to ON1."); } TransitionTo(ON1)
        if (trace_guard("State OFF: check behavior `INCREASE TransitionTo(ON1)`.", true))
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            this.#OFF_exit();
            
            // Step 2: Transition action: `trace("Transition action `` for OFF to ON1.");`.
            trace("Transition action `` for OFF to ON1.");
            
            // Step 3: Enter/move towards transition target `ON1`.
            this.#ON1_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = OnOffSm.StateId.ON1;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for OFF
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state ON1
    ////////////////////////////////////////////////////////////////////////////////
    
    #ON1_enter()
    {
        // setup trigger/event handlers
        this.#currentStateExitHandler = this.#ON1_exit;
        this.#currentEventHandlers[OnOffSm.EventId.DIM] = this.#ON1_dim;
        
        // ON1 behavior
        // uml: enter / { trace("Enter ON1."); }
        {
            // Step 1: execute action `trace("Enter ON1.");`
            trace("Enter ON1.");
        } // end of behavior for ON1
        
        // ON1 behavior
        // uml: enter [trace_guard("State ON1: check behavior `enter / { light_blue(); }`.", true)] / { light_blue(); }
        if (trace_guard("State ON1: check behavior `enter / { light_blue(); }`.", true))
        {
            // Step 1: execute action `light_blue();`
            console.log('light is blue');
        } // end of behavior for ON1
    }
    
    #ON1_exit()
    {
        // ON1 behavior
        // uml: exit / { trace("Exit ON1."); }
        {
            // Step 1: execute action `trace("Exit ON1.");`
            trace("Exit ON1.");
        } // end of behavior for ON1
        
        // adjust function pointers for this state's exit
        this.#currentStateExitHandler = this.#ROOT_exit;
        this.#currentEventHandlers[OnOffSm.EventId.DIM] = null;  // no ancestor listens to this event
    }
    
    #ON1_dim()
    {
        // No ancestor state handles `dim` event.
        
        // ON1 behavior
        // uml: DIM [trace_guard("State ON1: check behavior `DIM TransitionTo(OFF)`.", true)] / { trace("Transition action `` for ON1 to OFF."); } TransitionTo(OFF)
        if (trace_guard("State ON1: check behavior `DIM TransitionTo(OFF)`.", true))
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            this.#ON1_exit();
            
            // Step 2: Transition action: `trace("Transition action `` for ON1 to OFF.");`.
            trace("Transition action `` for ON1 to OFF.");
            
            // Step 3: Enter/move towards transition target `OFF`.
            this.#OFF_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = OnOffSm.StateId.OFF;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for ON1
    }
    
    // Thread safe.
    stateIdToString(/** @type {number} */ id)
    {
        switch (id)
        {
            case OnOffSm.StateId.ROOT: return "ROOT";
            case OnOffSm.StateId.OFF: return "OFF";
            case OnOffSm.StateId.ON1: return "ON1";
            default: return "?";
        }
    }
    
    // Thread safe.
    eventIdToString(/** @type {number} */ id)
    {
        switch (id)
        {
            case OnOffSm.EventId.DIM: return "DIM";
            case OnOffSm.EventId.INCREASE: return "INCREASE";
            default: return "?";
        }
    }
}
