using DxLibDLL;

namespace Shooting
{
    public static class Image
    {
        public static int[] player = new int[3];
        public static int playerBullet;
        public static int[] zako0 = new int[4];
        public static int zako1;
        public static int zako2;
        public static int zako3;
        public static int zako4;
        public static int zako5;
        public static int zako6;
        public static int zako7;
        public static int enemyBullet16;
        public static int[] explosion = new int[16];
        public static int[] boss1 = new int[3];
        public static int boss2;
        public static int[] boss3 = new int[3];
        public static int itemLifeUp;
        public static int itemManaUp;
        public static int poison;
        public static int spike;
        public static int blood;
        public static int background;
        public static int guageHP;
        public static int guageMP;
        public static int guageUltimate;
        public static int guageBoss;
        public static int black;
        public static int winResult;
        public static int loseResult;
        public static int title;
        public static int howtoplay0;
        public static int howtoplay1;
        public static int howtoplay2;
        public static int story0;
        public static int story1;

        public static void Load() 
        {
            DX.LoadDivGraph("Image/player.png", 3, 3, 1, 64, 64, player);
            playerBullet    = DX.LoadGraph("Image/player_bullet.png");
            DX.LoadDivGraph("Image/zako0.png", 4, 4, 1, 32, 32, zako0);
            zako1           = DX.LoadGraph("Image/zako1.png");
            zako2           = DX.LoadGraph("Image/zako2.png");
            zako3           = DX.LoadGraph("Image/zako3.png");
            zako4           = DX.LoadGraph("Image/zako4.png");
            zako5           = DX.LoadGraph("Image/zako5.png");
            zako6           = DX.LoadGraph("Image/zako6.png");
            zako7           = DX.LoadGraph("Image/zako7.png");
            enemyBullet16   = DX.LoadGraph("Image/enemy_bullet_16.png");
            itemLifeUp      = DX.LoadGraph("Image/itemLifeUp.png");
            itemManaUp      = DX.LoadGraph("Image/itemManaUp.png");
            poison          = DX.LoadGraph("Image/poison.png");
            spike           = DX.LoadGraph("Image/spike.png");
            blood           = DX.LoadGraph("Image/blood.png");
            background      = DX.LoadGraph("Image/background.png");
            guageHP         = DX.LoadGraph("Image/guage300.png");
            guageMP         = DX.LoadGraph("Image/guage180.png");
            guageUltimate   = DX.LoadGraph("Image/guage90.png");
            guageBoss       = DX.LoadGraph("Image/guageBoss.png");
            black           = DX.LoadGraph("Image/black.png");
            winResult       = DX.LoadGraph("Image/winResult.png");
            loseResult      = DX.LoadGraph("Image/loseResult.png");
            title           = DX.LoadGraph("Image/title.png");
            howtoplay0      = DX.LoadGraph("Image/howtoplay0.png");
            howtoplay1      = DX.LoadGraph("Image/howtoplay1.png");
            howtoplay2      = DX.LoadGraph("Image/howtoplay2.png");
            story0          = DX.LoadGraph("Image/story0.png");
            story1          = DX.LoadGraph("Image/story1.png");


            // 爆発エフェクトの読み込み。
            // 横に8枚、縦に2枚の計16枚並んだ絵を分割して読み込む。
            // ハンドル（管理番号）は分割された絵ごとに振られ、配列に格納される。
            DX.LoadDivGraph("Image/explosion.png", 16, 8, 2, 64, 64, explosion);

            DX.LoadDivGraph("Image/boss1.png", 3, 3, 1, 128, 128, boss1);
            boss2 = DX.LoadGraph("Image/boss2.png");
            DX.LoadDivGraph("Image/boss3.png", 3, 3, 1, 128, 128, boss3);
        }
    }
}
