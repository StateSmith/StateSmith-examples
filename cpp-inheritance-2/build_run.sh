#!/bin/bash
cd "$(dirname "$0")" # change to current script directory

# exit when any command fails
set -e

# compile code
echo Compiling with GCC g++
g++ -std=c++11 -g -Wall -Wextra -Wshadow -Wnon-virtual-dtor -pedantic -I . -I ./test  *.cpp  ./test/*.cpp  -o a.out
# you can also use a newer c++ standard

# run the program
./a.out