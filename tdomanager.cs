using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;

using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


//C:\Users\ManavBhatia\OneDrive - Kongsberg Digital AS\Documents\C##\TaskPadApp\todolist.json

namespace TaskPadApp
{
    public class TaskItem 
    {
        public static int count = 0;
        public static int pcount = 0;
        public int id { get; set; }
        public string title {  get; set; }
        public string description { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? duedate { get; set; }
        public int priority { get; set; }


        

        public TaskItem(string title , string description)
        { 
            this.title = title;
            this.description = description;
            this.id = ++count;
            this.isCompleted = false;
            this.duedate = null;
            this.priority = 0;
        }

        public void display()
        {
            Console.WriteLine($"id                  : {this.id}");
            Console.WriteLine($"Title               : {this.title}");
            Console.WriteLine($"Description         : {this.description}");
            if (this.priority != 0) Console.WriteLine($"Priority            : {this.priority}");
            if (this.duedate != null) Console.WriteLine($"Due date             : {this.duedate}");
            Console.WriteLine($"Completion Status   : {this.isCompleted}\n");
        }
    }
//    DateTime userDateTime;
//if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
//{
//     Console.WriteLine("The day of the week is: " + userDateTime.DayOfWeek);
//}
//else
//{
//    Console.WriteLine("You have entered an incorrect value.");
//}
//Console.ReadLine();

public class ToDoManager 
    {
        public List<TaskItem> ToDolist = new List<TaskItem>();
        

        public string getString(string displaytext)
        {
            while (true)
            {
                Console.Write($"{displaytext}");
                string s = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(s)) { Console.WriteLine("\nPlease input a value."); }
                else return s;
            }
        }
        
        public string getyesno(string displaytext)
        {
            while (true)
            {
                Console.Write($"{displaytext}");
                string s = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(s)) { Console.WriteLine("\nPlease input a value."); }
                else
                {
                    if (s == "y" || s == "Y" || s == "n" || s == "N") return s;
                }
            }
        }

        public int getID()
        {
            while (true)
            {
                Console.Write($"\nId  : ");
                int id;
                if(int.TryParse(Console.ReadLine(), out id) && id>0 ) { Console.WriteLine("\n"); return id; }
                else Console.WriteLine("\nPlease provide correct id.");
            }
        }

        public int getoptionint(int limit)
        {
            while (true)
            {
                
                int a;
                Console.Write("Enter choice : ");
                if (int.TryParse(Console.ReadLine(), out a) && a > 0 && a <= limit) { Console.WriteLine("\n"); return a; }
                else Console.WriteLine("\nPlease provide correct option.\n");
            }
        }


        public void displayMenu()
        {


            bool temp = true;
            while (temp==true)
            {
                Console.WriteLine("\n     Menu\n");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. view all tasks");
                Console.WriteLine("3. view specific task");
                Console.WriteLine("4. mark task as completed");
                Console.WriteLine("5. update task");
                Console.WriteLine("6. delete a task");
                Console.WriteLine("7. save tasks");
                Console.WriteLine("8. load tasks");
                Console.WriteLine("9. sort and filter");
                Console.WriteLine("10. priority tasks");
                Console.WriteLine("11. exit\n");

                

                int c = getoptionint(11);

                switch (c)
                {
                    case 1:

                        //Console.Write("Enter title : ");
                        string title = getString("Title           : ");
                        //Console.Write("Enter description : ");
                        string description = getString("Description     : ");
                        //Console.Write("Add a duedate ? (Y/N)  ");
                        DateTime userDateTime = DateTime.Now ;     bool bool1=false , bool2=false ;
                        
                        string duedateoption = getyesno("DueDate (Y/N)   : ");

                        if (duedateoption == "Y" || duedateoption == "y")
                        {
                            while (true)
                            {
                                Console.Write("DATE (DD-MM-YYYY) : ");
                                if (DateTime.TryParse(Console.ReadLine(), out userDateTime)) { bool1 = true; break; }
                                else Console.WriteLine("You have entered an incorrect value.");
                            }
                        }
                        
                        
                        //Console.Write("Add a Priority ? (Y/N)  ");
                        
                        string priorityoption = getyesno("Priority (Y/N)  : ");
                        if (priorityoption == "Y" || priorityoption == "y") bool2 = true;
                       
                        if (bool1 && bool2) addTask(title,description,userDateTime,true);
                        else if( !bool1 && bool2 ) addTask(title,description,null,true);
                        else if (bool1 && !bool2) addTask(title,description,userDateTime,false);
                        else addTask(title,description);
                        break;

                    case 2:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else ViewallTasks();
                        break;

                    case 3:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else
                        {
                            int id1 = getID();
                            ViewSpecificTask(id1);
                        }
                        break;

                    case 4:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else
                        {
                            int id2 = getID();
                            MarkTaskasCompleted(id2);
                        }
                        break;

                    case 5:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else
                        {
                            int id3 = getID();
                            updatetask(id3);
                        }
                        break;

                    case 6:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else
                        {
                            int id4 = getID();
                            deletetask(id4);
                        }
                        break;

                    case 7:

                        saveTasks();

                        break;
                            
                    case 8:

                        loadTasks();

                        break;
                            
                    case 9:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else
                        {
                            bool temp1 = true;
                            while (temp1 == true)
                            {
                                Console.WriteLine("1.Title");
                                Console.WriteLine("2.Due date");
                                Console.WriteLine("3.Priority");
                                Console.WriteLine("4.Exit\n");
                                Console.Write("Enter option : ");
                                int a = getoptionint(4);
                                switch (a)
                                {
                                    case 1:
                                        ViewallTasksViaTitle();
                                        break;

                                    case 2:
                                        ViewallTasksViaDueDate();
                                        break;

                                    case 3:
                                        ViewallTasksViaPriority();
                                        break;

                                    case 4:
                                        temp1 = false;
                                        break;
                                }
                            }
                        }
                        
                        break;

                    case 10:
                        if (ToDolist.Count == 0) Console.WriteLine("No tasks created.");
                        else
                        {
                            int id5 = getID();
                            addPriority(id5);
                        }
                        break;


                    case 11:
                        temp = false; 
                        break;

                }


            }
        }

