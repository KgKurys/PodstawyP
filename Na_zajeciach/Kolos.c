#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

struct pacjent {
    char id[21];
    char nazwisko[51];
    int wiek;
    char diagnoza[101];
};
void wyswietlPacjenta(struct pacjent p) {
    printf("\nDane pacjenta:\n");
    printf("ID: %s\n", p.id);
    printf("Nazwisko: %s\n", p.nazwisko);
    printf("Wiek: %d\n", p.wiek);
    printf("Diagnoza: %s\n", p.diagnoza);
}

#define MAX_PACJENTOW 100

struct pacjent pacjenci[MAX_PACJENTOW];
char istniejace_id[MAX_PACJENTOW][21];
int liczba_pacjentow = 0;

int czy_id_istnieje(const char* id) {
    for(int i = 0; i < liczba_pacjentow; i++) {
        if(strcmp(istniejace_id[i], id) == 0) {
            return 1;
        }
    }
    return 0;
}
int znajdzPacjenta(const char* id) {
    for(int i = 0; i < liczba_pacjentow; i++) {
        if(strcmp(pacjenci[i].id, id) == 0) {
            return i;
        }
    }
    return -1;
}
void usunPacjenta() {
    char id[21];
    printf("Podaj ID pacjenta do usuniecia: ");
    fgets(id, sizeof(id), stdin);
    id[strcspn(id, "\n")] = 0;
    
    int index = znajdzPacjenta(id);
    if(index == -1) {
        printf("Nie znaleziono pacjenta o podanym ID.\n");
        return;
    }
    
    for(int i = index; i < liczba_pacjentow - 1; i++) {
        pacjenci[i] = pacjenci[i + 1];
        strcpy(istniejace_id[i], istniejace_id[i + 1]);
    }
    
    liczba_pacjentow--;
    printf("Pacjent zostal usuniety.\n");
}
void znajdzPacjentowPoNazwisku() {
    char szukane_nazwisko[51];
    int znaleziono = 0;
    
    printf("Podaj nazwisko do wyszukania: ");
    fgets(szukane_nazwisko, sizeof(szukane_nazwisko), stdin);
    szukane_nazwisko[strcspn(szukane_nazwisko, "\n")] = 0;
    
    for(int i = 0; i < liczba_pacjentow; i++) {
        if(strcmp(pacjenci[i].nazwisko, szukane_nazwisko) == 0) {
            wyswietlPacjenta(pacjenci[i]);
            znaleziono = 1;
        }
    }
    
    if(!znaleziono) {
        printf("Nie znaleziono pacjentow o nazwisku: %s\n", szukane_nazwisko);
    }
}

void znajdzPacjentowPoWieku() {
    int wiek_min, wiek_max;
    int znaleziono = 0;
    
    printf("Podaj minimalny wiek: ");
    scanf("%d", &wiek_min);
    printf("Podaj maksymalny wiek: ");
    scanf("%d", &wiek_max);
    
    if(wiek_min > wiek_max) {
        int temp = wiek_min;
        wiek_min = wiek_max;
        wiek_max = temp;
    }
    
    for(int i = 0; i < liczba_pacjentow; i++) {
        if(pacjenci[i].wiek >= wiek_min && pacjenci[i].wiek <= wiek_max) {
            wyswietlPacjenta(pacjenci[i]);
            znaleziono = 1;
        }
    }
    
    if(!znaleziono) {
        printf("Nie znaleziono pacjentów w przedziale wieku %d-%d\n", wiek_min, wiek_max);
    }
    
    while(getchar() != '\n');
}
void generujRaport() {
    float suma_wieku = 0;
    
    printf("\n=== RAPORT PACJENTOW ===\n");
    printf("Lista pacjentow:\n");
    
    for(int i = 0; i < liczba_pacjentow; i++) {
        printf("%d. ID: %s, Nazwisko: %s\n", 
               i+1, pacjenci[i].id, pacjenci[i].nazwisko);
        suma_wieku += pacjenci[i].wiek;
    }
    
    if(liczba_pacjentow > 0) {
        float srednia_wieku = suma_wieku / liczba_pacjentow;
        printf("\nSrednia wieku pacjentow: %.2f lat\n", srednia_wieku);
    } else {
        printf("\nBrak pacjentow w bazie.\n");
    }
}
void zapiszDoPliku() {
    FILE *plik = fopen("pacjenci.txt", "w");
    if (plik == NULL) {
        printf("Blad otwarcia pliku!\n");
        return;
    }
    
    fprintf(plik, "ID;Nazwisko;Wiek;Diagnoza\n");
    
    for(int i = 0; i < liczba_pacjentow; i++) {
        fprintf(plik, "%s;%s;%d;%s\n", 
            pacjenci[i].id,
            pacjenci[i].nazwisko,
            pacjenci[i].wiek,
            pacjenci[i].diagnoza);
    }
    
    fclose(plik);
    printf("Dane zostaly zapisane do pliku.\n");
}

