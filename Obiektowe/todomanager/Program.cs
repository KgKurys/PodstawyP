using System;
using System.Collections.Generic;
using System.Linq;

// Reprezentuje status zadania
enum TaskStatus { ToDo, InProgress, Done }

// Klasa reprezentująca użytkownika w systemie
class User
{
    public string Name { get; set; }         // Imię użytkownika
    public string Email { get; set; }         // Email - unikalny identyfikator
    public List<Task> Tasks { get; set; } = new List<Task>(); // Lista zadań przypisanych do użytkownika

    // Konstruktor tworzący nowego użytkownika
    public User(string name, string email) => (Name, Email) = (name, email);

    // Wyświetla wszystkie zadania przypisane do użytkownika
    public void DisplayTasks()
    {
        Console.WriteLine($"Zadania dla {Name} ({Email}):");
        if (Tasks.Count == 0) { Console.WriteLine("  Brak zadań."); return; }
        foreach (var task in Tasks) Console.WriteLine($"  {task}");
    }

    public override string ToString() => $"{Name} ({Email})";
}

// Klasa reprezentująca pojedyncze zadanie w projekcie
class Task
{
    public string Title { get; set; }          // Tytuł zadania
    public string Description { get; set; }    // Opis zadania
    public DateTime DueDate { get; set; }      // Termin wykonania
    public TaskStatus Status { get; set; } = TaskStatus.ToDo; // Status zadania
    public User AssignedUser { get; set; }     // Przypisany użytkownik (może być null)

    // Konstruktor tworzący nowe zadanie
    public Task(string title, string description, DateTime dueDate) => 
        (Title, Description, DueDate) = (title, description, dueDate);

    // Przypisuje użytkownika do zadania
    public void AssignUser(User user)
    {
        // Usuń zadanie z listy poprzedniego użytkownika
        if (AssignedUser != null) AssignedUser.Tasks.Remove(this);
        AssignedUser = user;
        // Dodaj zadanie do listy nowego użytkownika
        if (user != null) user.Tasks.Add(this);
    }

    // Formatuje informacje o zadaniu do wyświetlenia
    public override string ToString()
    {
        string userInfo = AssignedUser?.Name ?? "Nieprzypisane";
        string overdueInfo = DueDate < DateTime.Now && Status != TaskStatus.Done ? " [ZALEGŁE]" : "";
        return $"{Title}{overdueInfo} | {Description} | {DueDate.ToShortDateString()} | {Status} | {userInfo}";
    }
}

// Klasa reprezentująca projekt zawierający zadania
class Project
{
    public string Name { get; set; }           // Nazwa projektu
    public List<Task> Tasks { get; set; } = new List<Task>(); // Lista zadań w projekcie

    // Konstruktor tworzący nowy projekt
    public Project(string name) => Name = name;

    // Dodaje zadanie do projektu
    public void AddTask(Task task) => Tasks.Add(task);

    // Znajduje zadanie po tytule (case-insensitive)
    public Task GetTask(string title) => 
        Tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

    // Wyświetla zadania w projekcie, opcjonalnie filtrując po statusie
    public void DisplayTasks(TaskStatus? filterStatus = null)
    {
        var tasks = filterStatus.HasValue ? Tasks.Where(t => t.Status == filterStatus.Value).ToList() : Tasks;
        string statusInfo = filterStatus.HasValue ? $" o statusie {filterStatus}" : "";
        
        Console.WriteLine($"Zadania w projekcie {Name}{statusInfo}:");
        if (tasks.Count == 0) { Console.WriteLine("  Brak zadań."); return; }
        foreach (var task in tasks) Console.WriteLine($"  {task}");
    }

    // Wyświetla przeterminowane zadania w projekcie
    public void DisplayOverdueTasks()
    {
        var overdue = Tasks.Where(t => t.DueDate < DateTime.Now && t.Status != TaskStatus.Done).ToList();
        Console.WriteLine($"Przeterminowane zadania w projekcie {Name}:");
        if (overdue.Count == 0) { Console.WriteLine("  Brak przeterminowanych zadań."); return; }
        foreach (var task in overdue) Console.WriteLine($"  {task}");
    }

    public override string ToString() => $"Projekt: {Name} (Zadań: {Tasks.Count})";
}

// Główna klasa zarządzająca projektami, zadaniami i użytkownikami
class ToDoManager
{
    // Słownik użytkowników (klucz: email)
    public Dictionary<string, User> Users { get; } = new Dictionary<string, User>();
    // Lista projektów
    public List<Project> Projects { get; } = new List<Project>();

