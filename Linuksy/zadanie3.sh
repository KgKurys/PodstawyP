#!/bin/bash

echo Podaj lokalizacje
read A

DIR=$A

hidden_files=$(ls -a "$DIR" | grep -E "^\..*a.*")


if [ -z "$hidden_files" ]; then
echo Nie znaleziono plikow
else
echo "Znalezione ukryte pliki z literą a"
echo $hidden_files
fi