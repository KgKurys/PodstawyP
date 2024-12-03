#include <stdio.h>

void elmaks(int tab[], int size){
    int i;
    int max = tab[0];
    for(i = 1; i < size; i++){
        if(tab[i] > max){
            max = tab[i];
        }
    }
    printf("Najwiekszy element: %d\n", max);
}
int main(){
    int tab[10] = {1, 2, 3, 4, 5, 6, 7, 8, 9};
    elmaks(tab, 10);
    return 0;
}
