using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace SpaceInvaders
{
    internal class Entity
    {
        public Box box;
        public Vector2 Transform;
        public EntityType Type;

        public Entity(Box box, Vector2 transform, EntityType type)
        {
            this.box = box;
            Transform = transform;
            Type = type;
        }
        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        } 
    }
    enum EntityType
    {
        Ship,
        Invader,
        Bullet,
        Border,
    }
}
