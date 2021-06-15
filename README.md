# KaleidoscopeWF
Nibble Magazine Vol 1 Issue 1 Kaledoscope

A C# implementation for the first code in the first number of Nibble Magazine. written in Applesoft BASIC:
You are free to improve the C# code.

010 HGR
020 HCOLOR=INT(RND(1)*8)
030 X=INT(RND(1)*8)
040 FOR A=0 TO 139 STEP X+2
050 HPLOT A,0 TO 139-A,79
060 HPLOT 279-A,0 TO 140+A,79
070 HPLOT A,159 TO 139-A,80
080 HPLOT 279-A,159 TO 140+A,80
090 NEXT A
100 FOR B=0 TO 79 STEP X+2
110 HPLOT 0,79-B TO 139,B
120 HPLOT 140,B TO 279,79-B
130 HPLOT 0,80+B TO 139,159-B
140 HPLOT 279,80+B TO 140,159-B
150 NEXT B
160 FOR Z=1 TO 1500 : NEXT Z
17 GOTO 20
