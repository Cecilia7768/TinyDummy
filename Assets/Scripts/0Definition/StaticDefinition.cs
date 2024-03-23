using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public class GameSystem
    {
        //���� ù ���࿩��
        public static bool isFirstGameStart = true;
    }

    public class JjackStandard
    {
        //���� ���۽� ���� ���� ���� 
        public const int FirstCreatCount = 2;
        //��ü �Ҹ� �ð�
        public const float BodyExtinctionTime = 3.5f;
        //����� ��θӸ��� Ȯ��
        public const float ProbabilityBossEgg = 0f;

        //����� �ֻ��� Ȯ��
        public static float ProbabilityDoubleEgg = 100f;

        public static int MaleCount = 0;
        public static int FemaleCount = 0;

        public static int MaleAdultCount = 0;

        public static int EggCount = 0;
        public static int ChildCount = 0;
        public static int AdultCount = 0;
        public static int OldCount = 0;
        public static int DeadCount = 0;
        public static int TotalCount = 0;

        public static int BossCount = 0;
        public static int BossMaxCount = 1;
    }

    public class Tags
    {
        public const string DRINK = "Drink";
        public const string FOOD = "Food";
    }
} 