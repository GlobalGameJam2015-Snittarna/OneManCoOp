using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace OneManCoOp
{
    class SoundManager
    {
        public static SoundEffect
            buttonPress;
        public static void Load(ContentManager content)
        {
            buttonPress = content.Load<SoundEffect>("buttonPress");
        }
    }
}
