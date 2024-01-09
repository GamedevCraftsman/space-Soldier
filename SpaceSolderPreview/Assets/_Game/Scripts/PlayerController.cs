using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Animator anim;
    [SerializeField] Transform orientation;
    [SerializeField] SpeedController speedController;

    public bool checkStaying = false;

    float moveSpeed;
    Vector2 playerInput;
    Vector3 moveDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        moveSpeed = speedController.speed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        PlayerDirection();
        Moving();
        RotatePlayer();
        Animations();
    }

    void PlayerDirection()
    {
        moveDirection = orientation.forward * playerInput.y + orientation.right * playerInput.x;
    }
    void Animations()
    {
        if (moveDirection == Vector3.zero)
        {
            anim.SetTrigger("Idle");
        }
        else if (checkStaying == false && moveDirection != Vector3.zero)
        {
            anim.SetTrigger("Run");
        }
    }

    void Moving()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    void RotatePlayer()
    {
        transform.Rotate((transform.up * playerInput.x) * rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext _context)
    {
        playerInput = _context.ReadValue<Vector2>();
    }

    public Animator Anim()
    {
        return anim;
    }
}