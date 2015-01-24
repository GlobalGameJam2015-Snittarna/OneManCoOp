using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp 
{
    class Corpse : GameObject
    {

        int tag;
        public Corpse(int newTag)
        {
            tag = newTag;
            Sprite = new Sprite(TextureManager.player, Game1.SPAWNPOSITION, new Vector2(32, 64), 1, new Point(32, 64), 0);
        }

        public override void Update()
        {
            Position += new Vector2(10,10);
        }
    }
}
