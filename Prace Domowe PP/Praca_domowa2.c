#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define N 20

int main(){
    srand(time(0));
    int i,j;
    int losowania = 0;
    int A,B;
    int tablica[N];

    while(1){
    printf("Podaj przedzial A ");
    scanf(" %d", &A);
    printf("Podaj przedzial B ");
    scanf(" %d", &B);
    if ((B - A) >= N){
        break;        
    }else{
        printf("Podaaj przedzial w ktorym jest wiecej niz %d lub rowno mozliwosci\n", N);
    }
    }


    for(i = 0; i < N; i++){
        int unikalna = 0;
        do{
            unikalna = 1;
            tablica[i] = rand() % (B - A + 1 )+ A;
            losowania++;
        for (j = 0; j < i; j++){
            if (tablica[i] == tablica[j]){
                unikalna = 0;
                break;

            }
        }
        }while(!unikalna);
        printf("%d ", tablica[i]);
    }
    printf("\nLiczba losowan %d ", losowania);
    return 0;

}