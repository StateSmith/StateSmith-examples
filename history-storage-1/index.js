// key used for browser local storage
const STORAGE_KEY_STATEMACHINE_HISTORY = "ordering-step-state";

// pizza state machine
let pizzaSm = new PizzaSm();

// This is the interesting part!
setHistoryStateFromLocalStorage();

// Now that history state is updated, start the state machine.
pizzaSm.start();

addButtonHandlers();


///////////////// STORAGE STUFF /////////////////


function setHistoryStateFromLocalStorage() {
    // We store the name (not the integer value) of the tracked history state.
    // NOTE!!! We don't save the history state id integer value, because if we add/remove states
    // in the future, the id integer values may change. The names are more likely to stay the same.
    // If this was to be used in a production app, some tests or validation should be added to protect against changes.
    let currentHistoryStateName = getStoredHistoryStateName();

    // Update history state id from storage/database.
    // Must happen before `pizzaSm.start()` is called.
    pizzaSm.vars.PizzaSm_history = getHistoryStateIdFromName(currentHistoryStateName);
    // NOTE!!! The path to the history field (`.vars.PizzaSm_history`) may change when we go to version 1.0.0 (in 2025?).
    // This functionality will still be supported, but you may need to use an accessor method instead someday. Simple update.
}

function getStoredHistoryStateName() {
    return localStorage.getItem(STORAGE_KEY_STATEMACHINE_HISTORY);
}

function persistHistoryState() {
    let historyStateName = getHistoryStateNameFromId(pizzaSm.vars.PizzaSm_history);
    localStorage.setItem(STORAGE_KEY_STATEMACHINE_HISTORY, historyStateName);
}

// finds the state name from id.
// If you were to do this in C/C++, you could just statically map them
// all in a switch statement and add a static assert to ensure that you didn't miss any.
function getHistoryStateNameFromId(id) {
    return Object.keys(PizzaSm.PizzaSm_HistoryId).find(key => PizzaSm.PizzaSm_HistoryId[key] === id);
}

function getHistoryStateIdFromName(name) {
    let id = PizzaSm.PizzaSm_HistoryId[name];
    if (id === undefined) {
        id = 0; // default to diagram default if not found.
    }

    return  id;
}


///////////////// UI STUFF /////////////////


function showStageDiv(div_id) {
    // hide all ordering divs other than desired one
    document.querySelectorAll("div.ordering-div").forEach((div) => {
        if (div.id == div_id)
            div.style.display = "block";
        else
            div.style.display = "none";
    });
}

function ask_crust() {
    showStageDiv("ask-crust");
}

function ask_size() {
    showStageDiv("ask-size");
}

function ask_toppings() {
    showStageDiv("ask-toppings");
}

function ask_toppings() {
    showStageDiv("ask-toppings");
}

function ask_review_order() {
    showStageDiv("ask-review-order");
}

function ask_confirmation() {
    showStageDiv("ask-confirmation");
}

function show_ordered() {
    showStageDiv("show-ordered");
}

function addButtonHandlers() {
    document.querySelectorAll("button.btn-next").forEach((btn) => {
        btn.addEventListener("click", (e) => {
            dispatchEventToSmSaveState(PizzaSm.EventId.NEXT);
        });
    });

    document.querySelectorAll("button.btn-back").forEach((btn) => {
        btn.addEventListener("click", (e) => {
            dispatchEventToSmSaveState(PizzaSm.EventId.BACK);
        });
    });

    document.querySelectorAll("button.btn-cancel").forEach((btn) => {
        btn.addEventListener("click", (e) => {
            dispatchEventToSmSaveState(PizzaSm.EventId.CANCEL);
        });
    });
}

function dispatchEventToSmSaveState(eventId) {
    pizzaSm.dispatchEvent(eventId);
    persistHistoryState();
    // NOTE! we save here (after dispatch) because it won't work as part of a state's entry handler.
    // This is because user enter event handler code happens before the history state is updated.
    // You can't do something like `enter / saveHistoryState()` because the history state is not updated yet.
}