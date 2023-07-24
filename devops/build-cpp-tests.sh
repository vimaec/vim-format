#!/bin/bash

SRC=`pwd`/../src/cpp/vim

echo Building C++ tests...

g++ $SRC/test.cpp -o $SRC/test_app -std=c++17 |
  while IFS= read -r line
  do
    echo "$line"
  done

chmod +x $SRC/test_app