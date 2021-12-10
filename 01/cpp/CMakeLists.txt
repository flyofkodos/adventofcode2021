cmake_minimum_required(VERSION 3.0.0)
project(c VERSION 0.1.0)

include(CTest)
enable_testing()

add_executable(output sonar.cpp)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)

add_custom_command(
        TARGET output POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy
                ${CMAKE_SOURCE_DIR}/input.txt
                Debug/input.txt)