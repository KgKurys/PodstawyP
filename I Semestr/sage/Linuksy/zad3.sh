#!bin/bash

DIR=$1
hidden_files=($(ls -ap  $DIR | grep -v / | grep -E "a" | grep "^\."))

if [ -z ${hidden_files[@]} ]; then
echo Pusto
fi
for i in ${hidden_files[@]}; do
    echo $i
done
