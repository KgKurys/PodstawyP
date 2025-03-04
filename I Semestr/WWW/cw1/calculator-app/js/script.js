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

function moveCalculatorToRandomPosition() {
    const calculator = document.querySelector('.calculator');
    
    // Force a layout reflow to ensure we get accurate dimensions
    void calculator.offsetWidth;
    
    const calculatorWidth = calculator.offsetWidth;
    const calculatorHeight = calculator.offsetHeight;
    
    // Get window dimensions
    const windowWidth = window.innerWidth;
    const windowHeight = window.innerHeight;
    
    // Calculate maximum values for left and top positions
    // Ensure calculator stays completely within the screen
    const maxLeft = Math.max(0, windowWidth - calculatorWidth);
    const maxTop = Math.max(0, windowHeight - calculatorHeight);
    
    // Generate random positions within screen bounds
    // Using Math.min to ensure we don't exceed the maximum values
    const randomLeft = Math.min(Math.max(0, Math.floor(Math.random() * maxLeft)), maxLeft);
    const randomTop = Math.min(Math.max(0, Math.floor(Math.random() * maxTop)), maxTop);
    
    // Apply new position without transition
    calculator.style.position = 'absolute';
    calculator.style.left = `${randomLeft}px`;
    calculator.style.top = `${randomTop}px`;
    calculator.style.transition = 'none';
    
    // For debugging - log the values
    console.log({
        calculatorWidth,
        calculatorHeight,
        windowWidth,
        windowHeight,
        maxLeft,
        maxTop,
        randomLeft,
        randomTop
    });
}

function calculate() {
    // Remove calculation logic and just move the calculator
    moveCalculatorToRandomPosition();
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

// Initialize display and set initial position
document.addEventListener('DOMContentLoaded', function() {
    // Set initial position to center of screen
    const calculator = document.querySelector('.calculator');
    calculator.style.position = 'absolute';
    calculator.style.left = '50%';
    calculator.style.top = '50%';
    calculator.style.transform = 'translate(-50%, -50%)';
    
    // Initialize the display
    updateDisplay();
});