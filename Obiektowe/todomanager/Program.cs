using System;
using System.Collections.Generic;
using System.Linq;

enum TaskStatus { ToDo, InProgress, Done }

class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void DisplayTasks()
    {
        Console.WriteLine($"Zadania dla {Name} ({Email}):");
        if (Tasks.Count == 0)
        {
            Console.WriteLine("  Brak zadań.");
            return;
        }
        foreach (var task in Tasks)
            Console.WriteLine($"  {task}");
    }

    public override string ToString() => $"{Name} ({Email})";
}

class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public User AssignedUser { get; set; }

    public Task(string title, string description, DateTime dueDate)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
    }

    public void AssignUser(User user)
    {
        if (AssignedUser != null)
            AssignedUser.Tasks.Remove(this);
        
        AssignedUser = user;
        
        if (user != null)
            user.Tasks.Add(this);
    }

    public override string ToString()
    {
        string userInfo = AssignedUser == null ? "Nieprzypisane" : AssignedUser.Name;
        string overdueInfo = DueDate < DateTime.Now && Status != TaskStatus.Done ? " [ZALEGŁE]" : "";
        return $"Zadanie: {Title}{overdueInfo} | Opis: {Description} | Termin: {DueDate.ToShortDateString()} | Status: {Status} | Przypisane: {userInfo}";
    }
}

class Project
{
    public string Name { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();

    public Project(string name) => Name = name;

    public void AddTask(Task task) => Tasks.Add(task);

    public Task GetTask(string title) => Tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

    public void DisplayTasks(TaskStatus? filterStatus = null)
    {
        var tasks = filterStatus.HasValue 
            ? Tasks.Where(t => t.Status == filterStatus.Value).ToList() 
            : Tasks;
            
        Console.WriteLine($"Zadania w projekcie {Name}" + (filterStatus.HasValue ? $" o statusie {filterStatus}" : ""));
        
        if (tasks.Count == 0)
        {
            Console.WriteLine("  Brak zadań.");
            return;
        }
        
        foreach (var task in tasks)
            Console.WriteLine($"  {task}");
    }

    public void DisplayOverdueTasks()
    {
        Console.WriteLine($"Przeterminowane zadania w projekcie {Name}:");
        var overdueTasks = Tasks.Where(t => t.DueDate < DateTime.Now && t.Status != TaskStatus.Done).ToList();
        
        if (overdueTasks.Count == 0)
        {
            Console.WriteLine("  Brak przeterminowanych zadań.");
            return;
        }
        
        foreach (var task in overdueTasks)
            Console.WriteLine($"  {task}");
    }

    public override string ToString() => $"Projekt: {Name} (Zadań: {Tasks.Count})";
}

class ToDoManager
{
    public Dictionary<string, User> Users { get; } = new Dictionary<string, User>();
    public List<Project> Projects { get; } = new List<Project>();

    public bool AddUser(string name, string email)
    {
        if (Users.ContainsKey(email))
        {
            Console.WriteLine($"Użytkownik o emailu {email} już istnieje.");
            return false;
        }
        
        Users[email] = new User(name, email);
        Console.WriteLine($"Dodano użytkownika {name}.");
        return true;
    }

    public void AddProject(string name)
    {
        Projects.Add(new Project(name));
        Console.WriteLine($"Dodano projekt {name}.");
    }

    public Project GetProject(string name) => Projects.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    public bool AddTaskToProject(string projectName, string title, string description, DateTime dueDate)
    {
        var project = GetProject(projectName);
        if (project == null)
        {
            Console.WriteLine($"Nie znaleziono projektu \"{projectName}\".");
            return false;
        }
        
        // Check if task with this title already exists
        if (project.GetTask(title) != null)
        {
            Console.WriteLine($"Zadanie o tytule \"{title}\" już istnieje w projekcie.");
            return false;
        }
        
        project.AddTask(new Task(title, description, dueDate));
        Console.WriteLine($"Dodano zadanie \"{title}\".");
        return true;
    }

    public bool AssignUserToTask(string projectName, string taskTitle, string userEmail)
    {
        var project = Projects.FirstOrDefault(p => p.Name == projectName);
        if (project == null)
        {
            Console.WriteLine($"Nie znaleziono projektu.");
            return false;
        }
        
        var task = project.Tasks.FirstOrDefault(t => t.Title == taskTitle);
        if (task == null)
        {
            Console.WriteLine("Nie znaleziono zadania.");
            return false;
        }
        
        if (!Users.TryGetValue(userEmail, out User? user) || user == null)
        {
            Console.WriteLine("Nie znaleziono użytkownika.");
            return false;
        }
        
        task.AssignUser(user);
        Console.WriteLine($"Przypisano zadanie do użytkownika {user.Name}.");
        return true;
    }

    public bool ChangeTaskStatus(string projectName, string taskTitle, TaskStatus newStatus)
    {
        var project = GetProject(projectName);
        if (project == null)
        {
            Console.WriteLine($"Nie znaleziono projektu \"{projectName}\".");
            return false;
        }
        
        var task = project.GetTask(taskTitle);
        if (task == null)
        {
            Console.WriteLine($"Nie znaleziono zadania \"{taskTitle}\".");
            return false;
        }
        
        task.Status = newStatus;
        Console.WriteLine($"Zmieniono status zadania na {newStatus}.");
        return true;
    }

    public void DisplayAllProjects()
    {
        Console.WriteLine("=== Projekty ===");
        if (Projects.Count == 0)
        {
            Console.WriteLine("Brak projektów.");
            return;
        }
        
        foreach (var project in Projects)
        {
            Console.WriteLine(project);
            project.DisplayTasks();
            Console.WriteLine();
        }
    }

