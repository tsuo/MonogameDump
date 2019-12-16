using System;

using Microsoft.Xna.Framework;

namespace projVN
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (Game game = new MainGame())
                game.Run();
        }
    }

}
