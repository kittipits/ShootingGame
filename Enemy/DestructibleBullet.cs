namespace Shooting
{
    public abstract class DestructibleBullet
    {
        public float x;
        public float y;
        public bool isDead = false;
        public float collisionRadius = 8f;

        protected float vx = 0;
        protected float vy = 0;

        protected Game game;
        protected float life = 3;

        public DestructibleBullet( Game game, float x, float y)
        {
            this.x = x;
            this.y = y;
            this.game = game;
        }

        public abstract void Update();

        public abstract void Draw();

        public virtual void OnCollisionPlayerBullet(PlayerBullet playerBullet)
        {
            life -= 1;

            if (life <= 0)
            {
                isDead = true;
                game.explosions.Add(new Explosion(x, y));
            }
        }
    }
}
