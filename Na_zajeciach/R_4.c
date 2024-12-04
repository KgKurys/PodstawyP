#include <stdio.h>
#include <stdlib.h>
#include <string.h>


void odwrocona(char str[]) {
    int n = strlen(str);
    for (int i = 0; i < n / 2; i++) {
        char temp = str[i];
        str[i] = str[n - i - 1];
        str[n - i - 1] = temp;
    }
}
int main() {
    char str[100];
    printf("Podaj napis: ");
    scanf("%99s", str);
    odwrocona(str);
    printf("Odworcona: %s\n", str);
    return 0;
}