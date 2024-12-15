#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct produkt{
    int id;
    char nazwa[20];
    float cena;
};
struct pozycja_koszyka{
    int id;
    int liczba_sztuk;
};



int main(){
    srand(time(0));
    struct produkt produkty[] = 
    {
        {1, "Jabko", 2.50},
        {2, "Chlep", 3.25},
        {3, "Majezon", 8.00},
        {4, "Picie", 5.00},
        {5, "Maselo", 10.00},
        {6, "Fajki", 17.99},

    };
    int liczba_produktow = sizeof(produkty)/sizeof(produkty[0]);

    int liczba_pozycji;
    printf("Podaj liczbe pozycji w koszyku: ");
    scanf("%d", &liczba_pozycji);


    struct pozycja_koszyka koszyk[liczba_pozycji];
    for (int i = 0; i < liczba_pozycji; i++)
    {
        koszyk[i].id = rand() % liczba_produktow + 1;
        koszyk[i].liczba_sztuk = rand() % 10 +1;
    }

    printf("Paragon: \n");
    printf("ID\tNazwa\t\tCena\t\tLiczba sztuk\tWartosc\n");
    float suma = 0;

    for (int i = 0; i<liczba_pozycji; i++)
    {
        struct produkt p = produkty[koszyk[i].id -1];
        float wartosc = p.cena * koszyk[i].liczba_sztuk;
        suma+=wartosc;
        printf("%d\t%s\t\t%.2f\t\t%d\t\t%.2f\n", p.id, p.nazwa, p.cena, koszyk[i].liczba_sztuk, wartosc);
    }
    printf("Suma: %.2f ", suma);

    return 0;


}