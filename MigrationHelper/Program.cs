using System.Diagnostics;

Directory.SetCurrentDirectory("../../../../Backend");

var running = true;
while (running) {
    MainMenu();
}

void MainMenu() {
    Console.Clear();
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("[1] Update database");
    Console.WriteLine("[2] Generate new migration from code");
    Console.WriteLine("[3] Reset database");
    Console.WriteLine("[4] Flatten migrations");
    Console.WriteLine("[0] Exit");
    switch (Console.ReadKey().KeyChar) {
        case '0':
            Console.Clear();
            running = false;
            break;
        case '1':
            Console.Clear();
            UpdateDatabase();
            break;
        case '2':
            Console.Clear();
            GenerateMigration();
            break;
        case '3':
            Console.Clear();
            DropDatabase();
            UpdateDatabase();
            break;
        case '4':
            Console.Clear();
            DropDatabase();
            DeleteMigrations();
            GenerateMigrationDirect("Initial");
            break;
    }
}

void UpdateDatabase() {
    Console.WriteLine("Updating database...");
    using var proc = Process.Start("dotnet", "ef database update");
    proc.WaitForExit();
    Pause();
}

void GenerateMigrationDirect(string name) {
    Console.WriteLine("Generating migration...");
    using var proc = Process.Start("dotnet", "ef migrations add " + name.Replace(" ", ""));
    proc.WaitForExit();
    if (proc.ExitCode == 0) {
        UpdateDatabase();
    } else {
        Pause();
    }
}

void GenerateMigration() {
    string? mig;
    do {
        Console.Write("Please enter a migration name: ");
        mig = Console.ReadLine();
    } while (string.IsNullOrEmpty(mig));
    GenerateMigrationDirect(mig);
}

void DropDatabase() {
    string? resp;
    do {
        Console.WriteLine("Are you sure you want to drop the database? (y/n)");
        resp = Console.ReadLine()?.ToLowerInvariant();
    } while (resp != "y" && resp != "n");

    if (resp == "n") {
        return;
    }

    using var proc = Process.Start("dotnet", "ef database drop -f");
    proc.WaitForExit();
}

void DeleteMigrations() {
    const string MIGRATIONS_DIR = "Migrations";
    Console.WriteLine("Deleting migrations...");
    if (Directory.Exists(MIGRATIONS_DIR)) {
        Directory.Delete(MIGRATIONS_DIR, true);
    }
}

void Pause() {
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
