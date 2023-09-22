namespace Shooting
{
    public abstract class Item
    {
        public float x;
        public float y;
        public float collisionRadius = 32;

        public bool isDead = false;
        protected Game game;

        public Item(Game game, float x, float y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
        }

        public abstract void Update();

        public abstract void Draw();
        
        public virtual void OnCollisionPlayer(Player player)
        {
            isDead = true;
        }
    }
}
