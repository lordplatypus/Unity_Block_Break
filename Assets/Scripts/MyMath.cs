using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath
{
    public const float PI = 3.14f;
    public const float Deg2Rad = PI / 180f;

    static System.Random random;
    public static void Init()
    {
        random = new System.Random();
    }
    public static int Range(int min, int max)
    {
        return random.Next(min, max);
    }

    // 指定した範囲の小数の乱数を取得する（maxは出ないので注意）
    public static float Range(float min, float max)
    {
        return (float)(random.NextDouble() * (max - min) + min);
    }

    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}
