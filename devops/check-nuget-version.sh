#!/bin/bash

if [ -z ${2+x} ]; then
    echo "The script expects 2 arguments: (1) project file path and (2) package name."
    exit 2
fi

__dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ver=`bash ${__dir}/csproj-version.sh $1`

if ! [ $? == 0 ]; then
    echo "Version couldn't be parsed."
    echo $ver
    exit 3
fi

if [ "$ver" == "" ]; then
    echo "Version not found."
    exit 4
fi

echo "Project version is ${ver}. Checking NuGet versions..."

versions=`nuget list -AllVersions -IncludeDelisted -NonInteractive PackageId:$2 | awk '{print $2}'`

# `nuget list` returns "No packages found." message
if [ "$versions" = "packages" ]; then
    echo "No versions found."
    exit 0
fi

for nuget in $versions
do
    echo "Version found $nuget"

    if [ $nuget == ${ver} ] || [ $nuget == "v${ver}" ] || ([ ${ver:0:1} == 'v' ] && [ $nuget == ${ver:1} ]) ; then
        echo "The NuGet version $nuget already exists. The package cannot be pushed."
        exit 1
    fi
done