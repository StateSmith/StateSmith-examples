// pizza state machine
let pizzaSm = new PizzaSm();

// This is the interesting part! See storage_stuff.js for more details.
setHistoryStateFromLocalStorage(pizzaSm);

// Now that history state is updated, start the state machine.
pizzaSm.start();

addButtonHandlers();



////////////////////////////////// UI STUFF //////////////////////////////////

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
