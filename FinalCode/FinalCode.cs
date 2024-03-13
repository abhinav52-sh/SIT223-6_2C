using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCode
{
    // enumeration defining all options for the main menu
    enum MenuOption
    {
        ADD_TASK,
        REMOVE_TASK,
        ADD_CATEGORY,
        DELETE_CATEGORY,
        CHANGE_CATEGORY,
        CHANGE_ORDER,
        CHANGE_PRIORITY,
        CHANGE_DATE,
        EXIT
    };

    class FinalCode
    {

        // main function with a switch case for all the options and printing a Task Planner table  
        static void Main(string[] args) 
        {
            // Creating a new planner object and intialising three categories in the planner
            Planner newPlan = new Planner("Daily Planner".ToUpper());
            newPlan.AddCategory(new Category()); newPlan.Categories[0].TCategory = "Personal"; 
            newPlan.AddCategory(new Category()); newPlan.Categories[1].TCategory = "Work";
            newPlan.AddCategory(new Category()); newPlan.Categories[2].TCategory = "Family";
            Console.WriteLine(newPlan.Categories.Count);

            // infinite loop to print the menu repeatedly
            bool exit;
            while (true)
            {
                exit = false;
                // clearing the console window 
                Console.Clear();
                // printing a planner table with all the tasks
                PrintTable(newPlan);

                // reading an enum value after displaying text displaying all the options
                MenuOption option = ReadDisplayMenu();

                // switch case to perform all the functions based on the displayed menu
                switch(option)
                {
                    case MenuOption.ADD_TASK: 
                        AddNewTask(ref newPlan);
                        break;

                    case MenuOption.REMOVE_TASK:
                        TaskDelete(newPlan);
                        break;

                    case MenuOption.ADD_CATEGORY:
                        AddNewCategory(newPlan);
                        break;

                    case MenuOption.DELETE_CATEGORY:
                        DeleteIndexCategory(newPlan);
                        break;

                    case MenuOption.CHANGE_CATEGORY:
                        ChangeTaskCategory(newPlan);
                        break;

                    case MenuOption.CHANGE_ORDER:
                        changeTaskOrder(newPlan);
                        break;
                    
                    case MenuOption.CHANGE_PRIORITY:
                        ChangeTaskPriority(newPlan);
                        break;

                    case MenuOption.CHANGE_DATE:
                        ChangeTaskDate(newPlan);
                        break;
                    
                    case MenuOption.EXIT:
                        exit = true;
                        Console.Write("Quitting");
                        System.Threading.Thread.Sleep(200);
                        Console.Write(".");
                        System.Threading.Thread.Sleep(200);
                        Console.Write(".");
                        System.Threading.Thread.Sleep(200);
                        Console.Write(".");
                        break;

                    default:
                        Console.WriteLine("Enter a valid option.");
                        break;
                }
                if(exit) break;
            }
        }

        static MenuOption ReadDisplayMenu()
        {
            int returnInp = 0;
            // printing a menu until a valid option is read
            while(true)
            {
                try
                {
                    // setting white as the default font color of the menu
                    Console.ForegroundColor = ConsoleColor.White;

                    // printing all the available options
                    Console.WriteLine("\n" + new String('-', 30));
                    Console.Write(new String(' ', 13));
                    Console.WriteLine("Menu");
                    Console.WriteLine(new String('-', 30));
                    Console.WriteLine("1. Add Task");
                    Console.WriteLine("2. Delete Task");
                    Console.WriteLine("3. Add Category");
                    Console.WriteLine("4. Delete Category ");
                    Console.WriteLine("5. Change Task Category");
                    Console.WriteLine("6. Change Task Order");
                    Console.WriteLine("7. Change Task Priority(Color)");
                    Console.WriteLine("8. Change due date");
                    Console.WriteLine("9. Quit");
                    Console.WriteLine("\nEnter option: ");
                    Console.Write(">> ");
                    returnInp = Convert.ToInt32(Console.ReadLine())-1;

                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("\nError: Please choose only valid numbers from the menu");
                }
            }

            return (MenuOption)returnInp;
        }

        static void PrintTable(Planner newPlan)
        {
            // defining the number of seperator dashes
            int noOfDashes = (newPlan.Categories.Count*42) + newPlan.Categories.Count + 1;

            // setting default font color to blue
            Console.ForegroundColor = ConsoleColor.Blue;
            // printing name of the Planner at the centre of the table and then printing seperator dashes
            Console.WriteLine(new string(' ', (noOfDashes/2)+3) + newPlan.PlannerName);
            Console.WriteLine(new string(' ', 10)+new string('-', noOfDashes));

            // printing all category names 
            Console.Write("{0,10}|", "item #");
            for(int i = 0; i < newPlan.Categories.Count; i++)
            {
                Console.Write("{1,2}. {0,38}|", newPlan.Categories[i].TCategory, i);
            }
            Console.WriteLine();
            Console.WriteLine(new string(' ', 10) + new string('-', noOfDashes));

            // finding maximum no of tasks in any of the categories to create that many no of rows
            int max = newPlan.Categories[0].TaskList.Count;
            for(int i = 0; i < newPlan.Categories.Count; i++)
            {
                for(int j = 0; j < newPlan.Categories[i].TaskList.Count; j++)
                if(newPlan.Categories[i].TaskList.Count > max)
                {
                    max = newPlan.Categories[i].TaskList.Count;
                }
            }

            // filling up all the cells of the table with dates and task names and the item/serial number
            for (int i = 0; i < max; i++)
            {
                // printing the item/task/serial number
                Console.Write("{0,10}|", i);

                for(int j = 0; j < newPlan.Categories.Count; j++)
                {
                    
                    // printing due dates and names of the tasks
                    if(newPlan.Categories[j].TaskList.Count > i)
                    {
                        Console.ForegroundColor = newPlan.Categories[j].TaskList[i].TaskPriority;

                        Console.Write(newPlan.Categories[j].TaskList[i].TaskDate);

                        Console.Write("{0,32}|", newPlan.Categories[j].TaskList[i]);
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.Write("{0,42}|", "N/A");
                    }
                }
                
                Console.WriteLine();
                
            }
        }

        // adding a new task in the specified category
        static void AddNewTask(ref Planner newPlan)
        {
            int listNo;
            Task task = new Task();

            while(true)
            {
                try
                {
                    // reading the category to add the task to
                    Console.WriteLine("\nWhich category do you want to place a new task? Enter no: ");
                    Console.Write(">> "); 
                    listNo = Convert.ToInt32(Console.ReadLine());
                    
                    // reading Task name
                    Console.WriteLine("Describe your task below (max. 30 symbols)"); 
                    Console.Write(">> ");
                    task.TaskName = Console.ReadLine(); 
                    if (task.TaskName.Length > 30) task.TaskName = task.TaskName.Substring(0, 30);

                    // creating a new task
                    newPlan.Categories[listNo].AddTask(task);
                    // adding date component with the task
                    ChangeDate(newPlan, listNo, newPlan.Categories[listNo].TaskList.Count-1);
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter date again.");
                }
            }

        }

        static void ChangeDate(Planner newPlan, int listNo, int TaskNo)
        {
            while(true)
            {
                try
                {
                    // reading date, month and year values
                    Console.WriteLine("\nEnter date for due date of this task: ");
                    Console.Write(">> ");
                    int date = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter month for due date of this task: ");
                    Console.Write(">> ");
                    int month = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter year for due date of this task: ");
                    Console.Write(">> ");
                    int year = Convert.ToInt32(Console.ReadLine());

                    // validting the input date
                    if(date>28 || date<0 || month>=12 || month<0 || year>=9999 || year<2023)
                    {
                        if(month == 2 && date > 29 && year%4==0) throw new ArgumentOutOfRangeException("Invalid date. Change date according to the leap year.");
                        if(month == 2 && date > 28) throw new ArgumentOutOfRangeException("Invalid Date. February contains 28 days only.");
                        if((month == 4 || month == 6 || month == 9 || month == 11) && date > 30) throw new ArgumentOutOfRangeException("Invalid date. The month entered contains 30 days only. ");
                        if(date<0 || month<0 || year<0) throw new ArgumentOutOfRangeException("Invalid date. Date cannot be negative.");
                        if(date>31 || month>12 || year>9999) throw new ArgumentOutOfRangeException("Invalid date. Enter valid Date.");
                    }

                    // setting the input date to the associated task
                    newPlan.Categories[listNo].TaskList[TaskNo].TaskDate = new DateOnly(year, month, date);
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter date again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                    Console.WriteLine("Task not initialized. Enter input again.");
                }
            }
        }
        
        // deleting the specified task from the planner
        static void TaskDelete(Planner newPlan)
        {
            int categ = 0, deleteTaskNo = 0;
            while(true)
            {
                try
                {
                    // reading category number where task is to be deleted
                    Console.WriteLine("\nSelect the category from which task is to be deleted");
                    Console.Write(">> ");
                    categ = Convert.ToInt32(Console.ReadLine());
                    if(categ >= newPlan.Categories.Count-1) throw new ArgumentOutOfRangeException("Invalid Category Number. Only enter valid values from the table");
                    
                    Console.WriteLine("Select the item no to be deleted from category " + newPlan.Categories[categ]);
                    Console.Write(">> ");
                    deleteTaskNo =   Convert.ToInt32(Console.ReadLine());
                    if(deleteTaskNo >= newPlan.Categories[categ].TaskList.Count) throw new ArgumentOutOfRangeException("Invalid Task Number. Only enter valid values from the table");
                    
                    newPlan.Categories[categ].DeleteTask(deleteTaskNo);
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter input again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                }
            }
        }

        static void AddNewCategory(Planner newPlan)
        {
            if(newPlan.Categories.Count < 5)
            {
                Console.WriteLine("Enter name of the new category: ");
                Console.Write(">> ");
                string newName = Console.ReadLine();
                if (newName.Length > 30) newName = newName.Substring(0, 30);
                newPlan.AddCategory(new Category());
                newPlan.Categories[newPlan.Categories.Count-1].TCategory = newName; 
            }
            else
            {
                Console.WriteLine("Maximum Category Limit: 5");
                Console.WriteLine("New category cannot be created!");
            }
        }

        static void DeleteIndexCategory(Planner newPlan)
        {
            int deleteNo;
            while(true)
            {
                try
                {
                    Console.WriteLine("Enter the category number to be deleted: ");
                    Console.Write(">> ");
                    deleteNo = Convert.ToInt32(Console.ReadLine());

                    // asking for deletion confirmation 
                    Console.WriteLine($"\nAll the tasks of the category {newPlan.Categories[deleteNo].TCategory} will be deleted!");
                    Console.WriteLine($"To continue: press 'y' else press any key");
                    string inp = Console.ReadLine().ToLower();

                    // deleting the specified category
                    if(inp == "y")
                    {
                        if(deleteNo < 0 && deleteNo >= newPlan.Categories.Count) throw new ArgumentOutOfRangeException("Invalid Category Number. Only enter valid values from the table");
                        else newPlan.RemoveCategory(deleteNo);
                    }
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter input again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                }
            }
        }

        // changing task's category to another category
        static void ChangeTaskCategory(Planner newPlan)
        {
            int categ = 0, moveTaskNo = 0, categ1 = 0;

            while(true)
            {
                try
                {
                    // reading category number of the task to shift
                    Console.WriteLine("Select the category from which task is to be selected: "); 
                    Console.Write(">> ");
                    categ = Convert.ToInt32(Console.ReadLine());
                    if(categ >= newPlan.Categories.Count) throw new ArgumentOutOfRangeException("Invalid Category Number. Enter valid values from table");
                    
                    // reading task number to shift
                    Console.WriteLine("Select the task from category " + newPlan.Categories[categ]);
                    Console.Write(">> ");
                    moveTaskNo =   Convert.ToInt32(Console.ReadLine());
                    if(moveTaskNo >= newPlan.Categories[categ].TaskList.Count) throw new ArgumentOutOfRangeException("Invalid Task Number. Enter valid values from table");
                    
                    // reading category number where task is to be shifted
                    Console.WriteLine("Enter the category number where the task is to be shifted: "); 
                    Console.Write(">> ");
                    categ1 = Convert.ToInt32(Console.ReadLine());
                    if(categ1 >= newPlan.Categories.Count) throw new ArgumentOutOfRangeException("Invalid Category Number. Enter valid values from table");
            
                    // performing the deletion operation
                    newPlan.Categories[categ1].AddTask(newPlan.Categories[categ].TaskList[moveTaskNo]);
                    newPlan.Categories[categ].DeleteTask(moveTaskNo);
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter input again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                }
            }
            
        }

        // Changing order of the specified task in the same category
        static void changeTaskOrder(Planner newPlan)
        {
            int categ = 0, moveTaskNo = 0, newPosition = 0;
            while(true)
            {
                try
                {
                    Console.WriteLine("Select the category from which task is to be selected: "); 
                    Console.Write(">> ");
                    categ = Convert.ToInt32(Console.ReadLine());
                    if(categ >= newPlan.Categories.Count) throw new ArgumentOutOfRangeException("Invalid Category Number. Enter valid values from table");
                    
                    Console.WriteLine("Enter new entry number where task is to be moved: "); 
                    Console.Write(">> ");
                    Console.WriteLine("Select the task from category " + newPlan.Categories[categ]);
                    moveTaskNo =   Convert.ToInt32(Console.ReadLine());
                    if(moveTaskNo >= newPlan.Categories[categ].TaskList.Count) throw new ArgumentOutOfRangeException("Invalid Task Number. Enter valid values from table");

                    Console.WriteLine("Enter the number where task is to be moved: "); 
                    Console.Write(">> ");
                    newPosition = Convert.ToInt32(Console.ReadLine());
                    if(moveTaskNo >= newPlan.Categories[categ].TaskList.Count) throw new ArgumentOutOfRangeException("Invalid Task Number. Enter valid values from table");
                    
                    // swapping the task with the read position
                    Task tempTask;
                    tempTask = newPlan.Categories[categ].TaskList[moveTaskNo];
                    newPlan.Categories[categ].TaskList[moveTaskNo] = newPlan.Categories[categ].TaskList[newPosition];
                    newPlan.Categories[categ].TaskList[newPosition] = tempTask;

                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter input again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                }
            }
        }

        // changing task priority with the help of colors
        static void ChangeTaskPriority(Planner newPlan)
        {
            int categ = 0, taskNo = 0;
            while(true)
            {
                try
                {
                    Console.WriteLine("\nSelect the category from the table: ");
                    categ = Convert.ToInt32(Console.ReadLine());
                    if(categ >= newPlan.Categories.Count) throw new ArgumentOutOfRangeException("Invalid Category Number. Enter valid values from table");

                    Console.WriteLine("Select task from the category " + newPlan.Categories[categ]);
                    taskNo =   Convert.ToInt32(Console.ReadLine());
                    if(taskNo >= newPlan.Categories[categ].TaskList.Count) throw new ArgumentOutOfRangeException("Invalid Task Number. Enter valid values from table");

                    Console.WriteLine("Enter a number to change task priority (1-Red, 2-Yellow, 3-Blue): ");
                    Console.Write(">> ");
                    int inputNo = Convert.ToInt32(Console.ReadLine());
                    if(inputNo < 1 || inputNo > 3) throw new ArgumentOutOfRangeException("Invalid Task Priority Color number. Enter valid values from table");

                    if(inputNo == 1) newPlan.Categories[categ].TaskList[taskNo].TaskPriority = ConsoleColor.Red;
                    if(inputNo == 2) newPlan.Categories[categ].TaskList[taskNo].TaskPriority = ConsoleColor.Yellow;
                    if(inputNo == 3) newPlan.Categories[categ].TaskList[taskNo].TaskPriority = ConsoleColor.Blue;
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter input again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                }
            }
        }

        static void ChangeTaskDate(Planner newPlan)
        {
            int categ = 0, taskNo = 0;
            while(true)
            {
                try
                {
                    Console.WriteLine("\nSelect the category from the table: ");
                    categ = Convert.ToInt32(Console.ReadLine());
                    if(categ >= newPlan.Categories.Count) throw new ArgumentOutOfRangeException("Invalid Category Number. Enter valid values from table");

                    Console.WriteLine("Select task from the category " + newPlan.Categories[categ]);
                    taskNo =   Convert.ToInt32(Console.ReadLine());
                    if(taskNo >= newPlan.Categories[categ].TaskList.Count) throw new ArgumentOutOfRangeException("Invalid Task Number. Enter valid values from table");

                    ChangeDate(newPlan, categ, taskNo);

                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error: Only integral values from the table are accepted.");
                    Console.WriteLine("Enter input again.");
                }
                catch(ArgumentOutOfRangeException excp)
                {
                    Console.WriteLine($"Error: {excp.GetType()} - {excp.Message}");
                }
            }
        }
    }

    // creating a planner class to contain all the categories and tasks
    class Planner
    {
        // list of all the categories
        public List<Category> Categories = new List<Category>();
        
        // name of the planner table
        public String PlannerName {get; set;}

        // Constructors
        public Planner() 
        {
            Categories = new List<Category>();
        }
        public Planner(string name) 
        {
            Categories = new List<Category>();
            PlannerName = name;

        }

        // Methods
        public void AddCategory(Category newCategory)
        {
            Categories.Add(newCategory);
            Categories[Categories.Count-1] = new Category();
        }

        public void RemoveCategory(int categNo)
        {
            Categories.RemoveAt(categNo);
        }
    }

    // class containing all the tasks
    class Category
    {
        // list of all the tasks
        public List<Task> TaskList;

        // name of the current category
        public String TCategory {get; set;}
        
        // Constructors
        public Category() 
        {
            TaskList  = new List<Task>();
        }

        public Category(string categName)
        {
            TaskList  = new List<Task>();
            TCategory = categName;
        }

        // Methods
        public void AddTask(Task task)
        {
            TaskList.Add(task);
        }
        public void DeleteTask(int index)
        {
            TaskList.RemoveAt(index);
        }

        public override string ToString()
        {
            return TCategory;
        }
    }

    // class defining a task
    class Task
    {
        // defining variable identifying a task
        private DateOnly dueDate;
        private ConsoleColor taskColor;
        private string taskName = " ";

        // Properties to access state of a task
        public string TaskName 
        { 
            get {return taskName;} 
            set{taskName = value;} 
        }
        public ConsoleColor TaskPriority 
        { 
            get {return taskColor;} 
            set{taskColor = value;} 
        }
        public DateOnly TaskDate 
        { 
            get {return dueDate;} 
            set{dueDate = value;} 
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
}
