using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;

    
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * 10f * Time.deltaTime);
        }
    }
}
