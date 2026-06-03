using System;
using UnityEngine;

[Serializable]
public class SanityCondition
{
    public enum ConditionType
    {
        Equals = 0,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
    }

    public ConditionType conditionType;
    [Range(0, 100)] public float value;

    public bool ConditionMet()
    {
        var sanity = SanitySystem.Instance.Sanity;

        switch (conditionType)
        {
            case ConditionType.Equals:
                return sanity == value;
            case ConditionType.NotEquals:
                return sanity != value;
            case ConditionType.GreaterThan:
                return sanity > value;
            case ConditionType.LessThan:
                return sanity < value;
            case ConditionType.GreaterThanOrEqual:
                return sanity >= value;
            case ConditionType.LessThanOrEqual:
                return sanity <= value;
            default: return false;
        }
    }
}
