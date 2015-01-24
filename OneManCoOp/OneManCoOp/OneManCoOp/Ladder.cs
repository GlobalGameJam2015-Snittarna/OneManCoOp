using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Ladder : GameObject
    {
        public Ladder(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.ladder, position, new Vector2(32));
            Sprite.Origin = Vector2.Zero;
        }
    }
}
