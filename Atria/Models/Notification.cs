namespace Atria.Models;
public abstract record Notification {
    private string baseMessage { get; }
    private DateTimeOffset creationTime;
}

    
