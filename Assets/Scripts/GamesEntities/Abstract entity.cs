using System;
using UnityEngine;
using UnityEngine.UI;



public abstract class AbstractEntity : MonoBehaviour
{
    public Scrollbar ScrollbarHP;
    protected static class MoveParams
    {
        public const string HorizontalX = nameof(HorizontalX);
        public const string HorizontalZ = nameof(HorizontalZ);
    }

    [field: SerializeField] protected float MaxHP = 100f;
    [field: SerializeField] protected float CurrentHP { get; set; }
    [field: SerializeField] protected float MaxSpeedMove = 6f;
    public bool isLiveEntity { get; set; }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
            CurrentHP -= 1;
        ScrollbarHP.size = CurrentHP;
    }
}