@echo off
setlocal enabledelayedexpansion

:: Go one level up
cd ..

:: Initialize output file
set output_file=%~dp0cs_files.txt

:: Set directories to exclude
set exclude_dirs=obj,Debug,bin

:: Iterate through all subfolders
for /d /r %%d in (*) do (
    :: Skip .git and excluded directories
    set "dir=%%~nxd"
    if "!dir!" neq ".git" if "!exclude_dirs!" neq "!dir!" (
        :: Iterate through all .cs files in the subfolder
        for /f "delims=" %%f in ("%%d\*.cs") do (
            :: Check if file exists before appending
            if exist "%%f" (
                set "file_path=%%~dpf"
                :: Skip files in excluded directories
                if "!file_path!" neq "!exclude_dirs!" (
                    echo %%~dpnxnf >> "%output_file%"
                )
            )
        )
        :: Recursively search subfolders
        call :search_subfolders "%%d"
    )
)

:: Recursive search function
:search_subfolders
set "subfolder=%~1"
for /d /r %%s in ("!subfolder!\*") do (
    set "dir=%%~nxs"
    set "parent_dir=%%~dps"
    :: Skip excluded directories and their subdirectories
    if "!parent_dir!" neq "!exclude_dirs!" if "!dir!" neq "!exclude_dirs!" (
        :: Iterate through all .cs files in the subfolder
        for /f "delims=" %%f in ("%%s\*.cs") do (
            if exist "%%f" (
                echo %%~dpnxnf >> "%output_file%"
            )
        )
        :: Recursively search subfolders
        call :search_subfolders "%%s"
    )
)

echo Files listed successfully!