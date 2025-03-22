//Napisz funkcję, która wyznacza n-tą 
//(n jest liczba naturalną) potęgę zadanej liczby rzeczywistej x.

#include <stdio.h>
#include <stdlib.h>

float potega(float x, int n){
    float result = 1;
    int i;

    for(i = 0; i < n; i++){
        result *= x;
    }
    return result;
}
int main() {
    float x;
    int n;
    printf("Podaj x i potęge: ");
    scanf("%f %d", &x, &n);
    printf("Wynik: %f\n", potega(x, n));
    return 0;
}




