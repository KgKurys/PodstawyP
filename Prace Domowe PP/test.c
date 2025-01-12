#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX_BOOKS 100
#define MAX_AUTHOR_LEN 30
#define MAX_TITLE_LEN 50
#define MAX_CATEGORY_LEN 40

typedef struct {
    char author[MAX_AUTHOR_LEN];
    char title[MAX_TITLE_LEN];
    char category[MAX_CATEGORY_LEN];
    float price;
    int pages;
    int read;
    int shelf;
} Book;

void displayBooks(Book books[], int n);
void findCheapestBook(Book books[], int n);
void findThickestBooksByCategory(Book books[], int n);
void writeReadBooksAboveAveragePrice(Book books[], int n, const char* filename);
void findMostExpensiveUnreadBookByAuthor(Book books[], int n, const char* author);

int main() {
    FILE *file = fopen("books.txt", "r");
    if (file == NULL) {
        printf("Could not open file\n");
        return 1;
    }

    int n;
    fscanf(file, "%d", &n);

    Book books[MAX_BOOKS];
    for (int i = 0; i < n; i++) {
        fscanf(file, "%s %s %s %f %d %d %d", books[i].author, books[i].title, books[i].category, &books[i].price, &books[i].pages, &books[i].read, &books[i].shelf);
    }
    fclose(file);

    displayBooks(books, n);
    findCheapestBook(books, n);
    findThickestBooksByCategory(books, n);

    char outputFilename[100];
    printf("Enter the name of the output file: ");
    scanf("%s", outputFilename);
    writeReadBooksAboveAveragePrice(books, n, outputFilename);

    char author[MAX_AUTHOR_LEN];
    printf("Enter the author's name: ");
    scanf("%s", author);
    findMostExpensiveUnreadBookByAuthor(books, n, author);

    return 0;
}

void displayBooks(Book books[], int n) {
    for (int i = 0; i < n; i++) {
        printf("%s %s %s %.2f %d %d %d\n", books[i].author, books[i].title, books[i].category, books[i].price, books[i].pages, books[i].read, books[i].shelf);
    }
}

void findCheapestBook(Book books[], int n) {
    int minIndex = 0;
    for (int i = 1; i < n; i++) {
        if (books[i].price < books[minIndex].price) {
            minIndex = i;
        }
    }
    printf("Cheapest book: %s %s %s %.2f %d %d %d\n", books[minIndex].author, books[minIndex].title, books[minIndex].category, books[minIndex].price, books[minIndex].pages, books[minIndex].read, books[minIndex].shelf);
}

void findThickestBooksByCategory(Book books[], int n) {
    // Implementation will be added later
}

void writeReadBooksAboveAveragePrice(Book books[], int n, const char* filename) {
    // Implementation will be added later
}

void findMostExpensiveUnreadBookByAuthor(Book books[], int n, const char* author) {
    // Implementation will be added later
}