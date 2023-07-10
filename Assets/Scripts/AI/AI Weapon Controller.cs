using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.IO.LowLevel.Unsafe;

public class AIWeaponController : MonoBehaviour
{

    //bullet
    [SerializeField] private GameObject _bullet;

    //bullet force
    public float _shootForce;

    //Gun stats
    public float _timeBetweenShooting;

    //References
    [SerializeField] private Transform _attackPoint;

    //Sounds
    [SerializeField] private AudioSource _shootingSound;

    [SerializeField] private bool _allowInvoke = true;

    private bool inSight;
    public void Shoot(Vector3 directionToPlayer)
    {
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(_attackPoint.transform.position, directionToPlayer.normalized, out hit))
            targetPoint = hit.point;
        else
            targetPoint = hit.point;
        Debug.DrawRay(transform.position, directionToPlayer);
        //calculate direction from attackPoint to targetPoint
        Vector3 directiron = targetPoint;
        // Instantiate bullet
        GameObject currentBullet = Instantiate(_bullet, _attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directiron.normalized;

        //add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directiron.normalized * _shootForce, ForceMode.Impulse);

        //Instantiate muzzleFlash
        if (_shootingSound != null)
        {
            _shootingSound.Play();
        }

        //Invoke resetShot function
        if (_allowInvoke)
        {
            Invoke("ResetShot", _timeBetweenShooting);
            _allowInvoke = false;
        }
    }
    private void ResetShot()
    {
        _allowInvoke = true;
    }
}