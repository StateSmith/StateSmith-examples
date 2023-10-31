const STORAGE_KEY_STATEMACHINE_HISTORY = "ordering-step-state";

let pizzaSm = new PizzaSm();

// Name of the stored history state.
// NOTE!!! We don't save the history state id, because if we add/remove states
// in the future, the id's will change and we'll lose the history. The names are more likely to stay the same.
// If this was to be used in a production app, some tests or validation should be added here.
let currentHistoryStateName = localStorage.getItem(STORAGE_KEY_STATEMACHINE_HISTORY);
pizzaSm.vars.PizzaSm_history = getHistoryStateIdFromName(currentHistoryStateName);
pizzaSm.start();

function dispatchEvent(eventId) {
    pizzaSm.dispatchEvent(eventId);
    persistHistoryState(); // NOTE! we shouldn't save history state as part of state entry because the event handler happens before the history state is updated.
}

document.querySelectorAll("button.btn-next").forEach((btn) => {
    btn.addEventListener("click", (e) => {
        dispatchEvent(PizzaSm.EventId.NEXT);
    });
});

document.querySelectorAll("button.btn-back").forEach((btn) => {
    btn.addEventListener("click", (e) => {
        dispatchEvent(PizzaSm.EventId.BACK);
    });
});

document.querySelectorAll("button.btn-cancel").forEach((btn) => {
    btn.addEventListener("click", (e) => {
        dispatchEvent(PizzaSm.EventId.CANCEL);
    });
});

function show_ordering_div_screen(div_id) {
    document.querySelectorAll("div.ordering-div").forEach((div) => {
        if (div.id == div_id)
            div.style.display = "block";
        else
            div.style.display = "none";
    });
}

function persistHistoryState() {
    let historyStateName = getHistoryStateNameFromId(pizzaSm.vars.PizzaSm_history);
    localStorage.setItem(STORAGE_KEY_STATEMACHINE_HISTORY, historyStateName);
}

function ask_crust() {
    show_ordering_div_screen("ask-crust");
}

function ask_size() {
    show_ordering_div_screen("ask-size");
}

function ask_toppings() {
    show_ordering_div_screen("ask-toppings");
}

function ask_toppings() {
    show_ordering_div_screen("ask-toppings");
}

function ask_review_order() {
    show_ordering_div_screen("ask-review-order");
}

function ask_confirmation() {
    show_ordering_div_screen("ask-confirmation");
}

function show_ordered() {
    show_ordering_div_screen("show-ordered");
}

// finds the state name from id.
// If you were to do this in C/C++, you could just statically map them
// all in a switch statement and add a static assert that you didn't miss any.
function getHistoryStateNameFromId(id) {
    return Object.keys(PizzaSm.PizzaSm_HistoryId).find(key => PizzaSm.PizzaSm_HistoryId[key] === id);
}

function getHistoryStateIdFromName(name) {
    let id = PizzaSm.PizzaSm_HistoryId[name];
    if (id === undefined) {
        id = 0; // default
        // throw new Error("Invalid history state name");
    }

    return  id;
}