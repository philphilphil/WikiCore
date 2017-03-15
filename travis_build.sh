#!/usr/bin/env bash
dotnet restore  --infer-runtimes && dotnet build **/project.json