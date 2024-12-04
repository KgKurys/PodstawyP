#include <stdio.h>
#include <math.h>

int main(){
    float a, b;
    printf("Podaj dlugosc dluzszej przuprostokątnej: ");
    scanf("%f", &a);
    printf("Podaj dlugosc krotszej przuprostokątnej: ");
    scanf("%f", &b);
    double c = sqrt(a*a + b*b);
    printf("Przeciw Prostokątna ma dlugosc: %.2lf\n", c);
    return 0;
}