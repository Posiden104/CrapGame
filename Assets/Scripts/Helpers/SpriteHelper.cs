using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    static class SpriteHelper
    {
        private static readonly Sprite[] CasinoSprites, BlueButtonSprites, WhiteButtonSprites;
        public enum SpriteSheet { CasinoSprites, BlueButtons, WhiteButtons }

        // Static constructors get called 
        // before the first time anything is needed
        static SpriteHelper()
        {
            CasinoSprites = Resources.LoadAll<Sprite>("Images/CasinoSprites");
            BlueButtonSprites = Resources.LoadAll<Sprite>("Images/blueButtons");
            WhiteButtonSprites = Resources.LoadAll<Sprite>("Images/whiteButtons");
        }

        public static void SetSprite(SpriteSheet sheet, string spriteName, SpriteRenderer sr)
        {
            Sprite spr = GetSprite(sheet, spriteName);
            sr.sprite = spr;
        }

        public static Sprite GetSprite(SpriteSheet sheet, string spriteName)
        {
            switch (sheet)
            {
                case SpriteSheet.CasinoSprites:
                    return CasinoSprites.Where(s => s.name == spriteName).FirstOrDefault();
                case SpriteSheet.BlueButtons:
                    return BlueButtonSprites.Where(s => s.name == spriteName).FirstOrDefault();
                case SpriteSheet.WhiteButtons:
                    return WhiteButtonSprites.Where(s => s.name == spriteName).FirstOrDefault();
                default:
                    Debug.LogError($"GetSprite - {sheet} is not a valid sprite sheet.");
                    return null;
            }
        }
    }
}
