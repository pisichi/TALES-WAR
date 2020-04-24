using Microsoft.Xna.Framework;
using System;

namespace Final_Assignment
{
  class Camera
    {

        public Matrix Transform { get; private set; }
        public void Follow(GameObject target)
        {
            //Start check camera collision
            target.CameraPosition = target.Position;
            if (target.Position.X > 4000 - Singleton.SCREENWIDTH / 2 - target.Rectangle.Width)
                target.CameraPosition.X = 4000 - Singleton.SCREENWIDTH / 2 - target.Rectangle.Width;

            if (target.Position.X < Singleton.SCREENWIDTH / 2)
                target.CameraPosition.X = Singleton.SCREENWIDTH / 2;

            if (target.Position.Y < Singleton.SCREENHEIGHT / 2)
                target.CameraPosition.Y = Singleton.SCREENHEIGHT / 2;

            if (target.Position.Y > 1000 - Singleton.SCREENHEIGHT / 2 - target.Rectangle.Width)
                target.CameraPosition.Y = 1000 - Singleton.SCREENHEIGHT / 2 - target.Rectangle.Width;
            //End check camera collision
            var position = Matrix.CreateTranslation(
              -target.CameraPosition.X - (target.Rectangle.Width / 2),
              -target.CameraPosition.Y - (target.Rectangle.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                Singleton.SCREENWIDTH / 2,
               Singleton.SCREENHEIGHT / 2,
               0);

            Transform = position * offset;
            //Console.WriteLine("follow" + target.Position.X + " " + target.CameraPosition.Y);
        }
    }
}
