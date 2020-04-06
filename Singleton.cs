using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
    class Singleton
    {

        public const int SCREENWIDTH = 1200;
        public const int SCREENHEIGHT = 800;

        public int Score;
        public int Charge;

        public enum GameState
        {
            GameMenu,
            GamePlaying,
            GamePaused,
            GameLose,
            GameWin,
            StartNewLife
        }

        public Random Random;

        public KeyboardState PreviousKey, CurrentKey;
        public GameState CurrentGameState;

        private static Singleton instance;
        public float MasterBGMVolume;
        public float MasterSFXVolume;

        //public int Score;
        public int Life;
        public int Level;
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
