

#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define N 15


int main() 
{
    int tablica[N];
    int A,B;
    int liczba_losowan = 0;
    int unikalne;
    printf("Podaj zakres A: ");
    scanf("%d", &A);
    printf("Podaj zakres B: ");
    scanf("%d", &B);
    srand(time(NULL));

    
    for (int i = 0; i < N; i++){
        unikalne = 1;
        tablica[i] = rand() % (B - A + 1) + A;
        liczba_losowan++;
            for (int j = 0; j < i; j++){
                if (tablica[i] == tablica[j]){
                    unikalne = 0;
                    break;
                }
            }
            
        
        if (unikalne == 0){
            i--;
        }
    }
        
    
    for (int i = 0; i < N; i++){
        printf("%d ", tablica[i]);
       
    }
    printf("Liczba losowan: %d\n", liczba_losowan); 

        

   




    return 0;
}