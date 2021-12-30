*-----------------------------------------------------------
* Title      :
* Written by :
* Date       :
* Description:
*-----------------------------------------------------------
    ORG    $1000
START:                  ; first instruction of program
; At exit, D1 = Part 1 answer and D2 = Part 2 answer

    LEA     READINGS, A0  ; Load the readings
    MOVE.W  #1999, D0   ; Number of readings - 1
    MOVE.W  #0, D1      ; Counter for depth increases
    MOVE.W  #32767, D4  ; Make sure the 1st reading isn't counted 
PART1:
    MOVE.W  (A0)+, D3   ; Load depth reading and move to next
    CMP.W   D4, D3      ; Compare last reading with the current
    BLE.W   NotDeeper1  ; Skip if it's the same or smaller
    
    ADD.W   #1, D1      ; Increment the depth increase counter
NotDeeper1:    
    EXG     D4, D3      ; Move current reading to last reading
    DBRA    D0, PART1   ; Loop for the rest of the readings
    LEA     READINGS, A0
    MOVE.W  #1997, D0
    MOVE.W  #0, D2      ; Counter for depth increases
    MOVE.L  #32767, D4  ; Make sure the 1st window isn't counted
PART2:
    MOVE.W  (A0)+, D3   ; Load depth reading and move to next
    EXT.L   D3          ; Extend to a DWORD to stop overflows
    ADD.W   (A0), D3    ; Add the 2nd reading in the window
    ADD.W   2(A0), D3   ; Add the 3rd reading in the window
    CMP.L   D4, D3      ; Compare last window with the current
    BLE.W   NotDeeper2  ; Skip if it's the same or smaller
    ADD.W   #1, D2      ; Increment the depth increase counter
NotDeeper2:    
    EXG     D4, D3      ; Save the current window
    DBRA    D0, PART2   ; Loop through the readings

    SIMHALT             ; halt simulator
* Put variables and constants here
READINGS:
    INCBIN  "readings.bin"

    END    START        ; last line of source


*~Font name~Courier New~
*~Font size~10~
*~Tab type~1~
*~Tab size~4~
