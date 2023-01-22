using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    internal class Ship : Entity
    {
        float moveSpeed = 10;
        float moveDir = 0;
        float bulletTimer = 0;
        public Ship(Box box, Vector2 transform,  float moveSpeed) : base(box, transform, EntityType.Ship)
        {
        }
        public override void Start()
        {
            base.Start();
            ActionBus.OnKeyADown += MoveLeft;
            ActionBus.OnKeyDDown += MoveRight;
        }
        public override void Update()
        {
            base.Update();
            Transform.X += moveDir * moveSpeed * 0.0167f;
            moveDir = 0;
            bulletTimer += 0.0167f;
            if(bulletTimer > 1)
            {
                bulletTimer = 0;
                ActionBus.SpawnBullet?.Invoke(Transform + new Vector2(0, 1.1f));
            }
        }
        void MoveLeft()
        {
            moveDir = -1;
        }
        void MoveRight()
        {
            moveDir = 1;
        }
    }
}
