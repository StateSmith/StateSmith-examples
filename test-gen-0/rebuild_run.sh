#!/bin/bash
cd "$(dirname "$0")" # change to current script directory

# exit when any command fails
set -e

# compile code and run it
./code_gen_compile.sh
./a.out
