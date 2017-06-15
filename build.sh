#!/bin/bash

VERSION=`git describe --tags --always --long | cut -d '-' -f 1-2 | sed 's/-/./g'`

echo Version is $VERSION

dotnet restore
msbuild
mkdir -p artifacts
nuget pack src/Syncromatics.Clients.Metro.Api/Syncromatics.Clients.Metro.Api.nuspec \
  -OutputDirectory artifacts \
  -Properties configuration=Debug \
  -Version $VERSION