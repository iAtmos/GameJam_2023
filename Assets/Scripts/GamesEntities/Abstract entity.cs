using System;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    protected static class MoveParams
    {
        public const string HorizontalX = nameof(HorizontalX);
        public const string HorizontalZ = nameof(HorizontalZ);
    }

    [field: SerializeField] protected float MaxHP { get; set; }
    [field: SerializeField] protected float CurrentHP { get; set; }
    [field: SerializeField] protected float MaxSpeedMove { get; set; }
    protected bool isLiveEntity { get; private set; }

    protected virtual void Start()
    {
        isLiveEntity = true;
        CurrentHP = MaxHP;
    }

    public bool CheckLiveEyntity()
    {
        if (CurrentHP <= 0)
            isLiveEntity = false;
        return isLiveEntity;
    }

    protected float ConversionRange(float valueConverted, float inputRangeMax, 
        float outputRangeMax = 1, float inputRangeMin = 0, float outputRangeMin = 0)
    {
        var diffOutputRange = MathF.Abs(outputRangeMax - outputRangeMin);
        var diffInputRange = MathF.Abs(inputRangeMax - inputRangeMin);
        var convFactor = (diffOutputRange / diffInputRange);
        return (outputRangeMin + (convFactor * (valueConverted - inputRangeMin)));
    }
}