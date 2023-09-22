using DxLibDLL;
using MyLib;

namespace Shooting
{
    class Zako3 : Enemy //Scorpion
    {
        int counter = 0;
        private int state;
        private int moveCount;
        const int VisibleRadius = 32;

        public Zako3 (Game game, float x, float y) : base (game, x, y)
        {
            life = 15;
            state = 0;
            moveCount = 0;
        }

        public override void Update()
        {
            moveCount++;
            counter++;

            //四角運動（動いたり、止まったりする）
            if (moveCount >= 30)
                x -= 2f;

            if (state == 0)
            {
                if (moveCount >= 30)
                    x -= 1;
                
                if (moveCount == 90) 
                { 
                    state = 1;
                    moveCount = 0;
                }
            }
            else if (state == 1) 
            {
                if (moveCount >= 30)
                    y -= 2;

                if (moveCount == 90)
                {
                    state = 2;
                    moveCount = 0;
                }
            }
            else if (state == 2)
            {
                if (moveCount >= 30)
                    x += 4;

                if (moveCount == 90)
                {
                    state = 3;
                    moveCount = 0;
                }
            }
            else if (state == 3)
            {
                if (moveCount >= 30)
                    y += 2;

                if (moveCount == 90)
                {
                    state = 0;
                    moveCount = 0;
                }
            }
            
            //止まったら、連射弾
            if (moveCount < 30)
            {
                if (counter % 30 == 0)
                {
                    float angle = MyMath.PointToPointAngle(x, y, game.player.x, game.player.y);

                    for (int i = 0; i < 8; i++)
                    {
                        float speed = 6f + 0.5f * i;
                        game.enemyBullets.Add(new EnemyStraightBullet(x, y, angle, speed));
                    }
                }
            }

            //動いたら、自己の周りにランダム
            else if (moveCount >= 30)
            {
                if (counter % 3 == 0)
                    game.enemyBullets.Add(new EnemyStraightBullet(x, y, (180f - MyRandom.PlusMinus(180)) * MyMath.Deg2Rad, 4f));
            }

            if (x - VisibleRadius < 0 ||
                y + VisibleRadius < 0 ||
                y - VisibleRadius > Screen.Height)
            {
                isDead = true;
            }
        }

        public override void DropItem()
        {
            game.enemyTraps.Add(new EnemyTrapPoison( x, y));
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0, Image.zako3);
        }

        public override void ScoreUp()
        {
            game.score += 25;
        }
    }
}
