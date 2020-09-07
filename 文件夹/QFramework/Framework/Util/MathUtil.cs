namespace QFramework
{
    public partial class MathUtil
    {
        public static bool Percent(int percent)
        {
            return UnityEngine.Random.Range(0, 100) < percent;
        }

        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[UnityEngine.Random.Range(0, values.Length)];
        }
    }
}