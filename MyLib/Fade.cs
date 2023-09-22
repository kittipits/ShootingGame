using System;
using DxLibDLL;

namespace MyLib
{
    public class Fade
    {
        int maxTime;
        int startAlpha;
        int endAlpha;
        int image;
        int time;

        public Fade()
        {
            maxTime = 1;
            startAlpha = 0;
            endAlpha = 0;
            image = -1;
            time = maxTime;
        }

        /// <summary>
        /// フェード処理を開始する
        /// </summary>
        /// <param name="maxTime">フェード時間（フレーム数）</param>
        /// <param name="startAlpha">フェード開始時点の透明度</param>
        /// <param name="endAlpha">フェード終了時点の透明度</param>
        /// <param name="image">フェード用画像の画像ハンドル</param>
        public void Start(int maxTime, int startAlpha, int endAlpha, int image)
        {
            time = 0; //タイマー初期化
            this.maxTime = Math.Max(maxTime, 1);　//0の予算の防止で下限を1に
            this.image = image;
            //startAlphaとendAlphaは0～255でフランク
            this.startAlpha = Math.Min(Math.Max(startAlpha, 0), 255);
            this.endAlpha = Math.Min(Math.Max(endAlpha, 0), 255);
        }

        public void Update()
        {
            time++;

            if (time >= maxTime) 
            {
                time = maxTime;
            }
        }

        public void Draw()
        {
            if (!IsFading()) return;

            float t = time / (float)maxTime;
            int alpha = (int)((1.0f - t) * startAlpha + t * endAlpha);

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
            DX.DrawGraph(0, 0, image);
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
        }

        public bool IsFading()
        {
            return time != maxTime;
        }
    }
}
