using Microsoft.Xna.Framework;
using System;

namespace Final_Assignment
{
  class Camera
    {

        public Matrix Transform { get; private set; }

        public void Follow(GameObject target)
        {
            var position = Matrix.CreateTranslation(
              -target.Position.X - (target.Rectangle.Width / 2),
              0,
              0);

            var offset = Matrix.CreateTranslation(
                Singleton.SCREENWIDTH / 2,
               0,
                0);

            Transform = position * offset;
        }






    }
}
