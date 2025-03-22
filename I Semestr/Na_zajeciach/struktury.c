#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct ksiazka {
    char autor[32], tytul[64];
    int liczba_egzemplarzy;
    float cena;
};

struct ksiazka wczytaj_ksiazke(void){
    struct ksiazka k;
    printf("Podaj autora: ");
    fgets(k.autor, 32, stdin);
    if(k.autor[strlen(k.autor)-1]=='\n') k.autor[strlen(k.autor)-1] = '\0';
    printf("Podaj tytul: ");
    fgets(k.tytul, 64, stdin);
    if(k.tytul[strlen(k.tytul)-1]=='\n') k.tytul[strlen(k.tytul)-1] = '\0';
    printf("Podaj liczbe egzemplarzy: ");
    scanf("%d", &k.liczba_egzemplarzy);
    printf("Podaj cene: ");
    scanf("%f", &k.cena);
    //ponieważ w programie używamy zarówno funkcji fgets, jak i scanf
    //to musimy wyczyścić strumień wejściowy ze znaku Enter pozostałego po wczytaniu liczby funkcją scanf
    while (getchar() != '\n'); 
    return k;
}
void wypisz_ksiazke(struct  ksiazka k)
{
    printf("%s \"%s\" %.2f PLN (%d sztuk)\n", k.autor, k.tytul, k.cena, k.liczba_egzemplarzy);
};

struct ksiazka* wczytaj_ksiazki(int n){
    struct ksiazka *t = calloc(n, sizeof(struct ksiazka));
    if(t==NULL) return NULL;

    for(int i=0; i<n; ++i){
        t[i] = wczytaj_ksiazke();
    }
    return t;
}

void wypisz_ksiazki(struct ksiazka *t, int n){
    for(int i=0; i<n; ++i){
        wypisz_ksiazke(t[i]);
    }
}

float oblicz_wartosc(struct ksiazka *t, int n){
    float w = 0;
    for(int i=0; i<n; ++i){
        w += t[i].liczba_egzemplarzy*t[i].cena;
    }
    return w;
}

float oblicz_vat(struct ksiazka *t, int n, char autor[32]){
    float vat = 0;
    for(int i=0; i<n; ++i){
        if(strcmp(autor, t[i].autor)==0){
            vat += t[i].liczba_egzemplarzy*t[i].cena;
        }
    }
    return vat*0.05;
}

int main()
{
    int n;
    printf("Ile ksiazek chcesz wczytac?");
    do {
        scanf("%d", &n);
    }while (n<=0);
    while (getchar() != '\n'); 

    struct ksiazka *k = wczytaj_ksiazki(n);
    if(k==NULL)
        return 0;

    wypisz_ksiazki(k, n);

    printf("Wartosc ksiazek w magazynie: %.2f\n", oblicz_wartosc(k, n));

    char autor[32];
    printf("Podaj autora: ");
    fgets(autor, 32, stdin);
    if(autor[strlen(autor)-1]=='\n') autor[strlen(autor)-1] = '\0';
    printf("Wartosc podatku vat ksiazek wynosi: %.2f\n", oblicz_vat(k, n, autor));

    free(k);
    return 0;
}