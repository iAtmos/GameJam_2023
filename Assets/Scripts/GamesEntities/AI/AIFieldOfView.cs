using System;
using UnityEngine;

public class AIFieldOfView : AISystems
{
    [Header("Field of view settings")]
    //[SerializeField] private float RadiusFieldsView;
    public float RadiusFieldsView; // Заменить после настройки
    [Range(0, 360)] public float AngleView;
    [SerializeField] private LayerMask TargetMask;

    [Header("Masked objects")]
    private Collider[] objectsArea;
    //private bool canSeePlayer;
    public bool canSeePlayer; // Заменить после настройки

    private void FixedUpdate()
    {
        FieldView();
    }

    public void CheckingFieldView()
    {
        if (canSeePlayer)
        {
            AIStatusCurent = AIStatus.chase;
        }
    }

    private void FieldView()
    {
        objectsArea = Physics.OverlapSphere(transform.position, RadiusFieldsView, TargetMask);

        if (objectsArea.Length != 0)
        {
            var target = objectsArea[0].transform;
            var directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < AngleView / 2)
            {
                canSeePlayer = true;
                purposePersecution = target.transform.GetChild(0);
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            purposePersecution = null;
            canSeePlayer = false;
        }
    }
}