class LightBulb():
    # ANSI escape codes for colors
    RESET =  "\033[0m"
    BLUE =   "\033[0;34m"
    YELLOW = "\033[0;33m"
    RED =    "\033[0;31m"

    def __init__(self) -> None:
        self.count = 0
        pass

    def set(self, desired_status):
        if (not desired_status):
            print(LightBulb.BLUE + "Light is: OFF" + LightBulb.RESET)
        else:
            print(LightBulb.YELLOW + "Light is: ON" + LightBulb.RESET)
            print(LightBulb.YELLOW + "Count: " + str(self.count) + LightBulb.RESET)
