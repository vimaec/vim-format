#!/bin/bash

SRC=`pwd`/../src/cpp/vim

bash build-cpp-tests.sh

echo Running C++ tests...

$SRC/test_app |
  while IFS= read -r line
  do
    echo "$line"
  done

echo Cleaning up...

rm $SRC/test_app