namespace Shooting
{
    public abstract class Enemy
    {
        public float x;
        public float y;
        public float collisionRadius = 32f;
        public bool isDead = false;

        //protected float moveTheta;
        //protected float moveRadius;

        protected Game game;
        protected float life = 1;

        public Enemy (Game game, float x, float y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            //moveTheta = 0;
            //moveRadius = 5;
        }

        public abstract void Update ();

        public abstract void Draw();

        public virtual void OnCollisionPlayerBullet(PlayerBullet playerBullet)
        {
            life -= 1;

            if (life <= 0) 
            {
                isDead = true;
                game.explosions.Add(new Explosion(x, y));
                DropItem();
                ScoreUp();
            }
        }

        public virtual void OnCollisionPlayer(Player player)
        { 
        }

        public virtual void DropItem() 
        {
        }

        public virtual void ScoreUp()
        { 
        }
    }
}
