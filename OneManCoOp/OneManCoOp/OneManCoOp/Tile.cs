using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Tile
    {
        public const byte SIZE = 32;

        public Rectangle Hitbox { get; private set; }
        public Vector2 Position { get; private set; }

        public Tile(Vector2 position, byte type)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
