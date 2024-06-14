using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public static class GlobalsVar
    {
        public static float HP = 100f;
        public static float MANA = 0f;

        public static float _magicRotate = 0f;

        public static List<Item> Items = new List<Item>();

        public static bool isBasicMagicalAbilities = false;

    } 
}

