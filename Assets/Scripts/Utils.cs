using UnityEngine;

public static class EnumUtils
{
    public static T RandomEnumValue<T>()
    {
        var values = System.Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }
}
