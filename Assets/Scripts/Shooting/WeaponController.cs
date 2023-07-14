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
    private int _ShootLines = 1;

    //bools 
    private bool _shooting;
    private bool readyToShoot = true;

    //References
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _attackPoint30Left;
    [SerializeField] private Transform _attackPoint30Right;
    [SerializeField] private Transform _attackPoint60Left;
    [SerializeField] private Transform _attackPoint60Right;

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
        GameObject currentBulletMid = Instantiate(_bullet, _attackPoint.position, Quaternion.identity);
        currentBulletMid.transform.forward = _attackPoint.transform.forward;
        currentBulletMid.GetComponent<Rigidbody>().AddForce(_attackPoint.transform.forward.normalized * _shootForce, ForceMode.Impulse);
        if (_ShootLines > 1)
        {
            GameObject currentBullet30left = Instantiate(_bullet, _attackPoint30Left.position, Quaternion.identity);
            currentBullet30left.transform.forward = _attackPoint30Left.transform.forward;
            currentBullet30left.GetComponent<Rigidbody>().AddForce(_attackPoint30Left.transform.forward.normalized * _shootForce, ForceMode.Impulse);
            GameObject currentBullet30Right = Instantiate(_bullet, _attackPoint30Right.position, Quaternion.identity);
            currentBullet30Right.transform.forward = _attackPoint30Right.transform.forward;
            currentBullet30Right.GetComponent<Rigidbody>().AddForce(_attackPoint30Right.transform.forward.normalized * _shootForce, ForceMode.Impulse);
            if(_ShootLines > 2)
            {
                GameObject currentBullet60left = Instantiate(_bullet, _attackPoint60Left.position, Quaternion.identity);
                currentBullet60left.transform.forward = _attackPoint60Left.transform.forward;
                currentBullet60left.GetComponent<Rigidbody>().AddForce(_attackPoint60Left.transform.forward.normalized * _shootForce, ForceMode.Impulse);
                GameObject currentBullet60Right = Instantiate(_bullet, _attackPoint60Right.position, Quaternion.identity);
                currentBullet60Right.transform.forward = _attackPoint60Right.transform.forward;
                currentBullet60Right.GetComponent<Rigidbody>().AddForce(_attackPoint60Right.transform.forward.normalized * _shootForce, ForceMode.Impulse);
            }
        }
        

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

    public void AddLines()
    {
        _ShootLines++;
    }
}