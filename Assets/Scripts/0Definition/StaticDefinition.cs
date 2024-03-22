using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public class JjackStandard
    {
        //게임 시작시 유닛 생성 개수 
        public const int FirstCreatCount = 2;
        //시체 소멸 시간
        public const float BodyExtinctionTime = 3.5f;
        //산란시 쌍생일 확률
        public const float ProbabilityDoubleEgg = 20f;
        //산란시 우두머리일 확률
        public const float ProbabilityBossEgg = 10f;

        public static int MaleCount = 0;
        public static int FemaleCount = 0;

        public static int MaleAdultCount = 0;

        public static int EggCount = 0;
        public static int ChildCount = 0;
        public static int AdultCount = 0;
        public static int OldCount = 0;
        public static int DeadCount = 0;

        public static int BossCount = 0;

    }

    public class Tags
    {
        public const string DRINK = "Drink";
        public const string FOOD = "Food";
    }
} 