using System.Collections;
using UnityEngine;

public class AIAttack : AISystems
{
    [Header("Preference shooting")]
    [SerializeField] private GameObject positionSpawnShot;
    //[SerializeField] private Missile prefabShot; // œÂÙ‡· Ô‡ÚÓÌ‡
    [SerializeField] private float distanceAttack;

    public void Shot()
    {
        canShot = false;
        // ...
        // ...
        // ...
        //StartCoroutine(RechargeGun());
    }

    //private IEnumerator RechargeGun()
    //{
    //    yield return new WaitForSeconds(prefabShot.CooldownTime);
    //    canShot = true;
    //}

    public void —heckingAttackCondition()
    {
        if (AIStatusCurent == AIStatus.chase && canShot
            && agent.remainingDistance - agent.stoppingDistance < distanceAttack)
            Shot();
    }
}