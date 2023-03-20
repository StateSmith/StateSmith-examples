/** @type {OnOffSm} */
let sm = new OnOffSm();
let highlighter = new smHighlighter();

function start()
{
    print_start();
    sm.start();
    trace("");
}

function print_divider() {
    trace("===================================================");
}

function print_start() {
    trace("<b>Start Statemachine<b>");
    print_divider();
}

function print_dispatch_event_name(event_name) {
    trace(`Dispatch event ${event_name}`);
    print_divider();
}

function trace_guard(msg, condition) {
    let toAppend = "";
    
    if (condition)
    {
        toAppend = "Behavior running.";
    }
    else
    {
        toAppend = "Behavior skipped.";
    }

    trace(msg + " " + toAppend);
    return condition;
}

/**
 * @param {string} msg
 */
function trace(msg) {
    msg = highlighter.highlight(msg);
    let div = document.getElementById("sm-output");
    div.innerHTML += msg + "<br>";
    div.scrollTop = div.scrollHeight; // scroll to bottom
}

function dispatchEvent(/** @type {number} */ eventId)
{
    print_dispatch_event_name(sm.eventIdToString(eventId));
    sm.dispatchEvent(eventId);
    trace("");
}

window.addEventListener('DOMContentLoaded', () => {
    start();

    document.getElementById("INCREASE").onclick = () => {
        dispatchEvent(OnOffSm.EventId.INCREASE);
    };
    
    document.getElementById("DIM").onclick = () => {
        dispatchEvent(OnOffSm.EventId.DIM);
    };
});
