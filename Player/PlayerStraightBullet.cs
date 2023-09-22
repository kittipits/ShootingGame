using DxLibDLL;
using System;

namespace Shooting
{
    public class PlayerStraightBullet : PlayerBullet
    {
        const float Speed = 20f;            //速度
        const int VisibleRadius = 16;       //画面を出たら、...

        //方向処理
        float vx;
        float vy;

        Game game;

        public PlayerStraightBullet(Game game, float x, float y, float angle) : base(x, y, angle)
        {
            this.x = x ;
            this.y = y ;
            this.angle = angle;
            this.game = game;

            vx = (float)Math.Cos(angle) * Speed;
            vy = (float)Math.Sin(angle) * Speed;
        }

        public override void Update()
        {
            x += vx;
            y += vy;

            //画面を出たら、消える
            if (x + VisibleRadius < 0 ||
                x - VisibleRadius > Screen.Width ||
                y + VisibleRadius < 0 ||
                y - VisibleRadius > Screen.Height)
            { 
                isDead = true;
            }
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, angle, Image.playerBullet);
        }

        //敵にぶつかったら、敵のライフを減らす
        public override void OnCollisionEnemy(Enemy enemy)
        {
            game.player.ultimateGuage++;
            isDead = true;
        }

        public override void OnCollisionDestructibleBullet(DestructibleBullet destructibleBullet)
        {
            isDead = true;
        }

    }
}
