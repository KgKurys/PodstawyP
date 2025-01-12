#!/bin/bash

DIR=$1
if [ -z "$DIR" ]; then
  echo "Nie podano katalogu."
  exit 1
fi

pliki=($(ls -Ap $DIR | grep -v /))
katalogi=($(ls -Ap $DIR |grep /))

if [ -z "$katalogi" ];then
echo Katalog jest pusty
fi

for i in ${pliki[@]}; do
    echo $i >> lista.plikow
done


for i in ${katalogi[@]}; do
    echo $i
done

