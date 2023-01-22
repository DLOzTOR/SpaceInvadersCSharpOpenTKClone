using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;


namespace SpaceInvaders
{
    class Box
    {
        public Vector2[] points;//p1 min x min y p2 minx maxy p3 maxx maxy p4 maxx miny
        public Color4 color;

        public Box(Vector2[] points, Color4 color)
        {
            this.points = points;
            this.color = color;
        }
        public Box(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Color4 color)
        {
            this.points = new Vector2[] { p1, p2, p3, p4 };
            this.color = color;
        }
    }
}
