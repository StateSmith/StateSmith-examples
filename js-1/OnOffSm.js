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
            // uml: TransitionTo(OFF)
            {
                // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                
                // Step 2: Transition action: ``.
                
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
    dispatchEvent(/** @type {OnOffSm.EventId} */ eventId)
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
    #exitUpToStateHandler(/** @type {OnOffSm.Func} */ desiredStateExitHandler)
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
    }
    
    #ROOT_exit()
    {
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
        // uml: enter / { light_off(); }
        {
            // Step 1: execute action `light_off();`
            console.log('light is off');
        } // end of behavior for OFF
    }
    
    #OFF_exit()
    {
        // adjust function pointers for this state's exit
        this.#currentStateExitHandler = this.#ROOT_exit;
        this.#currentEventHandlers[OnOffSm.EventId.INCREASE] = null;  // no ancestor listens to this event
    }
    
    #OFF_increase()
    {
        // No ancestor state handles `increase` event.
        
        // OFF behavior
        // uml: INCREASE TransitionTo(ON1)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            this.#OFF_exit();
            
            // Step 2: Transition action: ``.
            
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
        // uml: enter / { light_blue(); }
        {
            // Step 1: execute action `light_blue();`
            console.log('light is blue');
        } // end of behavior for ON1
    }
    
    #ON1_exit()
    {
        // adjust function pointers for this state's exit
        this.#currentStateExitHandler = this.#ROOT_exit;
        this.#currentEventHandlers[OnOffSm.EventId.DIM] = null;  // no ancestor listens to this event
    }
    
    #ON1_dim()
    {
        // No ancestor state handles `dim` event.
        
        // ON1 behavior
        // uml: DIM TransitionTo(OFF)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            this.#ON1_exit();
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `OFF`.
            this.#OFF_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = OnOffSm.StateId.OFF;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for ON1
    }
    
    // Thread safe.
    stateIdToString(/** @type {OnOffSm.StateId} */ id)
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
    eventIdToString(/** @type {OnOffSm.EventId} */ id)
    {
        switch (id)
        {
            case OnOffSm.EventId.DIM: return "DIM";
            case OnOffSm.EventId.INCREASE: return "INCREASE";
            default: return "?";
        }
    }
}
