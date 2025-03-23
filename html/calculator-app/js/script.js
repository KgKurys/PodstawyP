let firstNumber = '';
let secondNumber = '';
let currentOperator = null;
let shouldResetDisplay = false;

function updateDisplay() {
    document.getElementById('result').textContent = secondNumber || firstNumber || '0';
    document.getElementById('equation').textContent = currentOperator ? 
        `${firstNumber} ${currentOperator} ${secondNumber || ''}` : '';
}

function appendNumber(number) {
    if (shouldResetDisplay) {
        if (currentOperator) {
            secondNumber = number.toString();
        } else {
            firstNumber = number.toString();
        }
        shouldResetDisplay = false;
    } else {
        if (currentOperator) {
            secondNumber += number.toString();
        } else {
            firstNumber += number.toString();
        }
    }
    updateDisplay();
}

function appendDecimal() {
    if (currentOperator) {
        if (secondNumber.includes('.')) return;
        secondNumber += secondNumber ? '.' : '0.';
    } else {
        if (firstNumber.includes('.')) return;
        firstNumber += firstNumber ? '.' : '0.';
    }
    updateDisplay();
}

function setOperator(operator) {
    if (currentOperator && secondNumber) {
        calculate();
    }
    currentOperator = operator;
    shouldResetDisplay = false;
    updateDisplay();
}

function calculate() {
    if (!currentOperator || !firstNumber || !secondNumber) return;
    
    const num1 = parseFloat(firstNumber);
    const num2 = parseFloat(secondNumber);
    let result;

    switch (currentOperator) {
        case '+':
            result = num1 + num2;
            break;
        case '-':
            result = num1 - num2;
            break;
        case '*':
            result = num1 * num2;
            break;
        case '/':
            if (num2 === 0) {
                clearAll();
                document.getElementById('result').textContent = 'Error';
                return;
            }
            result = num1 / num2;
            break;
    }

    firstNumber = result.toString();
    secondNumber = '';
    currentOperator = null;
    shouldResetDisplay = true;
    updateDisplay();
}

function clearAll() {
    firstNumber = '';
    secondNumber = '';
    currentOperator = null;
    shouldResetDisplay = false;
    updateDisplay();
}

function backspace() {
    if (secondNumber) {
        secondNumber = secondNumber.slice(0, -1);
    } else if (currentOperator) {
        currentOperator = null;
    } else if (firstNumber) {
        firstNumber = firstNumber.slice(0, -1);
    }
    updateDisplay();
}

// Initialize display
updateDisplay();