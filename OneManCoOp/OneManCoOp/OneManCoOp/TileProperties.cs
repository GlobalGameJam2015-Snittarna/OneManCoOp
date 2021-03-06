﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class TileProperties
    {
        public byte TextureIndex { get; private set; }
        public byte Frames { get; private set; }
        public byte FramesPerFrame { get; private set; }
        public bool Solid { get; private set; }
        public bool ObstructsBullets { get; private set; }

        public bool IsAnimated { get { return Frames > 1; } }

        public TileProperties(byte textureIndex, bool solid, bool obstructsBullets, byte frames, byte animSpeed)
        {
            this.Frames = frames;
            this.FramesPerFrame = animSpeed;
            this.TextureIndex = textureIndex;
            this.Solid = solid;
            this.ObstructsBullets = obstructsBullets;
        }

        public TileProperties(byte textureIndex, bool solid)
            : this(textureIndex, solid, !solid, 1, 0)
        { }

        public TileProperties(byte textureIndex, bool walkable, bool obstructsBullets)
            : this(textureIndex, walkable, obstructsBullets, 1, 0)
        { }

        public void Animate(ref byte frame, ref byte counter)
        {
            counter += 1;
            if (counter >= FramesPerFrame)
            {
                counter = 0;
                frame++;
                if (frame >= Frames)
                {
                    frame = 0;
                }
            }
        }
    }
}
