echo off
cls 

setlocal enabledelayedexpansion

SET VERSION=1.0
SET SCRIPT_DIR=%~dp0
SET WORKING_DIR=%cd%
SET BUILD_TYPE=
SET ARC=x64

for %%x in (%*) do (
	if "%%x"=="help" (
		SET BUILD_TYPE=help
	)
	if "%%x"=="h" (
		SET BUILD_TYPE=help
	)
	if "%%x"=="zip" (
		SET BUILD_TYPE=zip
	)
	if "%%x"=="exe" (
		SET BUILD_TYPE=exe
	)
	if "%%x"=="z" (
		SET BUILD_TYPE=zip
	)
	if "%%x"=="x" (
		SET BUILD_TYPE=exe
	)
	if "%%x"=="86" (
		SET ARC=x86
	)
	if "%%x"=="64" (
		SET ARC=x64
	)
	if "%%x"=="x86" (
		SET ARC=x86
	)
	if "%%x"=="x64" (
		SET ARC=x64
	)
)
cd !SCRIPT_DIR!

if "!BUILD_TYPE!"=="" (
	echo Error: you need to specify the build type (exe or zip^)
	SET BUILD_TYPE=help
)

if "!BUILD_TYPE!"=="help" (
	call:help
)

if "!BUILD_TYPE!"=="exe" (
	call:build_exe_installer
)

if "!BUILD_TYPE!"=="zip" (
	call:build_zip_archive
)

:end_program
cd !WORKING_DIR!
exit /b 0

:build_exe_installer
	SET FOUND_ISCC=false
	for /F "tokens=* USEBACKQ" %%F in (`iscc.exe 2^> nul`) do SET FOUND_ISCC=true 
	if !FOUND_ISCC!==false (
		SET /p ISCC_FOLDER=Inno Script(iscc.exe^) not found in path. Enter Inno Setup folder: 
		if !ISCC_FOLDER!==close (goto:end_program)
		SET PATH=!PATH!;!ISCC_FOLDER!
		call:build_exe_installer
	)
	iscc.exe ./devjammer_!ARC!.iss 

	goto:end_program

:build_zip_archive
	SET FOUND_ZIPPER=false
	for /F "tokens=* USEBACKQ" %%F in (`zip.bat 2^> nul`) do SET FOUND_ZIPPER=true 
	if !FOUND_ZIPPER!==false (
		SET /p ZIPPER_FOLDER=zip.bat not found in path. Enter Cronux(https://thecarisma.github.io/Cronux^) folder: 
		if !ZIPPER_FOLDER!==close (goto:end_program)
		SET PATH=!PATH!;!ZIPPER_FOLDER!
		call:build_zip_archive
	)
	if not exist ".\build\" (mkdir .\build\)
	call zip.bat .\build\devjammer-!VERSION!-!ARC!.zip ..\bin\!ARC!\Release\devjammer.exe .\..\README.MD ..\LICENSE
	
	goto:end_program

:help
	echo Usage: build.bat [BUILD_TYPE] [ARC]
	echo [BUILD_TYPE]
	echo 	z zip    build a distributable zip arcive
	echo 	x exe    build a executable installer (Inno Setup Required)
	echo 	h help   print this help message
	echo [ARC]
	echo 	86 x32   build 32 bit distributable package
	echo 	64 x64   build 64 bit distributable package 
	
	exit /b 0