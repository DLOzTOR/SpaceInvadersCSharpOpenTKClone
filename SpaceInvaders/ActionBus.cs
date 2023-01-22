using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace SpaceInvaders
{
    class ActionBus
    {
        public static Action OnKeyDDown;
        public static Action OnKeyADown;
        public static Action<Vector2> SpawnBullet;
        public static Action<Entity> OnDestroy;
    }
}
