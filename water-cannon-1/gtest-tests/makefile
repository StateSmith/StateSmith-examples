CC = gcc
CXX = g++

INCLUDES = -I ../src -I .

CFLAGS = -std=c99 -Wall -Wextra -g $(INCLUDES)
CXXFLAGS =        -Wall -Wextra -g $(GTEST_INCLUDES) $(INCLUDES)

C_SRC = ../src

##################### GTEST #####################

GTEST_LINK = -lpthread
GTEST_SRC = lib/googletest/src/gtest-all.cc
GTEST_INCLUDES = -I lib/googletest/include -I lib/googletest/

OBJS = main.o gtest-all.o WaterCannonTestBasic.o WaterCannonTestBetter.o FakeDisplay.o WaterCannon.o WaterCannonSm.o Screens.o DebugLog.o

all: test_program1 test

test_program1: $(OBJS)
	$(CXX) $(CXXFLAGS) $^ -o $@ $(GTEST_LINK)

##################### C++ FILES #####################

FakeDisplay.o: FakeDisplay.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

WaterCannonTest.o: WaterCannonTest.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

main.o: main.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

gtest-all.o: $(GTEST_SRC)
	$(CXX) $(CXXFLAGS) -c $< -o $@


##################### C FILES #####################

WaterCannon.o: $(C_SRC)/WaterCannon.c
	$(CC) $(CFLAGS) -c $< -o $@
WaterCannonSm.o: $(C_SRC)/WaterCannonSm.c
	$(CC) $(CFLAGS) -c $< -o $@
Screens.o: $(C_SRC)/Screens.c
	$(CC) $(CFLAGS) -c $< -o $@
DebugLog.o: $(C_SRC)/DebugLog.c
	$(CC) $(CFLAGS) -c $< -o $@


test: test_program1
	./test_program1

clean:
	rm -rf test_program1 *.o

.PHONY: all clean