        public void addTask(string title, string description, DateTime? duedate = null , bool priority = false)
        {
            
            
            TaskItem task = new TaskItem(title, description);
            task.duedate = duedate;
            if (priority == true) task.priority = ++TaskItem.pcount;
            ToDolist.Add(task);

            
        }

        public void ViewallTasks()
        {
            
            Console.WriteLine("    Display all tasks : \n");
            foreach (TaskItem task in ToDolist) task.display();
           
        }

        public void ViewallTasksViaTitle()
        {
            //List<TaskItem> ToDolist1 = ToDolist;
            //ToDolist1.Sort( (task1 , task2) => task1.title.CompareTo(task2.title));
            
            var query = from item in ToDolist
                            orderby item.title
                            select item;
            foreach (TaskItem task in query) task.display();
        }
        public void ViewallTasksViaPriority()
        {
            
            var query = from item in ToDolist
                        where item.priority != 0
                        orderby item.priority
                        select item;
            foreach (TaskItem task in query) task.display();

            var querynull = from item in ToDolist
                        where item.priority == 0
                        orderby item.priority
                        select item;
            foreach (TaskItem task in querynull) task.display();
        }
        public void ViewallTasksViaDueDate()
        {
            
            var query = from item in ToDolist
                        where item.duedate != null
                        orderby item.duedate
                        select item;
            foreach (TaskItem task in query) task.display();

            var querynot = from item in ToDolist
                        where item.duedate == null
                        orderby item.duedate
                        select item;
            foreach (TaskItem task in querynot) task.display();

        }

        public void ViewSpecificTask( int id )
        {
            
            var query = from item in ToDolist
                        where item.id == id
                        select item;
            foreach (TaskItem task in query) task.display();

        }


        public void MarkTaskasCompleted( int id)
        {
            
            var itask = (from item in ToDolist
                        where item.id == id
                        select item);
            foreach (var task in itask)
            {
                if (task.isCompleted == false)
                {
                    if (task.priority != 0)
                    {
                        var tasksprioritylesserthancurrenttask = from item in ToDolist
                                                                 where item.priority > task.priority
                                                                 select item;
                        
                        foreach (var item in tasksprioritylesserthancurrenttask) --item.priority;

                        --TaskItem.pcount;
                        task.priority = 0;
                    }

                    task.isCompleted = true;
                    task.duedate = null;
                    task.display();

                    Console.WriteLine("\nTask completed successfully.");
                }
                else Console.WriteLine("Task already completed.");
            }
            
        }

