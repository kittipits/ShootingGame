using System; 
using DxLibDLL;

namespace Shooting
{
    public class EnemyStraightBullet : EnemyBullet
    {
        const float VisibleRadius = 8f;

        public EnemyStraightBullet(float x, float y, float angle, float speed) 
            : base (x,y) 
        {
            vx = (float)Math.Cos(angle) * speed;
            vy = (float)Math.Sin(angle) * speed;
        }
        public override void Update()
        {
            x += vx;
            y += vy;

            if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
                x + VisibleRadius < 0 || x - VisibleRadius > Screen.Width)
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0f, Image.enemyBullet16);
        }
    }
}
