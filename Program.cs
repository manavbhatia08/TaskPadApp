namespace TaskPadApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ToDoManager t = new ToDoManager();
            t.displayMenu();
            //Console.WriteLine("Hello, World!");
            //List<string> list = new List<string>{ "manav ", "jinil", "thakkar" };

            //var sort1 = from t in list
            //            orderby t
            //            select t ;
            //foreach ( var item in sort1 ) { Console.WriteLine(item); }

            //list.Sort((x, y) => x.CompareTo(y));
            //Console.WriteLine(list[0]);
            //Console.WriteLine(list[1]);
            //Console.WriteLine(list[2]);
        }
    }
}