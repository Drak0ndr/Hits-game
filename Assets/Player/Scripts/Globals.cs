using System.Collections.Generic;

namespace Player
{
    public static class GlobalsVar
    {
        public static float PlayerHP = 100f;
        public static float PlayerMANA = 0f;

        public static float EnemyHP = 100f;

        public static float _magicRotate = 0f;

        public static List<Item> Items = new List<Item>();

        public static bool isBasicMagicalAbilities = false;
        public static bool isSpecialMagicalAbilities = false;
        public static bool isThirdDialog = false;
        public static bool isFourthDialog = false;

        public static bool isBirdFlight = false;

        public static bool isFight = true;
        public static bool isFirstFight = true;

        public static bool isDeath = false;
        public static bool isWasInCamp = false;

        public static bool isTitles = false;

        public static bool isFrog = true;

        public static int currEnvelope = -1;
    }
}

