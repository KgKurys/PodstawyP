// Napisz program, który wczyta od użytkownika liczbę elementów w tablicy (n - ilość wierszy, m - ilość kolumn) i wypełni
// dwuwymiarową tablicę liczby całkowitych wartościami wczytanymi od użytkownika, a następnie wyznaczy ile jest elementów parzystych
// dla każdego wiersza osobno. Wyświetlić zawartość tej tablicy.

#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define N 3
#define M 3

int main() 
{
    int szachownica[N][M];
    srand(time(NULL));
    
    for (int i = 0; i < N; i++){
        for (int j = 0; j < M; j++){
            if ((i+j) % 2 != 0){
                szachownica[i][j] = rand() % 100 * 2;
            }else{
                szachownica[i][j] = rand() % 100 * 2 + 1;
            } 
            
        }

    }
    for (int i = 0; i<N; i++) {
        for(int j = 0; j<M; j++) {
            printf("%d ", szachownica[i][j]);              
        }
        printf("\n");
    }




    return 0;
}