using System;
using DxLibDLL;


namespace Shooting
{
    public class DestructibleBatBullet : DestructibleBullet
    {
        const float VisibleRadius = 16f;

        int animationCounter = 0;
        int[] enemyMotion = new int[4] { 0, 1, 2, 3 };
        int motionIndex = 0;

        public DestructibleBatBullet(Game game, float x, float y, float angle, float speed)
            : base (game, x, y)
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
            animationCounter++;
            if (motionIndex > 2) motionIndex = 0;
            if (animationCounter % 10 == 0) motionIndex++;

            DX.DrawRotaGraphF(x, y, 1, 0f, Image.zako0[enemyMotion[motionIndex]]);
        }

    }
}
