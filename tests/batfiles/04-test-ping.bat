@echo off

:loop
	echo %random%
	ping -n 3 127.0.0.1 >nul
goto loop