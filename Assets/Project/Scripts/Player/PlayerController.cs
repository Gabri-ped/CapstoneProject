using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float superJumpForce = 8.5f;
    [SerializeField] private GroundChecker _groundChecker;
    private float rotationSpeed = 0.1f;

    private Animator _anim;
    private Rigidbody _rb;
    private float rotationVelocity;
    private Vector3 moveDir;
    private float h;
    private float v;

    private bool canSuperJump = false;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Jump();
        Vector3 direction = new Vector3(h, 0f, v).normalized;

       ;
        Vector3 _camForward = Camera.main.transform.forward;
        Vector3 _camRight = Camera.main.transform.right;
        _camForward.y = 0;
        _camRight.y = 0;
        _camForward.Normalize();
        _camRight.Normalize();
        moveDir = (_camForward * direction.z + _camRight * direction.x).normalized;

        if(moveDir.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x,moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity,rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float _speed = isRunning ? runSpeed : walkSpeed;
        float _animSpeed = direction.magnitude * (isRunning ? 1f : 0.5f);
        _anim.SetFloat("Speed", _animSpeed);
    }

    private void FixedUpdate()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float _speed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = moveDir * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + move);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump")&& _groundChecker.IsGrounded)
        {
            float appliedJumpForce = canSuperJump ? superJumpForce : jumpForce;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        }
    }
    public void EnableSuperJump(bool value)
    {
       canSuperJump = value;
    }
}
