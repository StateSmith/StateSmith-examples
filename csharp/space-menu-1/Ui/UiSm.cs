#nullable enable
namespace ConsoleApp1.Ui;
// Generated state machine
public partial class UiSm : UiSmBase
{
    public enum EventId
    {
        DO = 0, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
        DOWN = 1,
        ESC = 2,
        LEFT = 3,
        PG_DOWN = 4,
        PG_UP = 5,
        RIGHT = 6,
        UP = 7,
    }

    public const int EventIdCount = 8;

    public enum StateId
    {
        ROOT = 0,
        DONE = 1,
        MENU = 2,
        ESC_CATCHER_1 = 3,
        DRINK_MENU = 4,
        FOOD_MENU = 5,
        EAT_BURRITO = 6,
        EAT_MRE = 7,
        EAT_SLUDGE = 8,
        EVENT_EATER_2 = 9,
        COOKING_FOOD = 10,
        COOKING_FOOD1 = 11,
        SYSTEM_INFO_MENU = 12,
        BURRITO_COUNT = 13,
        DISTANCE = 14,
        SELF_DESTRUCT = 15,
        SD_1 = 16,
        SELF_DESTRUCT_OPTION = 17,
        MAIN_MENU_INNER = 18,
        DRINK = 19,
        FOOD = 20,
        SYSTEM_INFO = 21,
        SPLASH_SCREEN = 22,
    }

    public const int StateIdCount = 23;

    // event handler type
    private delegate void Func(UiSm sm);

    // Used internally by state machine. Feel free to inspect, but don't modify.
    public StateId stateId;

    // Used internally by state machine. Don't modify.
    private Func? ancestorEventHandler;

    // Used internally by state machine. Don't modify.
    private readonly Func?[] currentEventHandlers = new Func[EventIdCount];

    // Used internally by state machine. Don't modify.
    private Func? currentStateExitHandler;

    // State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
    public UiSm()
    {
    }

