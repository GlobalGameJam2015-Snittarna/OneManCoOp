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
            tiles;

        public static void Load(ContentManager content)
        {
            map = content.Load<Texture2D>("map");
        }
    }
}
