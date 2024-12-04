#include <stdio.h>
#include <math.h>




int main(){

    char symbol;
    double A;
    double B;
    while (1){
        printf("Jakie działanie chcesz wykonac?(Podaj znak): \n");
        scanf(" %c", &symbol);
        if (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/'){
            break;
        } else{
            printf("nieznany symbol\n");
        }
    }
    printf("Podaj wartość pierwszą: \n");
    scanf("%lf", &A);
    printf("Podaj wartość pierwszą: \n");
    scanf("%lf", &B);


    switch(symbol){
        case '+':
        printf("Wynik Dodawania: %.2lf\n", A+B);
        break;
        case '-':
        printf("Wynik Dodawania: %.2lf\n", A-B);
        break;
        case '*':
        printf("Wynik Dodawania: %.2lf\n", A*B);
        break;
        case '/':
        if (B <= 0){
            printf("Cholero nie dziel przez zero");
            break;
        }
        else;
        printf("Wynik Dodawania: %.2lf\n", A/B);
        break;
    }

    return 0;

}