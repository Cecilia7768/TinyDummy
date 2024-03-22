using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public class JjackStandard
    {
        //���� ���۽� ���� ���� ���� 
        public const int FirstCreatCount = 2;
        //��ü �Ҹ� �ð�
        public const float BodyExtinctionTime = 3.5f;
        //����� �ֻ��� Ȯ��
        public const float ProbabilityDoubleEgg = 20f;
        //����� ��θӸ��� Ȯ��
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