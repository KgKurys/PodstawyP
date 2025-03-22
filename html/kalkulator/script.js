let display = document.getElementById('display');

function appendValue(value) {
    display.value += value;
}

function clearDisplay() {
    display.value = '';
}

function calculate() {
    try {
        if (display.value.includes('/0')) {
            display.value = 'Do not divide by zero';
        } else {
            display.value = eval(display.value);
        }
    } catch (e) {
        display.value = 'Error';
    }
}
