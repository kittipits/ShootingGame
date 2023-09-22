using DxLibDLL;
using MyLib;
using System;
namespace Shooting
{
    public class EnemyHomingBullet : EnemyBullet
    {
        const float VisibleRadius = 8f;
        int time;
        Game game;

        public EnemyHomingBullet(Game game, float x, float y, int time)
            : base(x, y)
        {
            this.game = game;
            this.time = time;
        }
        public override void Update()
        {
            time--;

            float angleToPlayer = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
            float speed = 3f;
            x += (float)Math.Cos(angleToPlayer) * speed;
            y += (float)Math.Sin(angleToPlayer) * speed;

            if (time <= 0)
            {
                isDead = true;
            }

            if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
                x + VisibleRadius < 0 || x - VisibleRadius > Screen.Width)
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            if (time > 60 || time / 5 % 2 == 0)
            {
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.enemyBullet16);
            }
            else
            {
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 128);
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.enemyBullet16);
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 128);
            }
        }
    }
}
