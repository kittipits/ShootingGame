using System;
using DxLibDLL;
using MyLib;

namespace Shooting
{
    public class LifeUpItem : Item
    {
        const float VisibleRadius = 8f;

        public LifeUpItem(Game game, float x, float y)
            : base(game, x, y)
        { 
        }

        public override void Update()
        {
            x -= 0.5f;

            if (y + VisibleRadius < 0 || y - VisibleRadius > Screen.Height ||
                x + VisibleRadius < 0 )
            {
                isDead = true;
            }
        }

        public override void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0f, Image.itemLifeUp);
        }

        public override void OnCollisionPlayer(Player player)
        {
            player.RecoverHP();
            isDead = true;
        }

    }
}
