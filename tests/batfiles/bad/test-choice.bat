@echo off

:loop
	echo %random%
	choice /T 1 /D y >nul
goto loop