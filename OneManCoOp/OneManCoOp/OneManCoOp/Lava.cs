using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OneManCoOp
{
    class Lava : GameObject
    {
        public Lava(Vector2 position2)
        {
            Position = position2;
            Sprite = new Sprite(TextureManager.door, Position, new Vector2(32, 32), 4, new Point(32, 32), 0.2f);
        }
        public override void Update()
        {
            if(Game1.player.Hitbox.Intersects(Hitbox))
            {

            }
        }
    }
}
