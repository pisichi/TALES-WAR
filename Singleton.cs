using Microsoft.Xna.Framework.Input;


namespace Final_Assignment
{
    class Singleton
    {

        public KeyboardState _currentkey;
        public KeyboardState _previouskey;

        public const int SCREENWIDTH = 1200;
        public const int SCREENHEIGHT = 800;

        public int Score;
        public int Charge;

        public string CurrentHero;
        public string CurrentStage;

        public enum GameState
        {
            GameIntro,
            GameMenu,
            GamePlaying,
            GamePaused,
            GameLose,
            GameWin
        }

        public GameState CurrentGameState;

        private static Singleton instance;

        private Singleton()
        {

        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }

}
