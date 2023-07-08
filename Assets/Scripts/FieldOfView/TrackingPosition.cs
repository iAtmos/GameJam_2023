using Unity.VisualScripting;
using UnityEngine;

public class TrackingPosition : MonoBehaviour
{
    [field: SerializeField] private Transform playerPosition;
    [field: SerializeField] private float positionLag = 1f;

    private void LateUpdate()
    {  
        var targetPosition = new Vector3(playerPosition.position.x, 0f, playerPosition.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * positionLag);
    }    
}
