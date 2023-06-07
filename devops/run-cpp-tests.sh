#!/bin/bash

SRC=`pwd`/../src/cpp/vim

bash build-cpp-tests.sh

echo Running C++ tests...
$SRC/test_app
CODE=$?

echo Cleaning up...
rm $SRC/test_app

exit $CODE