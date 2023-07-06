using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    [field: SerializeField] protected float MaxHP { get; set; }
    [field: SerializeField] protected float CurrentHP { get; set; }
    [field: SerializeField] protected float SpeedMove { get; set; }
    protected bool isLiveEntity { get; set; }

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
}