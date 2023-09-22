using DxLibDLL;

namespace Shooting
{
    public class Blood
    {
        const float VisibleRadius = 32f;
        const int vanishTime = 180;
        int time;
        Game game;

        float x;
        float y;
        public bool isDead = false;

        public Blood(Game game, float x, float y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            time = 0;
        }

        public void Update()
        {
            time++;

            if (time > vanishTime)
            {
                game.enemyTraps.Add(new EnemyTrapSpike(x, y));
                isDead = true;
            }

            if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
                x + VisibleRadius < 0 || x - VisibleRadius > Screen.Width)
            {
                isDead = true;
            }
        }

        public void Draw()
        {
            if (time / 30 % 2 == 0)
            {
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 128);
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.blood);
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 128);
            }
            else 
            {
                DX.DrawRotaGraphF(x, y, 1, 0f, Image.blood);
            }

        }
    }
}
