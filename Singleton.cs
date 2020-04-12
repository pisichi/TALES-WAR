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


        public int Score;
        public int Charge;

        public string CurrentHero;
        public string CurrentStage;

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
