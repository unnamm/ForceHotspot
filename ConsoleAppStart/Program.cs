namespace ConsoleAppStart
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World!");

            var hs = new Hotspot();
            var data = hs.Run("", "", 71).Result;

            Console.WriteLine("connected: " + data);
            Console.Read();
        }
    }
}
