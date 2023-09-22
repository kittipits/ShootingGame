using DxLibDLL;
using MyLib;

namespace Shooting
{
    class Zako0 : Enemy //Bat
    {
        const int VisibleRadius = 16;

        int animationCounter = 0;
        int[] enemyMotion = new int[5] { 0, 1, 2, 3, 0 };
        int motionIndex = 0;

        public Zako0(Game game, float x, float y) : base(game, x, y)
        {
            life = 2;
            collisionRadius = 16;
        }

        public override void Update()
        {
            x -= 8;

            if (x - VisibleRadius < 0 ||
                y + VisibleRadius < 0 ||
                y - VisibleRadius > Screen.Height)
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            animationCounter++;
            motionIndex = motionIndex % 4;
            if (animationCounter % 10 == 0) motionIndex++;

            DX.DrawRotaGraphF(x, y, 1, 0f, Image.zako0[enemyMotion[motionIndex]]);
        }

        public override void ScoreUp()
        {
            game.score += 10;
        }
    }
}
