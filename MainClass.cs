using MiniRPG.Characters;
using MiniRPG.Characters.Enemies;
using MiniRPG.GameLunch;
using MiniRPG.SaveSystem;

namespace MiniRPG
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();

            //Player player = new Player("MM");
            //player.TakeDamage(25);

            //SaveManager saveManager = new SaveManager();
            //saveManager.Save(player);

            //Player player = saveManager.Load();

            //player.PrintStats();
        }
    }
}
