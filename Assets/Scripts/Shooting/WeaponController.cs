using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.IO.LowLevel.Unsafe;

public class WeaponController : MonoBehaviour
{

    //bullet
    [SerializeField] private GameObject _bullet;

    //bullet force
    public float _shootForce;

    //Gun stats
    public float _timeBetweenShooting;

    //bools 
    private bool _shooting;
    private bool readyToShoot = true;

    //References
    [SerializeField] private Transform _attackPoint;

    //Sounds
    [SerializeField] private AudioSource _shootingSound;

    [SerializeField] private bool _allowInvoke = true;

    private PlayerInput _input;
    private void Awake()
    {
        _input = new PlayerInput();
        //make sure magazine is full
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _shooting = _input.Player.Shoot.inProgress;
        if (_shooting)
        {
            if (readyToShoot)
            {
                //shoot main
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        //find the exact hit positon using a raycast

        // Instantiate bullet
        GameObject currentBullet = Instantiate(_bullet, _attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = _attackPoint.transform.forward;

        //add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(_attackPoint.transform.forward.normalized * _shootForce, ForceMode.Impulse);

        //Instantiate muzzleFlash
        if(_shootingSound != null)
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
        readyToShoot = true;
        _allowInvoke = true;
    }
}