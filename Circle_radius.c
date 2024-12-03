#include <stdio.h>




int main()
{
    float Pi = 3.14;
    float r;
    printf("Podaj promień koła: ");
    scanf("%f", &r);
    printf("Pole koła wynosi: %.2f\n", Pi * r * r);
    return 0;
}