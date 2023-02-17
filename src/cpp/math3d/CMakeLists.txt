# CMakeList.txt : CMake project for vim_math3d, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.10)

set (PROJECT "vim_math_3d")
project (${PROJECT})

# set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /GR- /wd4091 /wd4065 /MP /bigobj")
# set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /GR /wd4091 /wd4065 /MP /bigobj")
# set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /O2 /GL /arch:AVX2 /fp:fast")

# Add source to this project's executable.
set(SRCS 
	"vim_math3d.cpp" 
	"vim_math3d.h" 
)

add_executable (${PROJECT} ${SRCS})

set_property(TARGET ${PROJECT} PROPERTY CXX_STANDARD 17)

# TODO: Add tests and install targets if needed.
