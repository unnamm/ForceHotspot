using Sequence;

namespace StartConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var hs = new Hotspot();
            var data = hs.Run("", "", 71).Result;

            Console.WriteLine("connected: " + data);
        }
    }
}
