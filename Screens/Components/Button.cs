using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Final_Assignment
{
  

    class Button
    {
        Vector2 _position;
        Rectangle _rectangle;
        SpriteFont _font;
        String _label;
        Texture2D _texture;

        Color _color = new Color(0, 0, 0, 255);

        public Vector2 size;

        public Button()
        {

        }

        bool down;
        public bool isClick;

        public void update(MouseState _currentmouse, MouseState _previousmouse)
        {
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(_currentmouse.X, _currentmouse.Y, 1, 1);

            if (mouseRectangle.Intersects(_rectangle))
            {
                if (_color.A == 255) down = false;
                if (_color.A == 0) down = true;

                if (down) _color.A += 5; else _color.A -= 5;

                if (_currentmouse.LeftButton == ButtonState.Pressed && _previousmouse.LeftButton == ButtonState.Released
                    ) isClick = true;
            }
            else if (_color.A < 255)
            {
                _color.A += 5;
                isClick = false;
            }
        }

        public void set(Vector2 newPosition, String label)
        {
            _position = newPosition;
            _label = label;
            size = _font.MeasureString("" + _label) * 1.5f;
            size.Y -= 20;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "" + _label, _position, Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "" + _label, _position, _color, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
        }
    }
}