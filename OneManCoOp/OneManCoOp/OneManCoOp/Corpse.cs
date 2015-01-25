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
            Sprite.Alpha = 0.5f;
        }

        public void Update(byte frame)
        {
            if (Game1.GlobalTimer > 0)
            {
                if (Game1.corpsesX[tag, Game1.GlobalTimer] == 0)
                {
                    Game1.corpsesX[tag, Game1.GlobalTimer] = Game1.corpsesX[tag, Game1.GlobalTimer - 1];
                }
                if (Game1.corpsesY[tag, Game1.GlobalTimer] == 0)
                {
                    Game1.corpsesY[tag, Game1.GlobalTimer] = Game1.corpsesY[tag, Game1.GlobalTimer - 1];
                }
            }
            
            Position = new Vector2(Game1.corpsesX[tag, Game1.GlobalTimer], Game1.corpsesY[tag, Game1.GlobalTimer]);
            Sprite.Frame = frame;
        } 
    }
}
