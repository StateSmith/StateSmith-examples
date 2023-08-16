// This is a fake Display file for testing purposes.
// It captures the output of Display_header() and Display_sub() in two global variables.
#include <string>

extern "C" {
    // Including inside an extern "C" block is not recommended if your production code is a mix of c/++
    // Fine if your production code is C only though and you want to use C++ for testing.
    // #include "Display.h"
}

class FakeDisplay {
public:
    static void reset() {
        header = "";
        sub = "";
    }

    static std::string header;
    static std::string sub;
};
