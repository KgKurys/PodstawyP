#include <stdio.h>
#include <ctype.h>

int main(){
    double temp;
    char type;
    while (1){ //loop ktory sprawdza czy jednostka jest poprwana
    printf("Podaj Docelowy format temperatury, F albo C\n");
    scanf(" %c", &type);
    type = toupper(type); // ignoruje czy wprowadzona jednostka jest mala czy duza litera, 
                            //zamiena mala na duza
        if (type == 'F' || type == 'C'){
            break; // break wychodzi z loopa i idzie do dalej

        } else{
            printf("Podaj poprawną jednostkę temperatury\n");
        }
    }
    


    switch(type){
        case 'F':
        printf("Podaj temperaturę w Celciuszach\n");
        scanf("%lf", &temp);
        printf("Temperatura %.2lf podana w Farenhaitah to %.2lf\n",temp, (temp * 9/5)+32);
        break;

        case 'C':
        printf("Podaj temperaturę w Farenhaitah\n");
        scanf("%lf", &temp);
        printf("Temperatura %.2lf podana w Farenhaitah to %.2lf\n",temp, ((temp -32)*5)/9);
        break;
    }


} ///git test