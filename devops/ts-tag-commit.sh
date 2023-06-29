#!/bin/bash

if [ -z ${1+x} ]; then
    echo "The script expects 1 argument: package.json path."
    exit 2
fi

ver=`awk -F'"' '/"version": ".+"/{ print $4; exit; }' $1`

echo "package.json version is ${ver}. Tagging ts_${ver}..."

git tag ts_${ver}

echo "Pushing tag..."

git push origin ts_${ver}