@echo off
setlocal enabledelayedexpansion

set "yamlFiles="
for %%i in (*.yaml) do (
    set "yamlFiles=!yamlFiles!,%%i"
)

set "yamlFiles=%yamlFiles:~1%"
echo %yamlFiles%

kubectl apply -f %yamlFiles%