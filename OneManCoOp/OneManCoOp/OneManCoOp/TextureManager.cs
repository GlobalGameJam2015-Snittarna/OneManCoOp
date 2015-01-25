using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class TextureManager
    {
        public static Texture2D 
            map, 
            tiles,
            player,
            door,
            button,
            ladder,
            lava,
            particle;

        public static void Load(ContentManager content)
        {
            map = content.Load<Texture2D>("map");
            tiles = content.Load<Texture2D>("basicTileSheet");
            player = content.Load<Texture2D>("player");
            door = content.Load<Texture2D>("trapdoor");
            button = content.Load<Texture2D>("buttonAnimation");
            ladder = content.Load<Texture2D>("ladder");
            lava = content.Load<Texture2D>("lavaAnimation");
            particle = content.Load<Texture2D>("particle");
        }
    }
}
