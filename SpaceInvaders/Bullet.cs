using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Bullet : Entity
    {
        static float bulletSpeed =10;
        public Bullet(Vector2 transform) : base(new Box(new Vector2(-0.2f, -0.2f), new Vector2(-0.2f, 0.2f), new Vector2(0.2f, 0.2f), new Vector2(0.2f, -0.2f), Color4.Cyan), transform, EntityType.Bullet)
        {
        }
        public override void Update()
        {
            base.Update();
            Transform.Y += bulletSpeed * 0.0167f;
            if (Transform.Y > 10) ActionBus.OnDestroy?.Invoke(this);
        }
    }
}
