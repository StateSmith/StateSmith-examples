import sys
from LightSm import LightSm

def read_input_run_sm(sm):
    print("\nCurrent state: " + LightSm.stateIdToString(sm.stateId))
    print("Please type 't' to toggle: ", end='')

    line = input().strip()
    if len(line) > 0:
        c = line[0]  # Get the first character
        run_sm_for_char(sm, c)
    else:
        run_sm_for_char(sm, '\0')


def run_sm_for_char(sm, c):
    valid_input = True
    event_id = None

    if c == 't':
        event_id = LightSm.EventId.TOGGLE
    else:
        valid_input = False

    if valid_input:
        print("Dispatching event: " + LightSm.eventIdToString(event_id))
        sm.dispatchEvent(event_id)
    else:
        print("Invalid input. Not running state machine.", file=sys.stderr)


if __name__ == "__main__":
    sm = LightSm()

    print("USAGE:")
    print("  type t <enter> to send TOGGLE event to state machine.")
    print("")
    print("Hit <enter> to start")
    input()  # Wait for user to press Enter

    sm.start()

    while True:
        read_input_run_sm(sm)
