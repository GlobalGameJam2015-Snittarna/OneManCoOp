using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Player : GameObject
    {

        public Player(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.player, position, new Vector2(48));
        }
    }
}
