#include <stdio.h>
#include <strings.h>
#include <time.h>



struct uczen {
    char nazwisko[51];
    float ocena;
};



int main()
{

int i, n, o=0;



while(o==0){
printf("Podaj liczbe uczniow dla ktorych chcesz wprowadzic ocene: ");
scanf("%d", &n);
struct uczen uczniowie[n];
for (i = 0; i<n; i++)
    {
    printf("Podaj nazwisko ucznia %d: ", i + 1);
    scanf("%s", &uczniowie[i].nazwisko);
    printf("Podaj ocene %d: ",i + 1);
    scanf("%f", &uczniowie[i].ocena);
    }

for (i = 0; i < n; i++){
    printf("Uczen: %s\tOceny: %.1f\n", uczniowie[i].nazwisko, uczniowie[i].ocena);

}
printf("Co chcesz zrobic?\n Koniec na dzisiaj = 1\n Dodaje dalej = 0 ");
scanf("%d", &o);
if (o == 0){
    continue;
}else {
    break;
}
}
return 0;





}