BEFORE TRANSFORMATIONS
===========================================================

Vertex: ROOT
-----------------------------------------
- Type: StateMachine
- Diagram Id: 152
- Initial State: ROOT.\<InitialState>

### Behaviors
    ANY / { log_unhandled_event(); }


Vertex: NORMAL
-----------------------------------------
- Parent: ROOT
- Type: State
- Diagram Id: 155

### Behaviors
    enter / { x = 0; }

    [err] TransitionTo(ERROR)


Vertex: STARTUP_BEEP
-----------------------------------------
- Parent: NORMAL
- Type: State
- Diagram Id: 158

### Behaviors
    enter / { buzz(); clear_timer(); }

    exit / { stop_buzz(); }

    [after_ms(100) 
    && x >= 13] TransitionTo(RUNNING)


Vertex: RUNNING
-----------------------------------------
- Parent: NORMAL
- Type: State
- Diagram Id: 160
- Initial State: RUNNING.\<InitialState>

### Behaviors


Vertex: PRE_HEAT
-----------------------------------------
- Parent: RUNNING
- Type: State
- Diagram Id: 162

### Behaviors
    enter / { clear_timer(); }

    [after_ms(200)] TransitionTo(WARM_ENOUGH)

    [too_cold] TransitionTo(PRE_HEAT)


Vertex: RUNNING.\<InitialState>
-----------------------------------------
- Parent: RUNNING
- Type: InitialState
- Diagram Id: 165

### Behaviors
    TransitionTo(PRE_HEAT)


Vertex: ERROR
-----------------------------------------
- Parent: ROOT
- Type: State
- Diagram Id: 167

### Behaviors
    enter / { shut_stuff_off(); }

    PRESS / { buzz(); }


Vertex: \<RenderConfig>
-----------------------------------------
- Parent: ROOT
- Type: RenderConfigVertex
- Diagram Id: 169


Vertex: \<Config>(TriggerMap)
-----------------------------------------
- Parent: \<RenderConfig>
- Type: ConfigOptionVertex
- Diagram Id: 171

### Option Content:
    ANY => * /* wildcard */


Vertex: WARM_ENOUGH
-----------------------------------------
- Parent: RUNNING
- Type: State
- Diagram Id: 188

### Behaviors
    enter / { stuff(); }


Vertex: \<Notes>
-----------------------------------------
- Parent: RUNNING
- Type: NotesVertex
- Diagram Id: 192

### Notes Content:
    NOTE! the constant transition back to self is dangerous in this example because it prevents error handling. Why? Because when a transition is taken, that event is considered consumed and its parent state NORMAL doesn't get to try its `[ err ]` transition.
    
    There are many ways to solve this. You can use timers differently, but if a particular error test is important, you should dispatch a special event ERR_CHECK that won't be consumed by the `[ too_cold ]` transition (which uses an implicit DO event).
    
    If you enable ancestors to be output to the description file, you can see in one place all the handlers active for a particular state. 
    
    This can be helpful for troubleshooting deeply nested designs. One day, I want to add this info ("inherited" behaviors) to the StateSmith plugin so that you can see it in the GUI as well.
    
    Vertex: PRE_HEAT
    -----------------------------------------
    - Parent: RUNNING
    - Type: State
    - Diagram Id: 162
    
    ### Behaviors
        enter / { clear_timer(); }
    
        do [after_ms(100)] TransitionTo(WARM_ENOUGH)
    
        do [too_cold] TransitionTo(PRE_HEAT)
    
        =========== from ancestor NORMAL ===========
    
        do [err] TransitionTo(ERROR)


Vertex: ROOT.\<InitialState>
-----------------------------------------
- Parent: ROOT
- Type: InitialState
- Diagram Id: 193

### Behaviors
    TransitionTo(STARTUP_BEEP)


Vertex: \<Notes>
-----------------------------------------
- Parent: ROOT
- Type: NotesVertex
- Diagram Id: 197

### Notes Content:
    Look inside RUNNING state for a safety related concern.


Vertex: \<RenderConfig>
-----------------------------------------
- Type: RenderConfigVertex
- Diagram Id: 175


Vertex: \<Config>(AutoExpandedVars)
-----------------------------------------
- Parent: \<RenderConfig>
- Type: ConfigOptionVertex
- Diagram Id: 177

### Option Content:
    int x;
    int y;


Vertex: \<Notes>
-----------------------------------------
- Type: NotesVertex
- Diagram Id: 194

### Notes Content:
    Note text and RenderConfig's will show up in the "Before Transformations" section.
    
    
    Bold text.
