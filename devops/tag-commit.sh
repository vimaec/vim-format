#!/bin/bash

if [ -z ${2+x} ]; then
    echo "The script expects 2 arguments: (1) project file path and (2) package name."
    exit 2
fi

__dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ver=`bash ${__dir}/csproj-version.sh $1`

package=`(echo "$2" | tr '[:upper:]' '[:lower:]')`  # to lower
package=${package#*.}                               # remove everything before .

if ! [ $? == 0 ]; then
    echo "Version couldn't be parsed."
    echo $ver
    exit 3
fi

echo "Project version is ${ver}. Tagging..."

git tag cs_${package}_${ver}

echo "Pushing tags..."

git push origin ${ver}