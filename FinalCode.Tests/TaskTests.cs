// class defining a task
public class Task
{
    // defining variables identifying a task
    private DateOnly dueDate;
    private ConsoleColor taskColor;
    private string taskName = " ";

    // Properties to access the state of a task
    public string TaskName 
    { 
        get {return taskName;} 
        set {taskName = value;} 
    }
    public ConsoleColor TaskPriority 
    { 
        get {return taskColor;} 
        set {taskColor = value;} 
    }
    public DateOnly TaskDate 
    { 
        get {return dueDate;} 
        set {dueDate = value;} 
    }

    // Constructor
    public Task() 
    {
        dueDate = new DateOnly(2000, 1, 1);
        taskColor = ConsoleColor.Blue;
    }

    // Methods
    public override string ToString()
    {
        return TaskName;
    }
}