    // Dodaje nowego użytkownika
    public bool AddUser(string name, string email)
    {
        if (Users.ContainsKey(email)) { Console.WriteLine($"Użytkownik {email} już istnieje."); return false; }
        Users[email] = new User(name, email);
        Console.WriteLine($"Dodano użytkownika {name}.");
        return true;
    }

    // Dodaje nowy projekt
    public void AddProject(string name)
    {
        Projects.Add(new Project(name));
        Console.WriteLine($"Dodano projekt {name}.");
    }

    // Znajduje projekt po nazwie
    public Project GetProject(string name) => 
        Projects.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    // Dodaje zadanie do projektu
    public bool AddTaskToProject(string projectName, string title, string description, DateTime dueDate)
    {
        var project = GetProject(projectName);
        if (project == null) { Console.WriteLine($"Nie znaleziono projektu \"{projectName}\"."); return false; }
        if (project.GetTask(title) != null) { Console.WriteLine($"Zadanie \"{title}\" już istnieje."); return false; }
        
        project.AddTask(new Task(title, description, dueDate));
        Console.WriteLine($"Dodano zadanie \"{title}\".");
        return true;
    }

    // Przypisuje użytkownika do zadania
    public bool AssignUserToTask(string projectName, string taskTitle, string userEmail)
    {
        var project = GetProject(projectName);
        if (project == null) { Console.WriteLine("Nie znaleziono projektu."); return false; }
        
        var task = project.GetTask(taskTitle);
        if (task == null) { Console.WriteLine("Nie znaleziono zadania."); return false; }
        
        if (!Users.TryGetValue(userEmail, out User user)) { Console.WriteLine("Nie znaleziono użytkownika."); return false; }
        
        task.AssignUser(user);
        Console.WriteLine($"Przypisano zadanie do użytkownika {user.Name}.");
        return true;
    }

    // Zmienia status zadania
    public bool ChangeTaskStatus(string projectName, string taskTitle, TaskStatus newStatus)
    {
        var project = GetProject(projectName);
        if (project == null) { Console.WriteLine("Nie znaleziono projektu."); return false; }
        
        var task = project.GetTask(taskTitle);
        if (task == null) { Console.WriteLine("Nie znaleziono zadania."); return false; }
        
        task.Status = newStatus;
        Console.WriteLine($"Zmieniono status zadania na {newStatus}.");
        return true;
    }

    // Wyświetla wszystkie projekty i ich zadania
    public void DisplayAllProjects()
    {
        Console.WriteLine("=== Projekty ===");
        if (Projects.Count == 0) { Console.WriteLine("Brak projektów."); return; }
        
        foreach (var project in Projects)
        {
            Console.WriteLine(project);
            project.DisplayTasks();
            Console.WriteLine();
        }
    }

    // Wyświetla wszystkich użytkowników i ich zadania
    public void DisplayAllUsers()
    {
        Console.WriteLine("=== Użytkownicy ===");
        if (Users.Count == 0) { Console.WriteLine("Brak użytkowników."); return; }
        
        foreach (var user in Users.Values)
        {
            Console.WriteLine(user);
            user.DisplayTasks();
            Console.WriteLine();
        }
    }
}

// Klasa główna programu obsługująca interfejs użytkownika
class Program
{
    static void Main(string[] args)
    {
        var todoManager = new ToDoManager();
        
        // Dodanie przykładowych danych
        todoManager.AddUser("Jan Kowalski", "jan@przykład.pl");
        todoManager.AddUser("Anna Nowak", "anna@przykład.pl");
        todoManager.AddProject("Strona WWW");
        todoManager.AddProject("Aplikacja mobilna");
        todoManager.AddTaskToProject("Strona WWW", "Makiety", "Stworzyć makiety UI", DateTime.Now.AddDays(7));
        todoManager.AddTaskToProject("Aplikacja mobilna", "Architektura", "Przygotować schemat", DateTime.Now.AddDays(5));
        
        // Główna pętla programu
        while (true)
        {
            // Wyświetlenie menu
            Console.Clear();
            Console.WriteLine("\n=== ToDo Manager ===");
            Console.WriteLine("1. Dodaj użytkownika");
            Console.WriteLine("2. Dodaj projekt");
            Console.WriteLine("3. Dodaj zadanie");
            Console.WriteLine("4. Przypisz użytkownika");
            Console.WriteLine("5. Zmień status");
            Console.WriteLine("6. Pokaż projekty");
            Console.WriteLine("7. Pokaż użytkowników");
            Console.WriteLine("8. Pokaż zadania wg statusu");
            Console.WriteLine("9. Pokaż przeterminowane");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybór: ");
            
            // Obsługa wyboru użytkownika
            string choice = Console.ReadLine() ?? "";
            Console.Clear();
            
            if (choice == "0") break;
            
            switch (choice)
            {
                case "1": AddUser(todoManager); break;                  // Dodawanie użytkownika
                case "2": AddProject(todoManager); break;               // Dodawanie projektu
                case "3": AddTask(todoManager); break;                  // Dodawanie zadania
                case "4": AssignUserToTask(todoManager); break;         // Przypisywanie użytkownika
                case "5": ChangeTaskStatus(todoManager); break;         // Zmiana statusu
                case "6": todoManager.DisplayAllProjects(); break;      // Wyświetlenie projektów
                case "7": todoManager.DisplayAllUsers(); break;         // Wyświetlenie użytkowników
                case "8": ShowTasksByStatus(todoManager); break;        // Filtrowanie zadań
                case "9": ShowOverdueTasks(todoManager); break;         // Przeterminowane zadania
                default: Console.WriteLine("Niepoprawna opcja."); break;
            }
            
            // Poczekaj na klawisz przed powrotem do menu
            if (choice != "0") WaitForKey();
        }
        
        Console.WriteLine("Do widzenia!");
    }
    
