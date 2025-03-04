#!/bin/bash

runda=1
gdzie=4


echo Podaj która komore chcesz sprawdzic
read A
echo

while [ $runda -ne 5 ]
do
    echo "Runda: $runda"
    if [ $A -eq $gdzie ]; then
        echo Nie zyjesz
        break
    else
        echo Masz Szczęście
        runda=$((runda + 1))
        echo
    
    fi
echo Podaj która komore chcesz sprawdzic
read A
echo
done
if [ $runda -eq 5 ]; then
    echo "Runda: 5 - Brak innych komór"
    echo Wygrales!
fi