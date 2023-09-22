namespace Shooting
{
    public abstract class PlayerBullet
    {
        //座標
        public float x;
        public float y;

        public float angle;　　　　　　　　 //向き
        public bool isDead = false;         //画面を出たら、消える
        public float collisionRadius = 16f; //衝突半径

        public PlayerBullet(float x, float y, float angle) 
        {
            this.x = x ;
            this.y = y ;
            this.angle = angle;
        }

        public abstract void Update();

        public abstract void Draw();

        //敵にぶつかったら、敵のライフを減らす
        public virtual void OnCollisionEnemy(Enemy enemy)
        {
            isDead = true;
        }

        public virtual void OnCollisionDestructibleBullet(DestructibleBullet destructibleBullet)
        {
            isDead = true;
        }

    }
}
