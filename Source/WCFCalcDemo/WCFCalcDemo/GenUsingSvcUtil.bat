@echo off
REM Run the Server Host
REM Open a Command Prompt from withing Visual Studio and execute

svcutil.exe /language:cs /out:SvcUtilGeneratedProxy.cs /config:SvcUtilGeneratedApp.config http://localhost:8002/CalculatorService

Pause

