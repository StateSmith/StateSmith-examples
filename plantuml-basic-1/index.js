const sm = new LightSm();
let event_count = 0;

document.addEventListener('DOMContentLoaded', () => {
    sm.start();

    document.getElementById("increase").onclick = () => sendEventToSm(LightSm.EventId.INCREASE);
    document.getElementById("dim").onclick      = () => sendEventToSm(LightSm.EventId.DIM);
    document.getElementById("off").onclick      = () => sendEventToSm(LightSm.EventId.OFF);
});

/**
 * @param {number} eventId
 */
function sendEventToSm(eventId) {
    event_count++;
    document.getElementById("event-count").innerText = event_count + "";
    sm.dispatchEvent(eventId);
}

/**
 * @param {string} message
 */
function outputText(message) {
    const output = document.getElementById("output");
    output.append(message + "\n");
}

function lightBlue() {
    outputText("BLUE");
}

function lightYellow() {
    outputText("YELLOW");
}

function lightRed() {
    outputText("RED");
}
