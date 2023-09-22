using DxLibDLL;
using System;

namespace Shooting
{
    internal class PlayerReflectBullet : PlayerBullet
    {
        const float Speed = 30f;            //速度
        const int VisibleRadius = 16;       //画面を出たら、...
        const int Size = 32;

        //方向処理
        float vx;
        float vy;

        public PlayerReflectBullet(float x, float y, float angle) : base(x, y, angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;

            vx = (float)Math.Cos(angle) * Speed;
            vy = (float)Math.Sin(angle) * Speed;
        }

        public override void Update()
        {
            x += vx;
            y += vy;

            //左、上、下、出たら反射
            if (x < 0 + (Size / 2)) //左
            {
                x = 0 + (Size / 2);
                vx = -vx;
            }
            if (y < 0 + (Size / 2)) //上
            {
                y = 0 + (Size / 2);
                vy = -vy;
            }
            if (y > Screen.Height - (Size / 2)) //下
            {
                y = Screen.Height - (Size / 2);
                vy = -vy;
            }

            //画面を出たら消える
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
            isDead = true;
        }

        public override void OnCollisionDestructibleBullet(DestructibleBullet destructibleBullet)
        {
            isDead = true;
        }

    }
}