    // Pobiera projekt na podstawie wejścia użytkownika (numer lub nazwa)
    static Project GetProjectByInput(ToDoManager manager, string input)
    {
        if (int.TryParse(input, out int index) && index > 0 && index <= manager.Projects.Count)
            return manager.Projects[index - 1];
        return manager.GetProject(input);
    }
    
    // Wyświetla listę dostępnych projektów
    static void DisplayProjects(ToDoManager manager)
    {
        if (manager.Projects.Count == 0) { Console.WriteLine("Brak projektów."); return; }
        
        Console.WriteLine("=== Dostępne projekty ===");
        for (int i = 0; i < manager.Projects.Count; i++)
            Console.WriteLine($"  {i+1}. {manager.Projects[i].Name}");
        Console.WriteLine();
    }
    
    // Wyświetla listę dostępnych użytkowników
    static void DisplayUsers(ToDoManager manager)
    {
        if (manager.Users.Count == 0) { Console.WriteLine("Brak użytkowników."); return; }
        
        Console.WriteLine("=== Dostępni użytkownicy ===");
        int i = 1;
        foreach (var user in manager.Users.Values)
            Console.WriteLine($"  {i++}. {user.Name} ({user.Email})");
        Console.WriteLine();
    }
    
    // Wyświetla listę zadań w projekcie
    static void DisplayTasks(Project project)
    {
        if (project.Tasks.Count == 0) { Console.WriteLine("Brak zadań w projekcie."); return; }
        
        Console.WriteLine($"=== Zadania w projekcie {project.Name} ===");
        for (int i = 0; i < project.Tasks.Count; i++)
            Console.WriteLine($"  {i+1}. {project.Tasks[i].Title}");
        Console.WriteLine();
    }
    
    // Wybiera projekt i zadanie, zwraca ich nazwy
    static bool SelectProjectAndTask(ToDoManager manager, out string selectedProject, out string selectedTask)
    {
        selectedProject = selectedTask = "";
        
        DisplayProjects(manager);
        if (manager.Projects.Count == 0) { Console.WriteLine("Brak projektów."); return false; }
        
        Console.Write("Wybierz projekt (numer lub nazwa): ");
        var project = GetProjectByInput(manager, Console.ReadLine() ?? "");
        if (project == null) { Console.WriteLine("Nie znaleziono projektu."); return false; }
        selectedProject = project.Name;
        
        DisplayTasks(project);
        if (project.Tasks.Count == 0) { Console.WriteLine("Brak zadań."); return false; }
        
        Console.Write("Wybierz zadanie (numer lub tytuł): ");
        string input = Console.ReadLine() ?? "";
        
        Task task = null;
        if (int.TryParse(input, out int taskIndex) && taskIndex > 0 && taskIndex <= project.Tasks.Count)
            task = project.Tasks[taskIndex - 1];
        else
            task = project.GetTask(input);
        
        if (task == null) { Console.WriteLine("Nie znaleziono zadania."); return false; }
        selectedTask = task.Title;
        return true;
    }
    
