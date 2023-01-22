using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace SpaceInvaders
{
    class Window : GameWindow
    {
        static float borderSize = 0.5f;
        List<Entity> Entities = new List<Entity>() { 
        new Entity(new Box(new Vector2(-10, 10 - borderSize),new Vector2(-10, 10), new Vector2(10, 10), new Vector2(10, 10 - borderSize),  Color4.Red), Vector2.Zero, EntityType.Border),
        new Entity(new Box(new Vector2(-10, -10),new Vector2(-10, -10 + borderSize),new Vector2(10, -10 + borderSize), new Vector2(10, -10),   Color4.Red), Vector2.Zero, EntityType.Border),
        new Entity(new Box( new Vector2(-10, -10),new Vector2(-10, 10), new Vector2(-10 + borderSize, 10), new Vector2(-10 + borderSize, -10), Color4.Red), Vector2.Zero, EntityType.Border),
        new Entity(new Box(new Vector2(10 - borderSize, -10), new Vector2(10 - borderSize, 10),new Vector2(10, 10), new Vector2(10, -10),  Color4.Red), Vector2.Zero, EntityType.Border),
        //new Ship(new Box(new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), Color4.Blue), new Vector2(0, -8),10),
        new Ship(new Box(new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), Color4.Blue), new Vector2(0.5f, -7.5f),10),
        };
        List<Entity> SpawnOrder = new List<Entity>();
        List<Entity> DestroyOrder = new List<Entity>();
        void CollisionDetector()
        {
            int curEt = 0;
            foreach (Entity entity in Entities)
            {
                for(int i = curEt; i < Entities.Count; i++)
                {
                    if(entity == Entities[i])
                    {
                        continue;
                    }
                    if (entity.box.points[0].X + entity.Transform.X < Entities[i].box.points[2].X + Entities[i].Transform.X && 
                        entity.box.points[2].X + entity.Transform.X > Entities[i].box.points[0].X + Entities[i].Transform.X && 
                        entity.box.points[0].Y + entity.Transform.Y < Entities[i].box.points[2].Y + Entities[i].Transform.Y && 
                        entity.box.points[2].Y + entity.Transform.Y > Entities[i].box.points[0].Y + Entities[i].Transform.Y 
                        ) {
                        if((entity.Type == EntityType.Bullet && Entities[i].Type == EntityType.Invader) || (entity.Type == EntityType.Invader && Entities[i].Type == EntityType.Bullet))
                        {
                            DestroyOrder.Add(entity);
                            DestroyOrder.Add(Entities[i]);
                        }
                    }
                }
                curEt++;
            }
        }
        public Window(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title, Flags = ContextFlags.Default, Profile = ContextProfile.Compatability })
        {
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.D)) ActionBus.OnKeyDDown?.Invoke();
            if (input.IsKeyDown(Keys.A)) ActionBus.OnKeyADown?.Invoke();

        }
        void SpawnBullet(Vector2 transform)
        {
            SpawnOrder.Add(new Bullet(transform));
        }
        void DestroyEntity(Entity entity)
        {
            DestroyOrder.Add(entity);
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            ActionBus.SpawnBullet += SpawnBullet;
            ActionBus.OnDestroy += DestroyEntity;
            for(int x =  -7; x < 8; x+=2)
            {
                for(int y = 4; y < 9l; y += 2)
                {
                    Entities.Add(new Invader(new Vector2(x,y)));
                }
            }
            foreach (Entity entity in Entities)
            {
                entity.Start();
            }
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }
        void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.PushMatrix();
            GL.Scale(0.1f, 0.1f, 0.1f);
            foreach (Entity entity in Entities)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(entity.box.color);
                for (int i = 0; i < 4; i++)
                {
                    GL.Vertex2(entity.box.points[i] + entity.Transform);
                }
                GL.End();
            }
            GL.Flush();
            GL.PopMatrix();
            SwapBuffers();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
            Entities.AddRange(SpawnOrder);
            SpawnOrder = new List<Entity>();
            CollisionDetector();
            foreach (Entity entity in DestroyOrder)
            {
                Entities.Remove(entity);
            }
            DestroyOrder = new List<Entity>();
            base.OnRenderFrame(e);
            Render();
        }
    }
}
