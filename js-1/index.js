/** @type {OnOffSm} */
let sm = new OnOffSm();

sm.start();

window.addEventListener('DOMContentLoaded', () => {
    document.getElementById("INCREASE").onclick = () => {
        sm.dispatchEvent(OnOffSm.EventId.INCREASE);
    };
    
    document.getElementById("DIM").onclick = () => {
        sm.dispatchEvent(OnOffSm.EventId.DIM);
    };
});