    // Dodaje nowego użytkownika
    static void AddUser(ToDoManager manager)
    {
        Console.WriteLine("=== Dodawanie użytkownika ===");
        Console.Write("Imię: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";
        
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            Console.WriteLine("Imię i email nie mogą być puste.");
        else
            manager.AddUser(name, email);
    }
    
    // Dodaje nowy projekt
    static void AddProject(ToDoManager manager)
    {
        Console.WriteLine("=== Dodawanie projektu ===");
        Console.Write("Nazwa projektu: ");
        string name = Console.ReadLine() ?? "";
        
        if (string.IsNullOrEmpty(name))
            Console.WriteLine("Nazwa projektu nie może być pusta.");
        else
            manager.AddProject(name);
    }
    
    // Dodaje nowe zadanie do projektu
    static void AddTask(ToDoManager manager)
    {
        Console.WriteLine("=== Dodawanie zadania ===");
        DisplayProjects(manager);
        if (manager.Projects.Count == 0) { Console.WriteLine("Brak projektów."); return; }
        
        Console.Write("Wybierz projekt (numer lub nazwa): ");
        var project = GetProjectByInput(manager, Console.ReadLine() ?? "");
        if (project == null) { Console.WriteLine("Nie znaleziono projektu."); return; }
        
        Console.Write("Tytuł zadania: ");
        string title = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(title)) { Console.WriteLine("Tytuł nie może być pusty."); return; }
        
        Console.Write("Opis: ");
        string desc = Console.ReadLine() ?? "";
        
        Console.Write("Termin (dd.MM.rrrr): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate)) { Console.WriteLine("Nieprawidłowy format daty."); return; }
        
        manager.AddTaskToProject(project.Name, title, desc, dueDate);
    }
    
    // Przypisuje użytkownika do zadania
    static void AssignUserToTask(ToDoManager manager)
    {
        Console.WriteLine("=== Przypisywanie użytkownika ===");
        if (!SelectProjectAndTask(manager, out string project, out string task)) return;
        
        DisplayUsers(manager);
        if (manager.Users.Count == 0) { Console.WriteLine("Brak użytkowników."); return; }
        
        Console.Write("Wybierz użytkownika (numer lub email): ");
        string input = Console.ReadLine() ?? "";
        
        string email = "";
        if (int.TryParse(input, out int userIndex) && userIndex > 0 && userIndex <= manager.Users.Count)
            email = manager.Users.Keys.ElementAt(userIndex - 1);
        else
            email = input;
        
        if (string.IsNullOrEmpty(email)) { Console.WriteLine("Email nie może być pusty."); return; }
        
        manager.AssignUserToTask(project, task, email);
    }
    
    // Zmienia status zadania
    static void ChangeTaskStatus(ToDoManager manager)
    {
        Console.WriteLine("=== Zmiana statusu zadania ===");
        if (!SelectProjectAndTask(manager, out string project, out string task)) return;
        
        Console.WriteLine("Dostępne statusy:");
        Console.WriteLine("0 - ToDo");
        Console.WriteLine("1 - InProgress");
        Console.WriteLine("2 - Done");
        Console.Write("Wybierz status: ");
        
        if (!int.TryParse(Console.ReadLine(), out int status) || !Enum.IsDefined(typeof(TaskStatus), status))
            { Console.WriteLine("Nieprawidłowy status."); return; }
        
        manager.ChangeTaskStatus(project, task, (TaskStatus)status);
    }
    
    // Wyświetla zadania według statusu
    static void ShowTasksByStatus(ToDoManager manager)
    {
        Console.WriteLine("=== Zadania według statusu ===");
        DisplayProjects(manager);
        if (manager.Projects.Count == 0) { Console.WriteLine("Brak projektów."); return; }
        
        Console.Write("Wybierz projekt (numer lub nazwa): ");
        var project = GetProjectByInput(manager, Console.ReadLine() ?? "");
        if (project == null) { Console.WriteLine("Nie znaleziono projektu."); return; }
        
        Console.WriteLine("Dostępne statusy:");
        Console.WriteLine("0 - ToDo");
        Console.WriteLine("1 - InProgress");
        Console.WriteLine("2 - Done");
        Console.Write("Wybierz status: ");
        
        if (!int.TryParse(Console.ReadLine(), out int status) || !Enum.IsDefined(typeof(TaskStatus), status))
            { Console.WriteLine("Nieprawidłowy status."); return; }
        
        project.DisplayTasks((TaskStatus)status);
    }
    
    // Wyświetla przeterminowane zadania
    static void ShowOverdueTasks(ToDoManager manager)
    {
        Console.WriteLine("=== Przeterminowane zadania ===");
        DisplayProjects(manager);
        if (manager.Projects.Count == 0) { Console.WriteLine("Brak projektów."); return; }
        
        Console.Write("Wybierz projekt (numer lub nazwa): ");
        var project = GetProjectByInput(manager, Console.ReadLine() ?? "");
        if (project == null) { Console.WriteLine("Nie znaleziono projektu."); return; }
        
        project.DisplayOverdueTasks();
    }
    
    // Czeka na naciśnięcie klawisza
    static void WaitForKey()
    {
        Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
}
