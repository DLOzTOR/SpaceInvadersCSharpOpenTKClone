using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Invader : Entity
    {
        public Invader(Vector2 transform) : base(new Box(new Vector2(-0.8f, -0.5f), new Vector2(-0.8f, 0.5f), new Vector2(0.8f, 0.5f), new Vector2(0.8f, -0.5f), Color4.Yellow), transform, EntityType.Invader)
        {
            
        }
    }
}
