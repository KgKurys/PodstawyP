#!/bin/bash

echo Witaj w kalkulatorze
echo Podaj typ działania jaki chcesz wykonać "/ * - +"
read typ
echo Podaj pierwsza liczbe
read A
echo Podaj druga liczbe
read B
divide=$(($A/$B))
multi=$(($A*$B))
add=$(($A+$B))
sub=$(($A-$B))

if [ "$typ" = "/" ]; then
    echo "Wynik działania to" $divide
elif [ "$typ" = "*" ]; then
    echo "Wynik działania to" $multi
elif [ "$typ" = "+" ]; then
    echo "Wynik działania to" $add
elif [ "$typ" = "-" ]; then
    echo "Wynik działania to" $sub

fi