using UnityEngine;

public class PlayerController : AbstractEntity
{
    [Header("Movement")]
    private const float correctorSpeed = 10000f;
    private float horizontalX;
    private float horizontalZ;
    private Vector3 moveDerection;

    [Header("Ñharacter Parameters")]
    private Animator animator;
    private Rigidbody rb;

    [Header("Animtaions State")]
    private const string SpeedWalk = nameof(SpeedWalk);

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animator.SetFloat(SpeedWalk, 0f);
    }

    protected void Update()
    {
        PlayerInput();
        animator.SetFloat(SpeedWalk, ConversionRange(rb.velocity.magnitude, MaxSpeedMove));
    }

    protected void FixedUpdate()
    {
        PlayerMove();
        //PlayerRotation(); Âðàùåíèå ìûøêîé
    }

    private void PlayerInput()
    {
        horizontalX = Input.GetAxis(MoveParams.HorizontalX);
        horizontalZ = Input.GetAxis(MoveParams.HorizontalZ);
    }

    private void PlayerMove()
    {
        moveDerection = new Vector3(horizontalX, 0f, horizontalZ);

        if (rb.velocity.magnitude >= MaxSpeedMove)
        {
            moveDerection = Vector3.zero;
        }

        rb.AddRelativeForce(moveDerection.normalized * correctorSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }
}