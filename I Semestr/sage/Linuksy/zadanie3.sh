#!/bin/bash
DIR=$1
hidden_files=($(ls -ap $DIR | grep -v / | grep -E "a" | grep "^\."))
if [ -z "$hidden_files" ]; then
echo Nie znaleziono plikow
else
echo "Znalezione ukryte pliki z literą a"
for i in ${hidden_files[@]}; do
echo $i
done
fi
