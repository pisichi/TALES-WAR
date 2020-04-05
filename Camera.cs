using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
  class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(GameObject target)
        {
            var position = Matrix.CreateTranslation(
              -target.Position.X - (target.Rectangle.Width / 2),
              -target.Position.Y - (target.Rectangle.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                Singleton.SCREENWIDTH / 2,
                Singleton.SCREENHEIGHT / 2,
                0);

            Transform = position * offset;
        }
    }
}
