using DxLibDLL;

namespace Shooting
{
    public static class Sound
    {
        public static string bgmTitle = "Sound/bgmTitle.wav";
        public static string bgmBoss = "Sound/bgmBoss.wav";
        public static string bgmWin = "Sound/bgmWin.wav";
        public static string bgmLose = "Sound/bgmLose.wav";

        public static int seStart;
        public static int seShoot1;
        public static int seShoot2;
        public static int seExplosion;
        public static int seSelect;


        public static void Load()
        {
            seStart     = DX.LoadSoundMem("Sound/seStart.wav");
            seShoot1    = DX.LoadSoundMem("Sound/seShoot1.wav");
            seShoot2    = DX.LoadSoundMem("Sound/seShoot2.wav");
            seExplosion = DX.LoadSoundMem("Sound/seExplosion.wav");
            seSelect    = DX.LoadSoundMem("Sound/seSelect.wav");
        }

        //ー回
        public static void Play(int handle)
        {
            DX.PlaySoundMem(handle, DX.DX_PLAYTYPE_BACK);
        }

        //BGMを流す
        public static void PlayMusic(string fileName)
        {
            DX.PlayMusic(fileName, DX.DX_PLAYTYPE_LOOP);
        }
        //DX.StopMusic();   //BGMを停止させる。

        //ループ開始
        public static void PlayLoop(int handle)
        {
            DX.PlaySoundMem(handle, DX.DX_PLAYTYPE_LOOP);
        }

        //ループ停止
        public static void StopLoop(int handle)
        {
            DX.StopSoundMem(handle);
        }
    }
}
