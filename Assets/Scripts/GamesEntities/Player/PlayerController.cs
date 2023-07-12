using UnityEngine;

public class PlayerController : AbstractEntity
{
    [Header("Movement")]
    private const float correctorSpeed = 10000f;
    private float horizontalX;
    private float horizontalZ;
    private float rotationY;

    [Header("Miscalculation movement")]
    [field: SerializeField] private Camera viewPlayer;
    private Vector3 moveDerection;

    [Header("—haracter Parameters")]
    private Animator animator;
    private Rigidbody rb;

    [Header("Character level stats")]
    public int currentLevel;
    public int exp;

    [Header("Animtaions State")]
    private const string SpeedWalk = nameof(SpeedWalk);

    public LevelUpUpgrades levelUpUpgrades;
    private PlayerInput input;
    public GameObject gameOver;
    
    protected override void Start()
    {
        input = new PlayerInput();
        input.Enable();
        levelUpUpgrades = GameObject.Find("ManagerGamesScean").gameObject.GetComponent<LevelUpUpgrades>();
        base.Start();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animator.SetFloat(SpeedWalk, 0f);
    }

    protected void OnEnable()
    {
        input.Enable();
    }
    protected void OnDisable()
    {
        input.Disable();
    }

    protected void Update()
    {
        PlayerInput();
        PlayerOffsetAngle();
        animator.SetFloat(SpeedWalk, ConversionRange(rb.velocity.magnitude, MaxSpeedMove));
    }

    protected void FixedUpdate()
    {
        PlayerMove();
        //PlayerRotation(); ¬‡˘ÂÌËÂ Ï˚¯ÍÓÈ
    }

    private void PlayerInput()
    {
        horizontalX = input.Player.Move.ReadValue<Vector2>().x;
        horizontalZ = input.Player.Move.ReadValue<Vector2>().y;
    }

    private void PlayerMove()
    {
        moveDerection = new Vector3(horizontalX, 0f, horizontalZ);

        if (rb.velocity.magnitude >= MaxSpeedMove)
        {
            moveDerection = Vector3.zero;
        }

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // !!! »«Ã≈Õ»“‹ Õ¿ ‘»«» ” !!!
        rb.AddForce(moveDerection.normalized * correctorSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void PlayerOffsetAngle()
    {
        var differenceBetweenCursorAndPlayerPosition = viewPlayer.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotationY = Mathf.Atan2(differenceBetweenCursorAndPlayerPosition.x, differenceBetweenCursorAndPlayerPosition.z) * Mathf.Rad2Deg;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Player damage recived");
        CurrentHP -= damage;
        if (!CheckLiveEyntity())
        {
            gameOver.GetComponent<GameOverMeny>().OnDeath();
            Destroy(gameObject);
        };
    }
    
    public void CheckLvlUp()
    {
        if(exp>=10)
        {
            currentLevel++;
            exp = 0;
            levelUpUpgrades.GetUpgrades();
        }
    }
}