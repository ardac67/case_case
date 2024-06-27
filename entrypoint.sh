#!/bin/bash
until dotnet ef database update; do
sleep 1
done
exec dotnet initproject.dll