    public void DisplayAllUsers()
    {
        Console.WriteLine("=== Użytkownicy ===");
        if (Users.Count == 0)
        {
            Console.WriteLine("Brak użytkowników.");
            return;
        }
        
        foreach (var user in Users.Values)
        {
            Console.WriteLine(user);
            user.DisplayTasks();
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var todoManager = new ToDoManager();
        bool exit = false;
        
        // Przykładowe dane
        todoManager.AddUser("Jan Kowalski", "jan@przykład.pl");
        todoManager.AddUser("Anna Nowak", "anna@przykład.pl");
        todoManager.AddProject("Strona WWW");
        todoManager.AddProject("Aplikacja mobilna");
        todoManager.AddTaskToProject("Strona WWW", "Makiety", "Stworzyć makiety UI", DateTime.Now.AddDays(7));
        todoManager.AddTaskToProject("Aplikacja mobilna", "Architektura", "Przygotować schemat", DateTime.Now.AddDays(5));
        
        while (!exit)
        {
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
            
            string? choice = Console.ReadLine();
            Console.WriteLine();
            
            switch (choice)
            {
                case "1": // Dodawanie użytkownika
                    Console.Write("Imię: ");
                    string? name = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    string? email = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
                        todoManager.AddUser(name, email);
                    else
                        Console.WriteLine("Imię i email nie mogą być puste.");
                    break;
                    
                case "2": // Dodawanie projektu
                    Console.Write("Nazwa projektu: ");
                    string? projectName = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(projectName))
                        todoManager.AddProject(projectName);
                    else
                        Console.WriteLine("Nazwa projektu nie może być pusta.");
                    break;
                    
                case "3": // Dodawanie zadania do projektu
                    Console.Write("Projekt: ");
                    string? projName = Console.ReadLine() ?? "";
                    Console.Write("Tytuł: ");
                    string? title = Console.ReadLine() ?? "";
                    Console.Write("Opis: ");
                    string? desc = Console.ReadLine() ?? "";
                    Console.Write("Termin (dd.MM.rrrr): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate) && 
                        !string.IsNullOrEmpty(projName) && 
                        !string.IsNullOrEmpty(title))
                        todoManager.AddTaskToProject(projName, title, desc, dueDate);
                    else
                        Console.WriteLine("Niepoprawne dane.");
                    break;
                    
                case "4": // Przypisywanie użytkownika do zadania
                    Console.Write("Projekt: ");
                    string? pName = Console.ReadLine() ?? "";
                    Console.Write("Zadanie: ");
                    string? tTitle = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    string? uEmail = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(pName) && !string.IsNullOrEmpty(tTitle) && !string.IsNullOrEmpty(uEmail))
                        todoManager.AssignUserToTask(pName, tTitle, uEmail);
                    else
                        Console.WriteLine("Niepoprawne dane.");
                    break;
                    
                case "5": // Zmiana statusu zadania
                    Console.Write("Projekt: ");
                    string? prName = Console.ReadLine() ?? "";
                    Console.Write("Zadanie: ");
                    string? taTitle = Console.ReadLine() ?? "";
                    Console.Write("Status (0=ToDo, 1=InProgress, 2=Done): ");
                    if (int.TryParse(Console.ReadLine(), out int status) && 
                        Enum.IsDefined(typeof(TaskStatus), status) &&
                        !string.IsNullOrEmpty(prName) && 
                        !string.IsNullOrEmpty(taTitle))
                        todoManager.ChangeTaskStatus(prName, taTitle, (TaskStatus)status);
                    else
                        Console.WriteLine("Niepoprawne dane.");
                    break;
                    
                case "6": // Wyświetlanie wszystkich projektów
                    todoManager.DisplayAllProjects();
                    break;
                    
                case "7": // Wyświetlanie wszystkich użytkowników
                    todoManager.DisplayAllUsers();
                    break;
                    
                case "8": // Wyświetlanie zadań wg statusu
                    Console.Write("Projekt: ");
                    string? pForStatus = Console.ReadLine() ?? "";
                    if (string.IsNullOrEmpty(pForStatus))
                    {
                        Console.WriteLine("Niepoprawna nazwa projektu.");
                        break;
                    }
                    
                    Console.Write("Status (0=ToDo, 1=InProgress, 2=Done): ");
                    if (int.TryParse(Console.ReadLine(), out int sFilter) && Enum.IsDefined(typeof(TaskStatus), sFilter))
                    {
                        var project = todoManager.Projects.FirstOrDefault(p => p.Name == pForStatus);
                        if (project != null)
                            project.DisplayTasks((TaskStatus)sFilter);
                        else
                            Console.WriteLine("Nie znaleziono projektu.");
                    }
                    else
                        Console.WriteLine("Niepoprawny status.");
                    break;
                    
                case "9": // Wyświetlanie przeterminowanych zadań
                    Console.Write("Projekt: ");
                    string? pForOverdue = Console.ReadLine() ?? "";
                    if (string.IsNullOrEmpty(pForOverdue))
                    {
                        Console.WriteLine("Niepoprawna nazwa projektu.");
                        break;
                    }
                    
                    var proj = todoManager.Projects.FirstOrDefault(p => p.Name == pForOverdue);
                    if (proj != null)
                        proj.DisplayOverdueTasks();
                    else
                        Console.WriteLine("Nie znaleziono projektu.");
                    break;
                    
                case "0": // Wyjście z programu
                    exit = true;
                    break;
                    
                default:
                    Console.WriteLine("Niepoprawna opcja.");
                    break;
            }
        }
        
        Console.WriteLine("Do widzenia!");
    }
}
