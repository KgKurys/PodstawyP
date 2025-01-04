#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct {
    char autor[30];
    char tytul[50];
    char kat[40];
    float cena;
    int strony;
    int read;
    int regal;
} Book;

void displayBooks(Book books[], int n);
void findCheapestBook(Book books[], int n);
void findThickestBooksByCategory(Book books[], int n);
void writeReadBooksAboveAveragePrice(Book books[], int n, const char* filename);
void findMostExpensiveUnreadBookByAuthor(Book books[], int n, const char* author);

int main() {
    FILE *file = fopen("biblioteka.txt", "r");
    if (!file) {
        perror("Error opening file");
        return EXIT_FAILURE;
    }

    int n;
    fscanf(file, "%d", &n);
    printf("Number of books: %d\n", n);
    Book *books = malloc(n * sizeof(Book));
    
    for (int i = 0; i < n; i++) {
        fscanf(file, "%29s %49s %39s %f %d %d %d",
               books[i].autor,
               books[i].tytul,
               books[i].kat,
               &books[i].cena,
               &books[i].strony,
               &books[i].read,
               &books[i].regal);
    }
    fclose(file);

    displayBooks(books, n);
    findCheapestBook(books, n);
    findThickestBooksByCategory(books,n);
    free(books);
    return 0;
}

void displayBooks(Book books[], int n) {
    printf("%-20s %-40s %-20s %-10s %-10s %-10s %-10s\n",
    "Autor", "Tytul", "Kategoria", "Cena", "Strony", "Przeczytane?", "Polka\n");
    for (int i = 0; i < n; i++) {
        printf("%-20s %-40s %-20s %-10.2f %-10d %-10d %-10d\n",
               books[i].autor,
               books[i].tytul,
               books[i].kat,
               books[i].cena,
               books[i].strony,
               books[i].read,
               books[i].regal);
    }
    printf("\n");
}
void findCheapestBook(Book books[], int n){
    int index = 0;
    for (int i = 1; i<n;i++){
        if(books[i].cena < books[index].cena){
            index = i;
        }
        
    }
    printf("Cheapest book: %s (%.2f)\n",
           books[index].tytul,
           books[index].cena);
printf("\n");
}

void findThickestBooksByCategory(Book books[], int n){
    for (int i = 0; i < n; i++) {
        int found = 0;
        
        for (int j = 0; j < i; j++) {
            if (books[j].kat[0] == books[i].kat[0]) {
                found = 1;
                break;
            }
        }
        if (!found) {
            int maxPages = books[i].strony;
            int maxIndex = i;
            for (int j = i + 1; j < n; j++) {
                if (books[j].kat[0] == books[i].kat[0] && 
                    books[j].strony > maxPages) {
                    maxPages = books[j].strony;
                    maxIndex = j;
                }
            }
            
            printf("Thickest in %s: %s (%d pages)\n",
                   books[maxIndex].kat,
                   books[maxIndex].tytul,
                   books[maxIndex].strony);
        }
    }
    
     
}
