document.addEventListener('DOMContentLoaded', function() {
    // Elements
    const tableNameInput = document.getElementById('table-name');
    const createTableBtn = document.getElementById('create-table');
    const measurementTypeSelect = document.getElementById('measurement-type');
    const measurementValueInput = document.getElementById('measurement-value');
    const measurementDescriptionInput = document.getElementById('measurement-description');
    const addMeasurementBtn = document.getElementById('add-measurement');
    const tablesContainer = document.getElementById('measurement-tables');
    const notesArea = document.getElementById('notes-area');
    const exportBtn = document.getElementById('export-btn');
    
    // State
    let currentTableId = null;
    
    // Event listeners
    createTableBtn.addEventListener('click', createNewTable);
    measurementValueInput.addEventListener('input', validateMeasurementForm);
    addMeasurementBtn.addEventListener('click', addMeasurement);
    exportBtn.addEventListener('click', exportToTxt);
    
    // Functions
    function createNewTable() {
        const tableName = tableNameInput.value.trim();
        
        if (!tableName) {
            alert('Wprowadź nazwę tabeli.');
            return;
        }
        
        const tableId = 'table-' + Date.now();
        
        const tableHtml = `
            <div class="measurement-table" id="${tableId}">
                <h3>
                    <span><i class="fas fa-table"></i> ${tableName}</span>
                    <button class="delete-table"><i class="fas fa-trash-alt"></i> Usuń</button>
                </h3>
                <table>
                    <thead>
                        <tr>
                            <th>Typ pomiaru</th>
                            <th>Wartość</th>
                            <th>Jednostka</th>
                            <th>Opis</th>
                            <th>Akcje</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        `;
        
        tablesContainer.insertAdjacentHTML('beforeend', tableHtml);
        
        // Add event listener for delete table button
        document.querySelector(`#${tableId} .delete-table`).addEventListener('click', function() {
            if (confirm('Czy na pewno chcesz usunąć tę tabelę?')) {
                document.getElementById(tableId).remove();
                
                // If deleted current table, reset currentTableId
                if (currentTableId === tableId) {
                    currentTableId = null;
                    addMeasurementBtn.disabled = true;
                }
            }
        });
        
        // Set this as current table
        currentTableId = tableId;
        addMeasurementBtn.disabled = false;
        
        // Clear input
        tableNameInput.value = '';
        
        // Show success message
        showToast('Tabela została utworzona!');
    }
    
    function validateMeasurementForm() {
        const value = measurementValueInput.value.trim();
        addMeasurementBtn.disabled = !currentTableId || !value;
    }
    
    function addMeasurement() {
        const typeSelect = measurementTypeSelect;
        const typeValue = typeSelect.value;
        const typeText = typeSelect.options[typeSelect.selectedIndex].text;
        const value = measurementValueInput.value.trim();
        const description = measurementDescriptionInput.value.trim();
        
        if (!currentTableId || !value) {
            return;
        }
        
        // Get unit based on measurement type
        let unit;
        switch(typeValue) {
            case 'resistance':
            case 'total-resistance':
                unit = 'Ω';
                break;
            case 'voltage':
                unit = 'V';
                break;
            case 'current':
                unit = 'A';
                break;
            case 'capacitance':
            case 'total-capacitance':
                unit = 'F';
                break;
            default:
                unit = '';
        }
        
        // Create row
        const rowHtml = `
            <tr>
                <td>${typeText}</td>
                <td>${value}</td>
                <td>${unit}</td>
                <td>${description || '-'}</td>
                <td><button class="delete-row"><i class="fas fa-times"></i></button></td>
            </tr>
        `;
        
        const tbody = document.querySelector(`#${currentTableId} tbody`);
        tbody.insertAdjacentHTML('beforeend', rowHtml);
        
        // Add event listener for delete row button
        const deleteBtn = tbody.lastElementChild.querySelector('.delete-row');
        deleteBtn.addEventListener('click', function() {
            this.closest('tr').remove();
        });
        
        // Clear inputs
        measurementValueInput.value = '';
        measurementDescriptionInput.value = '';
        addMeasurementBtn.disabled = true;
        
        // Show success message
        showToast('Pomiar został dodany!');
    }
    
    function exportToTxt() {
        let content = '=== NOTATNIK POMIARÓW LABORATORYJNYCH ===\n\n';
        
        // Add tables and measurements
        const tables = document.querySelectorAll('.measurement-table');
        
        if (tables.length === 0) {
            alert('Brak danych do eksportu. Dodaj przynajmniej jedną tabelę z pomiarami.');
            return;
        }
        
        tables.forEach(table => {
            const tableName = table.querySelector('h3 span').textContent.trim();
            content += `## ${tableName} ##\n\n`;
            
            const rows = table.querySelectorAll('tbody tr');
            
            if (rows.length === 0) {
                content += 'Brak pomiarów\n\n';
            } else {
                content += 'Typ pomiaru | Wartość | Jednostka | Opis\n';
                content += '------------|---------|-----------|------\n';
                
                rows.forEach(row => {
                    const cells = row.querySelectorAll('td');
                    content += `${cells[0].textContent} | ${cells[1].textContent} | ${cells[2].textContent} | ${cells[3].textContent}\n`;
                });
                
                content += '\n';
            }
        });
        
        // Add notes
        const notes = notesArea.value.trim();
        if (notes) {
            content += '## DODATKOWE NOTATKI ##\n\n';
            content += notes + '\n\n';
        }
        
        // Add date and time
        const now = new Date();
        content += `Wygenerowano: ${now.toLocaleDateString()} ${now.toLocaleTimeString()}`;
        
        // Create and download file
        const blob = new Blob([content], { type: 'text/plain' });
        const url = URL.createObjectURL(blob);
        
        const a = document.createElement('a');
        a.href = url;
        a.download = 'pomiary_laboratoryjne.txt';
        document.body.appendChild(a);
        a.click();
        
        // Cleanup
        setTimeout(() => {
            document.body.removeChild(a);
            URL.revokeObjectURL(url);
        }, 0);
        
        // Show success message
        showToast('Plik został wyeksportowany!');
    }
    
    // Toast notification function
    function showToast(message) {
        // Create toast element if it doesn't exist
        let toast = document.querySelector('.toast');
        if (!toast) {
            toast = document.createElement('div');
            toast.className = 'toast';
            document.body.appendChild(toast);
            
            // Add styles for toast
            const style = document.createElement('style');
            style.textContent = `
                .toast {
                    position: fixed;
                    bottom: 30px;
                    right: 30px;
                    background-color: var(--primary-color);
                    color: white;
                    padding: 15px 25px;
                    border-radius: 4px;
                    font-weight: 500;
                    box-shadow: 0 3px 10px rgba(0, 0, 0, 0.2);
                    transform: translateY(100px);
                    opacity: 0;
                    transition: transform 0.3s ease-out, opacity 0.3s ease-out;
                    z-index: 1000;
                }
                
                .toast.show {
                    transform: translateY(0);
                    opacity: 1;
                }
            `;
            document.head.appendChild(style);
        }
        
        // Set message and show toast
        toast.textContent = message;
        toast.classList.add('show');
        
        // Hide toast after timeout
        setTimeout(() => {
            toast.classList.remove('show');
        }, 3000);
    }
});