        public void updatetask(int id)
        {
            
            var itask = from item in ToDolist
                                       where item.id == id
                                       select item;
            bool temp = true;
            foreach (var task in itask)
            {
                while (temp==true)
                {
                    Console.WriteLine("1. Title");
                    Console.WriteLine("2. Description");
                    Console.WriteLine("3. Due date");
                    Console.WriteLine("4. Mark task as completed");
                    Console.WriteLine("5. Exit\n");
                    
                    int a = getoptionint(5);
                    switch (a)
                    {
                        case 1:
                            string title = getString("Title           : ");
                            task.title = title;
                            task.display();
                            Console.WriteLine("\nTitle updated successfully.\n");
                            break;
                        case 2:
                            string description = getString("Description     : ");
                            task.description = description;
                            task.display();
                            Console.WriteLine("\nDescription updated successfully.\n");
                            break;
                        case 3:
                            DateTime userDateTime;
                            bool t = true;
                            while (t == true)
                            {
                                Console.Write("DATE (DD-MM-YYYY) : ");
                                if (DateTime.TryParse(Console.ReadLine(), out userDateTime)) { Console.WriteLine("\nDuedate updated successfully.\n"); task.duedate = userDateTime; t = false; }
                                else Console.WriteLine("You have entered an incorrect value.");
                            }
                            task.display();
                            Console.WriteLine("\nDue date updated successfully.\n");
                            break;

                        case 4:
                            MarkTaskasCompleted(task.id);
                            Console.WriteLine("\nCompletion status updated successfully.\n");
                            break;
                        case 5: temp = false; break;
                    }
                    
                }
            }
        }

        public void deletetask(int id)
        {
            
            TaskItem task = (from item in ToDolist
                             where item.id == id
                        select item).ToArray()[0];



            task.display();
            string s = getString("Do you want to delete permanently ? (Y/N) ");

            if (s=="Y"|| s=="y")
            {
                if (task.isCompleted == false)
                {
                    if (task.priority != 0)
                    {
                        var tasksprioritylesserthancurrenttask = from item in ToDolist
                                                                 where item.priority > task.priority
                                                                 select item;

                        foreach (var item1 in tasksprioritylesserthancurrenttask) --item1.priority;
                        TaskItem.pcount = TaskItem.pcount - 1;
                        

                    }
                }
                ToDolist.Remove(task);
                Console.WriteLine("\nTask deleted successfully.");
                
            }
            

        }

        public void saveTasks()
        {
            //string json = File.ReadAllText("todolist.json");


            string path = @"C:\Users\ManavBhatia\OneDrive - Kongsberg Digital AS\Documents\C##\TaskPadApp\todolist.json";

            if (ToDolist.Count > 0)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(ToDolist));
                Console.WriteLine("Tasks saved successfully");
            }
            else
            {
                Console.WriteLine(" dekhte hai ");
            }
            //if (File.Exists(json))
            //{
            //    if (ToDolist == null) { var templist = JsonConvert.DeserializeObject<List<TaskItem>>(json); ToDolist = templist; }
            //    else
            //    {
            //        string jsonText = File.ReadAllText(json);
            //        List<TaskItem> templist = JsonConvert.DeserializeObject<List<TaskItem>>(jsonText);
            //        if(templist.Count > 0) ToDolist.AddRange(templist);
            //        File.WriteAllText("todolist.json", JsonConvert.SerializeObject(ToDolist));
            //    }

            //}
            //else
            //{
            //    File.Create(json);
            //    if (ToDolist == null) { Console.WriteLine("No task can be saved."); return; }
            //    else File.WriteAllText("todolist.json", JsonConvert.SerializeObject(ToDolist));
            //}

        }

        public void loadTasks()
        {
            string path = @"C:\Users\ManavBhatia\OneDrive - Kongsberg Digital AS\Documents\C##\TaskPadApp\todolist.json";
            //string json = File.ReadAllText("todolist.json");
            string jsontext = File.ReadAllText(path);



            if (jsontext.Length == 0) { Console.WriteLine("No task present till now."); return; }

            ToDolist = JsonConvert.DeserializeObject<List<TaskItem>>(jsontext);
            


            //if (json.Length == 0) { Console.WriteLine("Empty file."); return; }
            //var templist = JsonConvert.DeserializeObject<List<TaskItem>>(json);
            //ToDolist = templist;
            //foreach (var item in templist)
            //    {
            //        Console.WriteLine($"id                  : {item.id}");
            //        Console.WriteLine($"Title               : {item.title}");
            //        Console.WriteLine($"Description         : {item.description}");
            //        if (item.priority != 0) Console.WriteLine($"Priority            : {item.priority}");
            //        if (item.duedate != null) Console.WriteLine($"Due date             : {item.duedate}");
            //        Console.WriteLine($"Completion Status   : {item.isCompleted}\n");
            //    }

        }

        public void addPriority(int id)
        {
            
            var itask = (from item in ToDolist
                         where item.id == id
                                       select item);
            foreach (var task in itask)
            {

                if (task.isCompleted == false)
                {
                    if (task.priority == 0) task.priority = ++TaskItem.pcount;
                    else Console.WriteLine("Task already has priority.");
                }
                else Console.WriteLine("Task already completed");
            }
        }
        







    }
}
