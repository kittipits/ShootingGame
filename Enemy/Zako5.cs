using DxLibDLL;
using MyLib;
using System;

namespace Shooting
{
    public class Zako5 : Enemy //Scarecrow
    {
        int counter = 0;

        float theta;
        float radius;
        const int VisibleRadius = 32;

        public Zako5(Game game, float x, float y) : base(game, x, y) 
        { 
            life = 15;
            theta = 0;
            radius = 6;
            counter = 0;
        }

        public override void Update()
        {
            //∞の字
            theta += 0.05f;
            x = x + (float)(Math.Sin(theta / 2) * radius) - 0.8f;
            y = y + (float)(Math.Cos(theta) * radius / 2);

            counter++;
            //コウモリ(Zako0)を投げる
            if (counter % 60 == 0)
            {
                float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);
                game.destructibleBullets.Add(new DestructibleBatBullet(game, x + 32, y, angle, 10f));
            }

            if (x - VisibleRadius < 0 ||
                y + VisibleRadius < 0 ||
                y - VisibleRadius > Screen.Height)
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako5);
        }

        public override void DropItem()
        {
            game.items.Add(new ManaUpItem(game, x, y));
        }

        public override void ScoreUp()
        {
            game.score += 25;
        }
    }
}
