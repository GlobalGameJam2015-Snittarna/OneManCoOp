using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManCoOp
{
    class Player : GameObject
    {
        const float JUMP_SPEED = 15;
        const float FRICTION_GROUND = .95f;
        const float FRICTION_AIR = .99f;
        const float ACC_GROUND = 1;
        const float ACC_AIR = .1f;

        public Player(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.player, position, new Vector2(32, 64), 4, new Point(32, 64), 10 / 60f);
        }

        public override void Update()
        {
            Vector2 acceleration = new Vector2(0);
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) acceleration.X -= 1;
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) acceleration.X += 1;
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)) acceleration.Y -= 1;
            if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) acceleration.Y += 1;
            if (Input.KeyWasJustPressed(Microsoft.Xna.Framework.Input.Keys.Space) && CollidedOnY) Velocity -= new Vector2(0, JUMP_SPEED);
            if (acceleration != Vector2.Zero) acceleration.Normalize();

            Velocity *= new Vector2((CollidedOnY) ? FRICTION_GROUND : FRICTION_AIR, 1);

            Velocity += acceleration * (CollidedOnY ? ACC_GROUND : ACC_AIR);

            Velocity += new Vector2(0, Game1.GRAVITY);

            Move(true);
        }
    }
}
