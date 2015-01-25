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
            buttonPress,
            lava1,
            lava2;

        public static void Load(ContentManager content)
        {
            buttonPress = content.Load<SoundEffect>("buttonPress");

        }
    }
}
