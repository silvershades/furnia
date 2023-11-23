using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    public float moveSpeed = 4f;
    private bool _isMoving = false;
    private bool _canMove = true;
    private float _moveDistance = 1;
    private Vector2 _moveInput = Vector2.zero;
    private float _lastInputX;
    private float _lastInputY;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckKeyboardMovement();
        UpdateAnimation();
    }

    void CheckKeyboardMovement()
    {
        if (!_isMoving && _canMove)
        {
            // Vector3 moveDirection = Vector3.zero;
            // if (Keyboard.current.wKey.isPressed)
            // {
            //     moveDirection = new Vector3(0, _moveDistance, 0);
            // }
            // else if (Keyboard.current.sKey.isPressed)
            // {
            //     moveDirection = new Vector3(0, -_moveDistance, 0);
            // }
            // else if (Keyboard.current.aKey.isPressed)
            // {
            //     moveDirection = new Vector3(-_moveDistance, 0, 0);
            // }
            // else if (Keyboard.current.dKey.isPressed)
            // {
            //     moveDirection = new Vector3(_moveDistance, 0, 0);
            // }
            //
            // if (moveDirection != Vector3.zero)
            // {
            //     StartCoroutine(MovePlayerOverSeconds(moveDirection));
            // }
            if (_moveInput != Vector2.zero)
            {
                StartCoroutine(MovePlayerOverSeconds());
            }
            
        }
    }

    void UpdateAnimation()
    {
        animator.SetBool("IsMoving",true);
        animator.SetFloat("MoveX", _isMoving ? _moveInput.x : _lastInputX);
        animator.SetFloat("MoveY", _isMoving ? _moveInput.y : _lastInputY);
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    IEnumerator MovePlayerOverSeconds()
    {
        _isMoving = true;
        _lastInputX = _moveInput.x;
        _lastInputY = _moveInput.y;
        float elapsedTime = 0;
        Vector2 startingPos = new Vector2(transform.position.x,transform.position.y);
        Vector2 finalPos = startingPos + direction;

        while (elapsedTime < moveSpeed)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is accurate.
        transform.position = finalPos;

        _isMoving = false;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}