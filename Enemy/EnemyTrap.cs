using DxLibDLL;
using System;

namespace Shooting
{
    public abstract class EnemyTrap
    {
        public float x;
        public float y;
        public bool isDead = false;
        public float collisionRadius = 32f;

        protected float vx = 0;
        protected float vy = 0;

        public EnemyTrap(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public abstract void Update();

        public abstract void Draw();

    }
}
