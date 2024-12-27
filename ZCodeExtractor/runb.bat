@echo off
setlocal enabledelayedexpansion

:: Set the root path
set "rootPath=C:\personal\dsa"

:: Set the output file
set "outputFile=C:\personal\dsa\ZCodeExtractor\AllCode.cs"

:: Clear the output file if it exists
echo. > "%outputFile%" 2>nul

:: Function to process files
:process_files
for /r "%rootPath%" %%f in (*.cs) do (
    set "relativePath=%%f"
    set "relativePath=!relativePath:%rootPath%\=!"

    :: Ignore files in obj directories
    echo !relativePath! | findstr /i /r /c:"\\obj\\" >nul
    if errorlevel 1 (
        if exist "%%f" (
            echo Contents of %%f: >> "%outputFile%" 2>nul
            type "%%f" >> "%outputFile%" 2>nul
            echo. >> "%outputFile%" 2>nul
            echo ---------------------------------------------------------------------------- >> "%outputFile%" 2>nul
        )
    )
)
goto :eof

:: Start processing files
call :process_files

echo All files have been processed. Output saved to %outputFile%. >nul 2>&1