void wczytajZPliku() {
    FILE *plik = fopen("pacjenci.txt", "r");
    if (plik == NULL) {
        printf("Blad otwarcia pliku!\n");
        return;
    }
    
    char linia[200];

    fgets(linia, sizeof(linia), plik);
    

    liczba_pacjentow = 0;
    

    while(fgets(linia, sizeof(linia), plik) != NULL) {
        char *token;
        

        token = strtok(linia, ";");
        if(token) strcpy(pacjenci[liczba_pacjentow].id, token);
        

        token = strtok(NULL, ";");
        if(token) strcpy(pacjenci[liczba_pacjentow].nazwisko, token);
        

        token = strtok(NULL, ";");
        if(token) pacjenci[liczba_pacjentow].wiek = atoi(token);
        

        token = strtok(NULL, "\n");
        if(token) strcpy(pacjenci[liczba_pacjentow].diagnoza, token);
        
        strcpy(istniejace_id[liczba_pacjentow], pacjenci[liczba_pacjentow].id);
        liczba_pacjentow++;
    }
    
    fclose(plik);
    printf("Dane zostaly wczytane z pliku.\n");
}

int sprawdzPoprawnoscId(const char* id) {
    for(int i = 0; id[i]; i++) {
        if(!isalpha(id[i]) && !isspace(id[i])) {
            return 0;
        }
    }
    return 1;
}

int sprawdzPoprawnoscWieku(int wiek) {
    return (wiek >= 1 && wiek <= 120);
}

struct pacjent dodajPacjenta() {
    struct pacjent nowy;
    char temp_id[21];
    
    do {
        printf("Podaj ID pacjenta (max 20 znakow, tylko litery i spacje): ");
        fgets(temp_id, sizeof(temp_id), stdin);
        temp_id[strcspn(temp_id, "\n")] = 0;
        
        if(!sprawdzPoprawnoscId(temp_id)) {
            printf("Nieprawidlowe ID! Uzywaj tylko liter i spacji.\n");
            continue;
        }
        
        if(czy_id_istnieje(temp_id)) {
            printf("To ID juz istnieje! Wybierz inne.\n");
            continue;
        }
    } while(!sprawdzPoprawnoscId(temp_id) || czy_id_istnieje(temp_id));
    
    strcpy(nowy.id, temp_id);
    strcpy(istniejace_id[liczba_pacjentow], temp_id);
    
    printf("Podaj nazwisko pacjenta (max 50 znakow): ");
    fgets(nowy.nazwisko, sizeof(nowy.nazwisko), stdin);
    nowy.nazwisko[strcspn(nowy.nazwisko, "\n")] = 0;
    
    do {
        printf("Podaj wiek pacjenta (1-120): ");
        if (scanf("%d", &nowy.wiek) != 1 || !sprawdzPoprawnoscWieku(nowy.wiek)) {
            printf("Nieprawidlowy wiek! Wiek musi byc pomiedzy 1 a 120.\n");
            while (getchar() != '\n');
            nowy.wiek = -1;
        }
    } while (!sprawdzPoprawnoscWieku(nowy.wiek));
    
    while (getchar() != '\n');
    
    printf("Podaj diagnoze pacjenta (max 100 znakow): ");
    fgets(nowy.diagnoza, sizeof(nowy.diagnoza), stdin);
    nowy.diagnoza[strcspn(nowy.diagnoza, "\n")] = 0;
    
    return nowy;
}



int main() {
    char wybor;
    
    while(1) {
        printf("1. Dodaj pacjenta\n");
        printf("2. Wyswietl wszystkich pacjentow\n");
        printf("3. Usun pacjenta\n");
        printf("4. Szukaj po nazwisku\n");
        printf("5. Szukaj po wieku\n");
        printf("6. Generuj raport\n");
        printf("7. Zapisz do pliku\n");
        printf("8. Wczytaj z pliku\n");
        printf("9. Wyjscie\n");
        
        scanf(" %c", &wybor);
        while(getchar() != '\n');
        
        switch(wybor) {
            case '1':
                if(liczba_pacjentow >= MAX_PACJENTOW) {
                    printf("Osiagnieto maksymalna liczbe pacjentow!\n");
                    continue;
                }
                pacjenci[liczba_pacjentow] = dodajPacjenta();
                liczba_pacjentow++;
                printf("Pacjent dodany pomyslnie!\n");
                break;
                
            case '2':
                if(liczba_pacjentow == 0) {
                    printf("Brak pacjentow w bazie.\n");
                } else {
                    for(int i = 0; i < liczba_pacjentow; i++) {
                        printf("\nPacjent %d:", i+1);
                        wyswietlPacjenta(pacjenci[i]);
                    }
                }
                break;
                
            case '3':
            usunPacjenta();
            break;
            case '4':
            znajdzPacjentowPoNazwisku();
            break;
            case '5':
            znajdzPacjentowPoWieku();
            break;
            case '6':
            generujRaport();
            break;
            case '7':
            zapiszDoPliku();
            break;
            case '8':
            wczytajZPliku();
            break;
            case '9':
            printf("Koniec programu.\n");
            return 0;
                
            default:
                printf("Nieprawidlowy wybor!\n");
        }
    }
    
    return 0;

}