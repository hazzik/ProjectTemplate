@echo off

if exist .git goto exists

call :x git init
call :x git add .
call :x git commit -m "Initial project structure"
echo Your git repository has been created successfully.
pause
goto :eof

:exists
echo Your git repository already exists.
pause
goto :eof

:x
echo %*
echo.
call %*
goto :eof
