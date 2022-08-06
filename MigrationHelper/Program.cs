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
    }
}

void UpdateDatabase() {
    Console.WriteLine("Updating database...");
    using var proc = Process.Start("dotnet", "ef database update");
    proc.WaitForExit();
    Pause();
}

void GenerateMigration() {
    string? mig;
    do {
        Console.Write("Please enter a migration name: ");
        mig = Console.ReadLine();
    } while (string.IsNullOrEmpty(mig));
    Console.WriteLine("Generating migration...");
    using var proc = Process.Start("dotnet", "ef migrations add " + mig.Replace(' ', '_'));
    proc.WaitForExit();
    if (proc.ExitCode == 0) {
        UpdateDatabase();
    } else {
        Pause();
    }
}

void Pause() {
    Console.Write("Press any key to continue...");
    Console.ReadLine();
}