    // Starts the state machine. Must be called before dispatching events. Not thread safe.
    public void Start()
    {
        ROOT_enter();
        // ROOT behavior
        // uml: TransitionTo(ROOT.InitialState)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `ROOT.InitialState`.
            // ROOT.InitialState is a pseudo state and cannot have an `enter` trigger.

            // ROOT.InitialState behavior
            // uml: TransitionTo(SPLASH_SCREEN)
            {
                // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                // Step 2: Transition action: ``.

                // Step 3: Enter/move towards transition target `SPLASH_SCREEN`.
                SPLASH_SCREEN_enter();

                // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                this.stateId = StateId.SPLASH_SCREEN;
                // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
                return;
            } // end of behavior for ROOT.InitialState
        } // end of behavior for ROOT
    }

    // Dispatches an event to the state machine. Not thread safe.
    public void DispatchEvent(EventId eventId)
    {
        Func? behaviorFunc = this.currentEventHandlers[(int)eventId];

        while (behaviorFunc != null)
        {
            this.ancestorEventHandler = null;
            behaviorFunc(this);
            behaviorFunc = this.ancestorEventHandler;
        }
    }

    // This function is used when StateSmith doesn't know what the active leaf state is at
    // compile time due to sub states or when multiple states need to be exited.
    private void ExitUpToStateHandler(Func desiredStateExitHandler)
    {
        while (this.currentStateExitHandler != desiredStateExitHandler)
        {
            this.currentStateExitHandler!(this);
        }
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state ROOT
    ////////////////////////////////////////////////////////////////////////////////

    private void ROOT_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_ROOT_exit;
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_ROOT_exit = (UiSm sm) => sm.ROOT_exit();
    private void ROOT_exit()
    {
        // State machine root is a special case. It cannot be exited. Mark as unused.
        _ = this;
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state DONE
    ////////////////////////////////////////////////////////////////////////////////

    private void DONE_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_DONE_exit;

        // DONE behavior
        // uml: enter / { ShowDoneScreen(); }
        {
            // Step 1: execute action `ShowDoneScreen();`
            ShowDoneScreen();
        } // end of behavior for DONE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DONE_exit = (UiSm sm) => sm.DONE_exit();
    private void DONE_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_ROOT_exit;
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state MENU
    ////////////////////////////////////////////////////////////////////////////////

    private void MENU_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_MENU_exit;
        this.currentEventHandlers[(int)EventId.ESC] = ptr_MENU_esc;

        // MENU behavior
        // uml: enter / { display.SetMenuMode(true); }
        {
            // Step 1: execute action `display.SetMenuMode(true);`
            display.SetMenuMode(true);
        } // end of behavior for MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_MENU_exit = (UiSm sm) => sm.MENU_exit();
    private void MENU_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_ROOT_exit;
        this.currentEventHandlers[(int)EventId.ESC] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_MENU_esc = (UiSm sm) => sm.MENU_esc();
    private void MENU_esc()
    {
        // No ancestor state handles `esc` event.

        // MENU behavior
        // uml: ESC TransitionTo(DONE)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_ROOT_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DONE`.
            DONE_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DONE;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for MENU
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state ESC_CATCHER_1
    ////////////////////////////////////////////////////////////////////////////////

    private void ESC_CATCHER_1_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_ESC_CATCHER_1_exit;
        this.currentEventHandlers[(int)EventId.ESC] = ptr_ESC_CATCHER_1_esc;
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_ESC_CATCHER_1_exit = (UiSm sm) => sm.ESC_CATCHER_1_exit();
    private void ESC_CATCHER_1_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_MENU_exit;
        this.currentEventHandlers[(int)EventId.ESC] = ptr_MENU_esc;  // the next ancestor that handles this event is MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_ESC_CATCHER_1_esc = (UiSm sm) => sm.ESC_CATCHER_1_esc();
    private void ESC_CATCHER_1_esc()
    {
        // Setup handler for next ancestor that listens to `esc` event.
        this.ancestorEventHandler = ptr_MENU_esc;

        // ESC_CATCHER_1 behavior
        // uml: ESC TransitionTo(MAIN_MENU_INNER)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `MAIN_MENU_INNER`.
            MAIN_MENU_INNER_enter();

            // Finish transition by calling pseudo state transition function.
            MAIN_MENU_INNER_InitialState_transition();
            return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.
        } // end of behavior for ESC_CATCHER_1
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state DRINK_MENU
    ////////////////////////////////////////////////////////////////////////////////

    private void DRINK_MENU_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_DRINK_MENU_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_DRINK_MENU_left;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_DRINK_MENU_right;

        // DRINK_MENU behavior
        // uml: enter / { MenuHeader("Coffee System Offline");\nMenuOption("Activate self destruct?"); }
        {
            // Step 1: execute action `MenuHeader("Coffee System Offline");\nMenuOption("Activate self destruct?");`
            MenuHeader("Coffee System Offline");
            MenuOption("Activate self destruct?");
        } // end of behavior for DRINK_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_MENU_exit = (UiSm sm) => sm.DRINK_MENU_exit();
    private void DRINK_MENU_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_ESC_CATCHER_1_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_MENU_left = (UiSm sm) => sm.DRINK_MENU_left();
    private void DRINK_MENU_left()
    {
        // No ancestor state handles `left` event.

        // DRINK_MENU behavior
        // uml: LEFT TransitionTo(DRINK)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DRINK`.
            MAIN_MENU_INNER_enter();
            DRINK_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DRINK;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for DRINK_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_MENU_right = (UiSm sm) => sm.DRINK_MENU_right();
    private void DRINK_MENU_right()
    {
        // No ancestor state handles `right` event.

        // DRINK_MENU behavior
        // uml: RIGHT TransitionTo(SYSTEM_INFO_MENU.EntryPoint(self_destruct))
        {
            // Step 1: Exit states until we reach `ESC_CATCHER_1` state (Least Common Ancestor for transition).
            DRINK_MENU_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SYSTEM_INFO_MENU.EntryPoint(self_destruct)`.
            SYSTEM_INFO_MENU_enter();
            // SYSTEM_INFO_MENU.EntryPoint(self_destruct) is a pseudo state and cannot have an `enter` trigger.

            // SYSTEM_INFO_MENU.EntryPoint(self_destruct) behavior
            // uml: TransitionTo(SELF_DESTRUCT)
            {
                // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                // Step 2: Transition action: ``.

                // Step 3: Enter/move towards transition target `SELF_DESTRUCT`.
                SELF_DESTRUCT_enter();

                // Finish transition by calling pseudo state transition function.
                SELF_DESTRUCT_InitialState_transition();
                return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.
            } // end of behavior for SYSTEM_INFO_MENU.EntryPoint(self_destruct)
        } // end of behavior for DRINK_MENU
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state FOOD_MENU
    ////////////////////////////////////////////////////////////////////////////////

    private void FOOD_MENU_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_FOOD_MENU_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_FOOD_MENU_left;

        // FOOD_MENU behavior
        // uml: enter / { MenuHeader("Food Menu"); }
        {
            // Step 1: execute action `MenuHeader("Food Menu");`
            MenuHeader("Food Menu");
        } // end of behavior for FOOD_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_MENU_exit = (UiSm sm) => sm.FOOD_MENU_exit();
    private void FOOD_MENU_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_ESC_CATCHER_1_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_MENU_left = (UiSm sm) => sm.FOOD_MENU_left();
    private void FOOD_MENU_left()
    {
        // No ancestor state handles `left` event.

        // FOOD_MENU behavior
        // uml: LEFT TransitionTo(FOOD)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `FOOD`.
            MAIN_MENU_INNER_enter();
            FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.FOOD;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for FOOD_MENU
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state EAT_BURRITO
    ////////////////////////////////////////////////////////////////////////////////

    private void EAT_BURRITO_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_EAT_BURRITO_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_EAT_BURRITO_down;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_EAT_BURRITO_right;

        // EAT_BURRITO behavior
        // uml: enter / { MenuOption("Big Fat Burrito"); }
        {
            // Step 1: execute action `MenuOption("Big Fat Burrito");`
            MenuOption("Big Fat Burrito");
        } // end of behavior for EAT_BURRITO
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_BURRITO_exit = (UiSm sm) => sm.EAT_BURRITO_exit();
    private void EAT_BURRITO_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_FOOD_MENU_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_BURRITO_down = (UiSm sm) => sm.EAT_BURRITO_down();
    private void EAT_BURRITO_down()
    {
        // No ancestor state handles `down` event.

        // EAT_BURRITO behavior
        // uml: DOWN TransitionTo(EAT_SLUDGE)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_BURRITO_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `EAT_SLUDGE`.
            EAT_SLUDGE_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.EAT_SLUDGE;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_BURRITO
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_BURRITO_right = (UiSm sm) => sm.EAT_BURRITO_right();
    private void EAT_BURRITO_right()
    {
        // No ancestor state handles `right` event.

        // EAT_BURRITO behavior
        // uml: RIGHT / { burritoCount--; } TransitionTo(COOKING_FOOD)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_BURRITO_exit();

            // Step 2: Transition action: `burritoCount--;`.
            burritoCount--;

            // Step 3: Enter/move towards transition target `COOKING_FOOD`.
            EVENT_EATER_2_enter();
            COOKING_FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.COOKING_FOOD;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_BURRITO
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state EAT_MRE
    ////////////////////////////////////////////////////////////////////////////////

    private void EAT_MRE_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_EAT_MRE_exit;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_EAT_MRE_right;
        this.currentEventHandlers[(int)EventId.UP] = ptr_EAT_MRE_up;

        // EAT_MRE behavior
        // uml: enter / { MenuOption("MRE"); }
        {
            // Step 1: execute action `MenuOption("MRE");`
            MenuOption("MRE");
        } // end of behavior for EAT_MRE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_MRE_exit = (UiSm sm) => sm.EAT_MRE_exit();
    private void EAT_MRE_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_FOOD_MENU_exit;
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_MRE_right = (UiSm sm) => sm.EAT_MRE_right();
    private void EAT_MRE_right()
    {
        // No ancestor state handles `right` event.

        // EAT_MRE behavior
        // uml: RIGHT TransitionTo(COOKING_FOOD)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_MRE_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `COOKING_FOOD`.
            EVENT_EATER_2_enter();
            COOKING_FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.COOKING_FOOD;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_MRE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_MRE_up = (UiSm sm) => sm.EAT_MRE_up();
    private void EAT_MRE_up()
    {
        // No ancestor state handles `up` event.

        // EAT_MRE behavior
        // uml: UP TransitionTo(EAT_SLUDGE)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_MRE_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `EAT_SLUDGE`.
            EAT_SLUDGE_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.EAT_SLUDGE;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_MRE
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state EAT_SLUDGE
    ////////////////////////////////////////////////////////////////////////////////

    private void EAT_SLUDGE_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_EAT_SLUDGE_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_EAT_SLUDGE_down;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_EAT_SLUDGE_right;
        this.currentEventHandlers[(int)EventId.UP] = ptr_EAT_SLUDGE_up;

        // EAT_SLUDGE behavior
        // uml: enter / { MenuOption("Nutrient Mush"); }
        {
            // Step 1: execute action `MenuOption("Nutrient Mush");`
            MenuOption("Nutrient Mush");
        } // end of behavior for EAT_SLUDGE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_SLUDGE_exit = (UiSm sm) => sm.EAT_SLUDGE_exit();
    private void EAT_SLUDGE_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_FOOD_MENU_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_SLUDGE_down = (UiSm sm) => sm.EAT_SLUDGE_down();
    private void EAT_SLUDGE_down()
    {
        // No ancestor state handles `down` event.

        // EAT_SLUDGE behavior
        // uml: DOWN TransitionTo(EAT_MRE)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_SLUDGE_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `EAT_MRE`.
            EAT_MRE_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.EAT_MRE;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_SLUDGE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_SLUDGE_right = (UiSm sm) => sm.EAT_SLUDGE_right();
    private void EAT_SLUDGE_right()
    {
        // No ancestor state handles `right` event.

        // EAT_SLUDGE behavior
        // uml: RIGHT TransitionTo(COOKING_FOOD1)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_SLUDGE_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `COOKING_FOOD1`.
            EVENT_EATER_2_enter();
            COOKING_FOOD1_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.COOKING_FOOD1;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_SLUDGE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EAT_SLUDGE_up = (UiSm sm) => sm.EAT_SLUDGE_up();
    private void EAT_SLUDGE_up()
    {
        // No ancestor state handles `up` event.

        // EAT_SLUDGE behavior
        // uml: UP TransitionTo(EAT_BURRITO)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            EAT_SLUDGE_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `EAT_BURRITO`.
            EAT_BURRITO_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.EAT_BURRITO;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for EAT_SLUDGE
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state EVENT_EATER_2
    ////////////////////////////////////////////////////////////////////////////////

    private void EVENT_EATER_2_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_EVENT_EATER_2_exit;
        this.currentEventHandlers[(int)EventId.ESC] = ptr_EVENT_EATER_2_esc;
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_EVENT_EATER_2_left;
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EVENT_EATER_2_exit = (UiSm sm) => sm.EVENT_EATER_2_exit();
    private void EVENT_EATER_2_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_FOOD_MENU_exit;
        this.currentEventHandlers[(int)EventId.ESC] = ptr_ESC_CATCHER_1_esc;  // the next ancestor that handles this event is ESC_CATCHER_1
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_FOOD_MENU_left;  // the next ancestor that handles this event is FOOD_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EVENT_EATER_2_esc = (UiSm sm) => sm.EVENT_EATER_2_esc();
    private void EVENT_EATER_2_esc()
    {
        // Setup handler for next ancestor that listens to `esc` event.
        this.ancestorEventHandler = ptr_ESC_CATCHER_1_esc;

        // EVENT_EATER_2 behavior
        // uml: LEFT, ESC
        {
            // Step 1: execute action ``
            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for EVENT_EATER_2
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_EVENT_EATER_2_left = (UiSm sm) => sm.EVENT_EATER_2_left();
    private void EVENT_EATER_2_left()
    {
        // Setup handler for next ancestor that listens to `left` event.
        this.ancestorEventHandler = ptr_FOOD_MENU_left;

        // EVENT_EATER_2 behavior
        // uml: LEFT, ESC
        {
            // Step 1: execute action ``
            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for EVENT_EATER_2
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state COOKING_FOOD
    ////////////////////////////////////////////////////////////////////////////////

    private void COOKING_FOOD_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_COOKING_FOOD_exit;
        this.currentEventHandlers[(int)EventId.DO] = ptr_COOKING_FOOD_do;

        // COOKING_FOOD behavior
        // uml: enter / { t1Restart();\nMenuHeader("Cooking Food");\nMenuOption("..."); }
        {
            // Step 1: execute action `t1Restart();\nMenuHeader("Cooking Food");\nMenuOption("...");`
            t1.Restart();
            MenuHeader("Cooking Food");
            MenuOption("...");
        } // end of behavior for COOKING_FOOD
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_COOKING_FOOD_exit = (UiSm sm) => sm.COOKING_FOOD_exit();
    private void COOKING_FOOD_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_EVENT_EATER_2_exit;
        this.currentEventHandlers[(int)EventId.DO] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_COOKING_FOOD_do = (UiSm sm) => sm.COOKING_FOOD_do();
    private void COOKING_FOOD_do()
    {
        // No ancestor state handles `do` event.

        // COOKING_FOOD behavior
        // uml: do [t1After( 3.5 s )] TransitionTo(FOOD_MENU.ExitPoint(food_selected))
        if (t1.ElapsedMilliseconds >= 3500)
        {
            // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_FOOD_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `FOOD_MENU.ExitPoint(food_selected)`.
            // FOOD_MENU.ExitPoint(food_selected) is a pseudo state and cannot have an `enter` trigger.

            // FOOD_MENU.ExitPoint(food_selected) behavior
            // uml: TransitionTo(FOOD)
            {
                // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
                ExitUpToStateHandler(ptr_MENU_exit);

                // Step 2: Transition action: ``.

                // Step 3: Enter/move towards transition target `FOOD`.
                MAIN_MENU_INNER_enter();
                FOOD_enter();

                // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                this.stateId = StateId.FOOD;
                // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
                return;
            } // end of behavior for FOOD_MENU.ExitPoint(food_selected)
        } // end of behavior for COOKING_FOOD
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state COOKING_FOOD1
    ////////////////////////////////////////////////////////////////////////////////

    private void COOKING_FOOD1_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_COOKING_FOOD1_exit;
        this.currentEventHandlers[(int)EventId.DO] = ptr_COOKING_FOOD1_do;

        // COOKING_FOOD1 behavior
        // uml: enter / { t1Restart(); }
        {
            // Step 1: execute action `t1Restart();`
            t1.Restart();
        } // end of behavior for COOKING_FOOD1

        // COOKING_FOOD1 behavior
        // uml: enter / { MenuHeader("Mush..."); }
        {
            // Step 1: execute action `MenuHeader("Mush...");`
            MenuHeader("Mush...");
        } // end of behavior for COOKING_FOOD1

        // COOKING_FOOD1 behavior
        // uml: enter / { MenuOption("Gross..."); }
        {
            // Step 1: execute action `MenuOption("Gross...");`
            MenuOption("Gross...");
        } // end of behavior for COOKING_FOOD1
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_COOKING_FOOD1_exit = (UiSm sm) => sm.COOKING_FOOD1_exit();
    private void COOKING_FOOD1_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_EVENT_EATER_2_exit;
        this.currentEventHandlers[(int)EventId.DO] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_COOKING_FOOD1_do = (UiSm sm) => sm.COOKING_FOOD1_do();
    private void COOKING_FOOD1_do()
    {
        // No ancestor state handles `do` event.

        // COOKING_FOOD1 behavior
        // uml: do [t1After( 1s )] TransitionTo(COOKING_FOOD)
        if (t1.ElapsedMilliseconds >= 1000)
        {
            // Step 1: Exit states until we reach `EVENT_EATER_2` state (Least Common Ancestor for transition).
            COOKING_FOOD1_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `COOKING_FOOD`.
            COOKING_FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.COOKING_FOOD;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for COOKING_FOOD1
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state SYSTEM_INFO_MENU
    ////////////////////////////////////////////////////////////////////////////////

    private void SYSTEM_INFO_MENU_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_SYSTEM_INFO_MENU_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_SYSTEM_INFO_MENU_left;
        this.currentEventHandlers[(int)EventId.PG_DOWN] = ptr_SYSTEM_INFO_MENU_pg_down;
        this.currentEventHandlers[(int)EventId.PG_UP] = ptr_SYSTEM_INFO_MENU_pg_up;
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_MENU_exit = (UiSm sm) => sm.SYSTEM_INFO_MENU_exit();
    private void SYSTEM_INFO_MENU_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_ESC_CATCHER_1_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_MENU_left = (UiSm sm) => sm.SYSTEM_INFO_MENU_left();
    private void SYSTEM_INFO_MENU_left()
    {
        // No ancestor state handles `left` event.

        // SYSTEM_INFO_MENU behavior
        // uml: LEFT TransitionTo(SYSTEM_INFO)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SYSTEM_INFO`.
            MAIN_MENU_INNER_enter();
            SYSTEM_INFO_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SYSTEM_INFO;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for SYSTEM_INFO_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_MENU_pg_down = (UiSm sm) => sm.SYSTEM_INFO_MENU_pg_down();
    private void SYSTEM_INFO_MENU_pg_down()
    {
        // No ancestor state handles `pg_down` event.

        // SYSTEM_INFO_MENU behavior
        // uml: PG_DOWN TransitionTo(SELF_DESTRUCT_OPTION)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_SYSTEM_INFO_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SELF_DESTRUCT_OPTION`.
            SELF_DESTRUCT_OPTION_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SELF_DESTRUCT_OPTION;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for SYSTEM_INFO_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_MENU_pg_up = (UiSm sm) => sm.SYSTEM_INFO_MENU_pg_up();
    private void SYSTEM_INFO_MENU_pg_up()
    {
        // No ancestor state handles `pg_up` event.

        // SYSTEM_INFO_MENU behavior
        // uml: PG_UP TransitionTo(DISTANCE)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_SYSTEM_INFO_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DISTANCE`.
            DISTANCE_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DISTANCE;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for SYSTEM_INFO_MENU
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state BURRITO_COUNT
    ////////////////////////////////////////////////////////////////////////////////

    private void BURRITO_COUNT_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_BURRITO_COUNT_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_BURRITO_COUNT_down;
        this.currentEventHandlers[(int)EventId.UP] = ptr_BURRITO_COUNT_up;

        // BURRITO_COUNT behavior
        // uml: enter / { MenuOption("Burrito count: " + burritoCount); }
        {
            // Step 1: execute action `MenuOption("Burrito count: " + burritoCount);`
            MenuOption("Burrito count: " + burritoCount);
        } // end of behavior for BURRITO_COUNT
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_BURRITO_COUNT_exit = (UiSm sm) => sm.BURRITO_COUNT_exit();
    private void BURRITO_COUNT_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_SYSTEM_INFO_MENU_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_BURRITO_COUNT_down = (UiSm sm) => sm.BURRITO_COUNT_down();
    private void BURRITO_COUNT_down()
    {
        // No ancestor state handles `down` event.

        // BURRITO_COUNT behavior
        // uml: DOWN TransitionTo(SELF_DESTRUCT_OPTION)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            BURRITO_COUNT_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SELF_DESTRUCT_OPTION`.
            SELF_DESTRUCT_OPTION_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SELF_DESTRUCT_OPTION;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for BURRITO_COUNT
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_BURRITO_COUNT_up = (UiSm sm) => sm.BURRITO_COUNT_up();
    private void BURRITO_COUNT_up()
    {
        // No ancestor state handles `up` event.

        // BURRITO_COUNT behavior
        // uml: UP TransitionTo(DISTANCE)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            BURRITO_COUNT_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DISTANCE`.
            DISTANCE_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DISTANCE;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for BURRITO_COUNT
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state DISTANCE
    ////////////////////////////////////////////////////////////////////////////////

    private void DISTANCE_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_DISTANCE_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_DISTANCE_down;
        this.currentEventHandlers[(int)EventId.PG_UP] = ptr_DISTANCE_pg_up;
        this.currentEventHandlers[(int)EventId.UP] = ptr_DISTANCE_up;

        // DISTANCE behavior
        // uml: enter / { MenuOption("Distance to Tau Ceti: " + distance); }
        {
            // Step 1: execute action `MenuOption("Distance to Tau Ceti: " + distance);`
            MenuOption("Distance to Tau Ceti: " + "3.7 parsecs"); 
        } // end of behavior for DISTANCE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DISTANCE_exit = (UiSm sm) => sm.DISTANCE_exit();
    private void DISTANCE_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_SYSTEM_INFO_MENU_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_UP] = ptr_SYSTEM_INFO_MENU_pg_up;  // the next ancestor that handles this event is SYSTEM_INFO_MENU
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DISTANCE_down = (UiSm sm) => sm.DISTANCE_down();
    private void DISTANCE_down()
    {
        // No ancestor state handles `down` event.

        // DISTANCE behavior
        // uml: DOWN TransitionTo(BURRITO_COUNT)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            DISTANCE_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `BURRITO_COUNT`.
            BURRITO_COUNT_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.BURRITO_COUNT;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for DISTANCE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DISTANCE_pg_up = (UiSm sm) => sm.DISTANCE_pg_up();
    private void DISTANCE_pg_up()
    {
        // Setup handler for next ancestor that listens to `pg_up` event.
        this.ancestorEventHandler = ptr_SYSTEM_INFO_MENU_pg_up;

        // DISTANCE behavior
        // uml: UP, PG_UP / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for DISTANCE
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DISTANCE_up = (UiSm sm) => sm.DISTANCE_up();
    private void DISTANCE_up()
    {
        // No ancestor state handles `up` event.

        // DISTANCE behavior
        // uml: UP, PG_UP / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
        } // end of behavior for DISTANCE
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state SELF_DESTRUCT
    ////////////////////////////////////////////////////////////////////////////////

    private void SELF_DESTRUCT_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_SELF_DESTRUCT_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_SELF_DESTRUCT_left;

        // SELF_DESTRUCT behavior
        // uml: enter / { StartSelfDestructTimer(); }
        {
            // Step 1: execute action `StartSelfDestructTimer();`
            StartSelfDestructTimer();
        } // end of behavior for SELF_DESTRUCT
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_exit = (UiSm sm) => sm.SELF_DESTRUCT_exit();
    private void SELF_DESTRUCT_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_SYSTEM_INFO_MENU_exit;
        this.currentEventHandlers[(int)EventId.LEFT] = ptr_SYSTEM_INFO_MENU_left;  // the next ancestor that handles this event is SYSTEM_INFO_MENU
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_left = (UiSm sm) => sm.SELF_DESTRUCT_left();
    private void SELF_DESTRUCT_left()
    {
        // Setup handler for next ancestor that listens to `left` event.
        this.ancestorEventHandler = ptr_SYSTEM_INFO_MENU_left;

        // SELF_DESTRUCT behavior
        // uml: LEFT / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for SELF_DESTRUCT
    }

    private void SELF_DESTRUCT_InitialState_transition()
    {
        // SELF_DESTRUCT.InitialState behavior
        // uml: TransitionTo(SD_1)
        {
            // Step 1: Exit states until we reach `SELF_DESTRUCT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SD_1`.
            SD_1_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SD_1;
            this.ancestorEventHandler = null;
            return;
        } // end of behavior for SELF_DESTRUCT.InitialState
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state SD_1
    ////////////////////////////////////////////////////////////////////////////////

    private void SD_1_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_SD_1_exit;
        this.currentEventHandlers[(int)EventId.DO] = ptr_SD_1_do;

        // SD_1 behavior
        // uml: enter / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();
        } // end of behavior for SD_1

        // SD_1 behavior
        // uml: enter / { t1.Restart(); }
        {
            // Step 1: execute action `t1.Restart();`
            t1.Restart();
        } // end of behavior for SD_1

        // SD_1 behavior
        // uml: enter / { MenuHeader($"SELF DESTRUCT IN " + SelfDestructSeconds); }
        {
            // Step 1: execute action `MenuHeader($"SELF DESTRUCT IN " + SelfDestructSeconds);`
            MenuHeader($"SELF DESTRUCT IN " + SelfDestructSeconds);
        } // end of behavior for SD_1
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SD_1_exit = (UiSm sm) => sm.SD_1_exit();
    private void SD_1_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_SELF_DESTRUCT_exit;
        this.currentEventHandlers[(int)EventId.DO] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SD_1_do = (UiSm sm) => sm.SD_1_do();
    private void SD_1_do()
    {
        // No ancestor state handles `do` event.

        // SD_1 behavior
        // uml: DO [SelfDestructSeconds <= 0] / { SelfDestruct(); }
        if (SelfDestructSeconds <= 0)
        {
            // Step 1: execute action `SelfDestruct();`
            SelfDestruct();

            // Step 2: determine if ancestor gets to handle event next.
            // Don't consume special `do` event.
        } // end of behavior for SD_1

        // SD_1 behavior
        // uml: do [t1.Elapsed.Seconds >= 1] TransitionTo(SD_1)
        if (t1.Elapsed.Seconds >= 1)
        {
            // Step 1: Exit states until we reach `SELF_DESTRUCT` state (Least Common Ancestor for transition).
            SD_1_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SD_1`.
            SD_1_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SD_1;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for SD_1
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state SELF_DESTRUCT_OPTION
    ////////////////////////////////////////////////////////////////////////////////

    private void SELF_DESTRUCT_OPTION_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_SELF_DESTRUCT_OPTION_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_SELF_DESTRUCT_OPTION_down;
        this.currentEventHandlers[(int)EventId.PG_DOWN] = ptr_SELF_DESTRUCT_OPTION_pg_down;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_SELF_DESTRUCT_OPTION_right;
        this.currentEventHandlers[(int)EventId.UP] = ptr_SELF_DESTRUCT_OPTION_up;

        // SELF_DESTRUCT_OPTION behavior
        // uml: enter / { count = 0;\nMenuOption("Self Destruct?"); }
        {
            // Step 1: execute action `count = 0;\nMenuOption("Self Destruct?");`
            count = 0;
            MenuOption("Self Destruct?");
        } // end of behavior for SELF_DESTRUCT_OPTION
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_OPTION_exit = (UiSm sm) => sm.SELF_DESTRUCT_OPTION_exit();
    private void SELF_DESTRUCT_OPTION_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_SYSTEM_INFO_MENU_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_DOWN] = ptr_SYSTEM_INFO_MENU_pg_down;  // the next ancestor that handles this event is SYSTEM_INFO_MENU
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_OPTION_down = (UiSm sm) => sm.SELF_DESTRUCT_OPTION_down();
    private void SELF_DESTRUCT_OPTION_down()
    {
        // No ancestor state handles `down` event.

        // SELF_DESTRUCT_OPTION behavior
        // uml: DOWN, PG_DOWN / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
        } // end of behavior for SELF_DESTRUCT_OPTION
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_OPTION_pg_down = (UiSm sm) => sm.SELF_DESTRUCT_OPTION_pg_down();
    private void SELF_DESTRUCT_OPTION_pg_down()
    {
        // Setup handler for next ancestor that listens to `pg_down` event.
        this.ancestorEventHandler = ptr_SYSTEM_INFO_MENU_pg_down;

        // SELF_DESTRUCT_OPTION behavior
        // uml: DOWN, PG_DOWN / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for SELF_DESTRUCT_OPTION
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_OPTION_right = (UiSm sm) => sm.SELF_DESTRUCT_OPTION_right();
    private void SELF_DESTRUCT_OPTION_right()
    {
        // No ancestor state handles `right` event.

        // SELF_DESTRUCT_OPTION behavior
        // uml: 1. RIGHT / { count++; Beep(); }
        {
            // Step 1: execute action `count++; Beep();`
            count++; Beep();

            // Step 2: determine if ancestor gets to handle event next.
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
        } // end of behavior for SELF_DESTRUCT_OPTION

        // SELF_DESTRUCT_OPTION behavior
        // uml: RIGHT [count >= 5] TransitionTo(SELF_DESTRUCT)
        if (count >= 5)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            SELF_DESTRUCT_OPTION_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SELF_DESTRUCT`.
            SELF_DESTRUCT_enter();

            // Finish transition by calling pseudo state transition function.
            SELF_DESTRUCT_InitialState_transition();
            return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.
        } // end of behavior for SELF_DESTRUCT_OPTION
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SELF_DESTRUCT_OPTION_up = (UiSm sm) => sm.SELF_DESTRUCT_OPTION_up();
    private void SELF_DESTRUCT_OPTION_up()
    {
        // No ancestor state handles `up` event.

        // SELF_DESTRUCT_OPTION behavior
        // uml: UP TransitionTo(BURRITO_COUNT)
        {
            // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition).
            SELF_DESTRUCT_OPTION_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `BURRITO_COUNT`.
            BURRITO_COUNT_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.BURRITO_COUNT;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for SELF_DESTRUCT_OPTION
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state MAIN_MENU_INNER
    ////////////////////////////////////////////////////////////////////////////////

    private void MAIN_MENU_INNER_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_MAIN_MENU_INNER_exit;
        this.currentEventHandlers[(int)EventId.PG_DOWN] = ptr_MAIN_MENU_INNER_pg_down;
        this.currentEventHandlers[(int)EventId.PG_UP] = ptr_MAIN_MENU_INNER_pg_up;

        // MAIN_MENU_INNER behavior
        // uml: enter / { MenuHeader("Main Menu"); }
        {
            // Step 1: execute action `MenuHeader("Main Menu");`
            MenuHeader("Main Menu");
        } // end of behavior for MAIN_MENU_INNER
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_MAIN_MENU_INNER_exit = (UiSm sm) => sm.MAIN_MENU_INNER_exit();
    private void MAIN_MENU_INNER_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_MENU_exit;
        this.currentEventHandlers[(int)EventId.PG_DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_MAIN_MENU_INNER_pg_down = (UiSm sm) => sm.MAIN_MENU_INNER_pg_down();
    private void MAIN_MENU_INNER_pg_down()
    {
        // No ancestor state handles `pg_down` event.

        // MAIN_MENU_INNER behavior
        // uml: PG_DOWN TransitionTo(SYSTEM_INFO)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MAIN_MENU_INNER_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SYSTEM_INFO`.
            SYSTEM_INFO_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SYSTEM_INFO;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for MAIN_MENU_INNER
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_MAIN_MENU_INNER_pg_up = (UiSm sm) => sm.MAIN_MENU_INNER_pg_up();
    private void MAIN_MENU_INNER_pg_up()
    {
        // No ancestor state handles `pg_up` event.

        // MAIN_MENU_INNER behavior
        // uml: PG_UP TransitionTo(FOOD)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MAIN_MENU_INNER_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `FOOD`.
            FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.FOOD;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for MAIN_MENU_INNER
    }

    private void MAIN_MENU_INNER_InitialState_transition()
    {
        // MAIN_MENU_INNER.InitialState behavior
        // uml: TransitionTo(FOOD)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `FOOD`.
            FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.FOOD;
            this.ancestorEventHandler = null;
            return;
        } // end of behavior for MAIN_MENU_INNER.InitialState
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state DRINK
    ////////////////////////////////////////////////////////////////////////////////

    private void DRINK_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_DRINK_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_DRINK_down;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_DRINK_right;
        this.currentEventHandlers[(int)EventId.UP] = ptr_DRINK_up;

        // DRINK behavior
        // uml: enter / { MenuOption("Select Drink"); }
        {
            // Step 1: execute action `MenuOption("Select Drink");`
            MenuOption("Select Drink");
        } // end of behavior for DRINK
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_exit = (UiSm sm) => sm.DRINK_exit();
    private void DRINK_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_MAIN_MENU_INNER_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_down = (UiSm sm) => sm.DRINK_down();
    private void DRINK_down()
    {
        // No ancestor state handles `down` event.

        // DRINK behavior
        // uml: DOWN TransitionTo(SYSTEM_INFO)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition).
            DRINK_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SYSTEM_INFO`.
            SYSTEM_INFO_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.SYSTEM_INFO;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for DRINK
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_right = (UiSm sm) => sm.DRINK_right();
    private void DRINK_right()
    {
        // No ancestor state handles `right` event.

        // DRINK behavior
        // uml: RIGHT TransitionTo(DRINK_MENU)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DRINK_MENU`.
            ESC_CATCHER_1_enter();
            DRINK_MENU_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DRINK_MENU;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for DRINK
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_DRINK_up = (UiSm sm) => sm.DRINK_up();
    private void DRINK_up()
    {
        // No ancestor state handles `up` event.

        // DRINK behavior
        // uml: UP TransitionTo(FOOD)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition).
            DRINK_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `FOOD`.
            FOOD_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.FOOD;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for DRINK
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state FOOD
    ////////////////////////////////////////////////////////////////////////////////

    private void FOOD_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_FOOD_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_FOOD_down;
        this.currentEventHandlers[(int)EventId.PG_UP] = ptr_FOOD_pg_up;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_FOOD_right;
        this.currentEventHandlers[(int)EventId.UP] = ptr_FOOD_up;

        // FOOD behavior
        // uml: enter / { MenuOption("Select Food"); }
        {
            // Step 1: execute action `MenuOption("Select Food");`
            MenuOption("Select Food"); 
        } // end of behavior for FOOD
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_exit = (UiSm sm) => sm.FOOD_exit();
    private void FOOD_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_MAIN_MENU_INNER_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_UP] = ptr_MAIN_MENU_INNER_pg_up;  // the next ancestor that handles this event is MAIN_MENU_INNER
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_down = (UiSm sm) => sm.FOOD_down();
    private void FOOD_down()
    {
        // No ancestor state handles `down` event.

        // FOOD behavior
        // uml: DOWN TransitionTo(DRINK)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition).
            FOOD_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DRINK`.
            DRINK_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DRINK;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for FOOD
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_pg_up = (UiSm sm) => sm.FOOD_pg_up();
    private void FOOD_pg_up()
    {
        // Setup handler for next ancestor that listens to `pg_up` event.
        this.ancestorEventHandler = ptr_MAIN_MENU_INNER_pg_up;

        // FOOD behavior
        // uml: UP, PG_UP / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for FOOD
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_right = (UiSm sm) => sm.FOOD_right();
    private void FOOD_right()
    {
        // No ancestor state handles `right` event.

        // FOOD behavior
        // uml: RIGHT TransitionTo(FOOD_MENU)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `FOOD_MENU`.
            ESC_CATCHER_1_enter();
            FOOD_MENU_enter();

            // FOOD_MENU.InitialState behavior
            // uml: TransitionTo(EAT_BURRITO)
            {
                // Step 1: Exit states until we reach `FOOD_MENU` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                // Step 2: Transition action: ``.

                // Step 3: Enter/move towards transition target `EAT_BURRITO`.
                EAT_BURRITO_enter();

                // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                this.stateId = StateId.EAT_BURRITO;
                // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
                return;
            } // end of behavior for FOOD_MENU.InitialState
        } // end of behavior for FOOD
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_FOOD_up = (UiSm sm) => sm.FOOD_up();
    private void FOOD_up()
    {
        // No ancestor state handles `up` event.

        // FOOD behavior
        // uml: UP, PG_UP / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
        } // end of behavior for FOOD
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state SYSTEM_INFO
    ////////////////////////////////////////////////////////////////////////////////

    private void SYSTEM_INFO_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_SYSTEM_INFO_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = ptr_SYSTEM_INFO_down;
        this.currentEventHandlers[(int)EventId.PG_DOWN] = ptr_SYSTEM_INFO_pg_down;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_SYSTEM_INFO_right;
        this.currentEventHandlers[(int)EventId.UP] = ptr_SYSTEM_INFO_up;

        // SYSTEM_INFO behavior
        // uml: enter / { MenuOption("System Info"); }
        {
            // Step 1: execute action `MenuOption("System Info");`
            MenuOption("System Info");
        } // end of behavior for SYSTEM_INFO
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_exit = (UiSm sm) => sm.SYSTEM_INFO_exit();
    private void SYSTEM_INFO_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_MAIN_MENU_INNER_exit;
        this.currentEventHandlers[(int)EventId.DOWN] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.PG_DOWN] = ptr_MAIN_MENU_INNER_pg_down;  // the next ancestor that handles this event is MAIN_MENU_INNER
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
        this.currentEventHandlers[(int)EventId.UP] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_down = (UiSm sm) => sm.SYSTEM_INFO_down();
    private void SYSTEM_INFO_down()
    {
        // No ancestor state handles `down` event.

        // SYSTEM_INFO behavior
        // uml: DOWN, PG_DOWN / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
        } // end of behavior for SYSTEM_INFO
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_pg_down = (UiSm sm) => sm.SYSTEM_INFO_pg_down();
    private void SYSTEM_INFO_pg_down()
    {
        // Setup handler for next ancestor that listens to `pg_down` event.
        this.ancestorEventHandler = ptr_MAIN_MENU_INNER_pg_down;

        // SYSTEM_INFO behavior
        // uml: DOWN, PG_DOWN / { Beep(); }
        {
            // Step 1: execute action `Beep();`
            Beep();

            // Step 2: determine if ancestor gets to handle event next.
            this.ancestorEventHandler = null;  // consume event
        } // end of behavior for SYSTEM_INFO
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_right = (UiSm sm) => sm.SYSTEM_INFO_right();
    private void SYSTEM_INFO_right()
    {
        // No ancestor state handles `right` event.

        // SYSTEM_INFO behavior
        // uml: RIGHT TransitionTo(SYSTEM_INFO_MENU)
        {
            // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition).
            ExitUpToStateHandler(ptr_MENU_exit);

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `SYSTEM_INFO_MENU`.
            ESC_CATCHER_1_enter();
            SYSTEM_INFO_MENU_enter();

            // SYSTEM_INFO_MENU.InitialState behavior
            // uml: / { MenuHeader("System Info"); } TransitionTo(DISTANCE)
            {
                // Step 1: Exit states until we reach `SYSTEM_INFO_MENU` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                // Step 2: Transition action: `MenuHeader("System Info");`.
                MenuHeader("System Info");

                // Step 3: Enter/move towards transition target `DISTANCE`.
                DISTANCE_enter();

                // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                this.stateId = StateId.DISTANCE;
                // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
                return;
            } // end of behavior for SYSTEM_INFO_MENU.InitialState
        } // end of behavior for SYSTEM_INFO
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SYSTEM_INFO_up = (UiSm sm) => sm.SYSTEM_INFO_up();
    private void SYSTEM_INFO_up()
    {
        // No ancestor state handles `up` event.

        // SYSTEM_INFO behavior
        // uml: UP TransitionTo(DRINK)
        {
            // Step 1: Exit states until we reach `MAIN_MENU_INNER` state (Least Common Ancestor for transition).
            SYSTEM_INFO_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `DRINK`.
            DRINK_enter();

            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.stateId = StateId.DRINK;
            // No ancestor handles event. Can skip nulling `ancestorEventHandler`.
            return;
        } // end of behavior for SYSTEM_INFO
    }


    ////////////////////////////////////////////////////////////////////////////////
    // event handlers for state SPLASH_SCREEN
    ////////////////////////////////////////////////////////////////////////////////

    private void SPLASH_SCREEN_enter()
    {
        // setup trigger/event handlers
        this.currentStateExitHandler = ptr_SPLASH_SCREEN_exit;
        this.currentEventHandlers[(int)EventId.RIGHT] = ptr_SPLASH_SCREEN_right;

        // SPLASH_SCREEN behavior
        // uml: enter / { ShowSplashScreen(); }
        {
            // Step 1: execute action `ShowSplashScreen();`
            ShowSplashScreen();
        } // end of behavior for SPLASH_SCREEN
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SPLASH_SCREEN_exit = (UiSm sm) => sm.SPLASH_SCREEN_exit();
    private void SPLASH_SCREEN_exit()
    {
        // adjust function pointers for this state's exit
        this.currentStateExitHandler = ptr_ROOT_exit;
        this.currentEventHandlers[(int)EventId.RIGHT] = null;  // no ancestor listens to this event
    }

    // static delegate to avoid implicit conversion and garbage collection
    private static readonly Func ptr_SPLASH_SCREEN_right = (UiSm sm) => sm.SPLASH_SCREEN_right();
    private void SPLASH_SCREEN_right()
    {
        // No ancestor state handles `right` event.

        // SPLASH_SCREEN behavior
        // uml: RIGHT TransitionTo(MENU)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
            SPLASH_SCREEN_exit();

            // Step 2: Transition action: ``.

            // Step 3: Enter/move towards transition target `MENU`.
            MENU_enter();

            // MENU.InitialState behavior
            // uml: TransitionTo(MAIN_MENU_INNER)
            {
                // Step 1: Exit states until we reach `MENU` state (Least Common Ancestor for transition). Already at LCA, no exiting required.

                // Step 2: Transition action: ``.

                // Step 3: Enter/move towards transition target `MAIN_MENU_INNER`.
                MAIN_MENU_INNER_enter();

                // Finish transition by calling pseudo state transition function.
                MAIN_MENU_INNER_InitialState_transition();
                return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.
            } // end of behavior for MENU.InitialState
        } // end of behavior for SPLASH_SCREEN
    }

    // Thread safe.
    public static string StateIdToString(StateId id)
    {
        switch (id)
        {
            case StateId.ROOT: return "ROOT";
            case StateId.DONE: return "DONE";
            case StateId.MENU: return "MENU";
            case StateId.ESC_CATCHER_1: return "ESC_CATCHER_1";
            case StateId.DRINK_MENU: return "DRINK_MENU";
            case StateId.FOOD_MENU: return "FOOD_MENU";
            case StateId.EAT_BURRITO: return "EAT_BURRITO";
            case StateId.EAT_MRE: return "EAT_MRE";
            case StateId.EAT_SLUDGE: return "EAT_SLUDGE";
            case StateId.EVENT_EATER_2: return "EVENT_EATER_2";
            case StateId.COOKING_FOOD: return "COOKING_FOOD";
            case StateId.COOKING_FOOD1: return "COOKING_FOOD1";
            case StateId.SYSTEM_INFO_MENU: return "SYSTEM_INFO_MENU";
            case StateId.BURRITO_COUNT: return "BURRITO_COUNT";
            case StateId.DISTANCE: return "DISTANCE";
            case StateId.SELF_DESTRUCT: return "SELF_DESTRUCT";
            case StateId.SD_1: return "SD_1";
            case StateId.SELF_DESTRUCT_OPTION: return "SELF_DESTRUCT_OPTION";
            case StateId.MAIN_MENU_INNER: return "MAIN_MENU_INNER";
            case StateId.DRINK: return "DRINK";
            case StateId.FOOD: return "FOOD";
            case StateId.SYSTEM_INFO: return "SYSTEM_INFO";
            case StateId.SPLASH_SCREEN: return "SPLASH_SCREEN";
            default: return "?";
        }
    }

    // Thread safe.
    public static string EventIdToString(EventId id)
    {
        switch (id)
        {
            case EventId.DO: return "DO";
            case EventId.DOWN: return "DOWN";
            case EventId.ESC: return "ESC";
            case EventId.LEFT: return "LEFT";
            case EventId.PG_DOWN: return "PG_DOWN";
            case EventId.PG_UP: return "PG_UP";
            case EventId.RIGHT: return "RIGHT";
            case EventId.UP: return "UP";
            default: return "?";
        }
    }
}

