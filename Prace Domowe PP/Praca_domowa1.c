#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define N 3
#define M 4


int main(){
int i,j;
int szachownica[N][M];
srand(time(0));

for(i = 0; i < N; i++ ){
    for(j = 0; j < M; j++ ){
        if ((i+j) % 2 == 0){
        szachownica[i][j] = (rand() % 50) * 2;
        }else{
          szachownica[i][j] = (rand() % 50) * 2 + 1;  
        }
        printf(" %3d", szachownica[i][j]);
        
    }
    printf("\n");
    
}




return 0;
}
