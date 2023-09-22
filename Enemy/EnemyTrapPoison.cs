using DxLibDLL;

namespace Shooting
{
    class EnemyTrapPoison : EnemyTrap
    {
        const float VisibleRadius = 32f;
        public EnemyTrapPoison(float x, float y)
            : base(x, y)
        {
        }
        public override void Update()
        {
            x -= 1f;
            
            if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
                x + VisibleRadius < 0)
            {
                isDead = true;
            }
        }
        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0f, Image.poison);
        }
    }
}
