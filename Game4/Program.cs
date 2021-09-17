using System;

namespace Game4
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Pong())
                game.Run();
        }
    }
}
