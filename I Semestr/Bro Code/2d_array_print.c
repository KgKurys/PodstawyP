#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define N 3
#define M 3
int tablica[N][M] = {{1,2,3},{1,2,3},{1,2,3}};
int main(){
    int i,j;

    for(i = 0; N > i; i++) {
        for(j = 0; M > j; j++){
            printf(" %d", tablica[i][j]);
        }
        printf("\n");
    }


    return 0; 
}