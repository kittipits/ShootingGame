using System;
using DxLibDLL;

namespace Shooting
{
    public abstract class EnemyBullet
    {
        //const float VisibleRadius = 8f;

        public float x;
        public float y;
        public bool isDead = false;
        public float collisionRadius = 8f;

        protected float vx = 0;
        protected float vy = 0;

        public EnemyBullet(float x, float y)
        { 
            this.x = x;
            this.y = y;

            //vx = (float)Math.Cos(angle) * speed;
            //vy = (float)Math.Sin(angle) * speed;
        }

        public abstract void Update();

        public abstract void Draw();

        //public virtual void Update() 
        //{
        //    x += vx;
        //    y += vy;

        //    if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
        //        x + VisibleRadius < 0 || x - VisibleRadius > Screen.Width)
        //    { 
        //        isDead = true;
        //    }
        //}

        //public virtual void Draw()
        //{
        //    DX.DrawRotaGraphF(x, y, 1, 0f, Image.enemyBullet16);
        //}
    }
}
