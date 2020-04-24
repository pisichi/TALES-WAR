using Microsoft.Xna.Framework.Input;


namespace Final_Assignment
{
    class Singleton
    {

        public KeyboardState _currentkey;
        public KeyboardState _previouskey;
        public const int SCREENWIDTH = 1200;
        public const int SCREENHEIGHT = 800;
        public GameObject follow;


        public string CurrentHero = "thor";
        public int CurrentStage = 1;


        public int level_s1 = 1;
        public int level_s2 = 1;
        public int level_s3 = 1;


        public enum GameState
        {
            GamePlaying,
            GamePaused,
            GameLose,
            GameWin
        }

        public GameState CurrentGameState;

        public enum TurnState
        {
            skill,
            angle,
            force,
            shoot,
            enemy
        }

        public TurnState CurrentTurnState;

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
