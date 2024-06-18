using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public static bool isThirdDialog = false;
        public static bool isFourthDialog = false;

        public static bool isBirdFlight = false;
    }
}

