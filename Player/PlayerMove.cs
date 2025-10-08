using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _poTanLayer;
    [SerializeField] private Transform _rayPosition;

    private Rigidbody2D _rigid;

    Vector2 movement;

    public bool CanMove { get; set; } = true;
    private bool realGravity = false;
    private float moveSpeed = 5;
    private float jumpPower = 5;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    
    }
    private void Update()
    {
        if(CanMove)
           _rigid.linearVelocityX = movement.x * moveSpeed;
        if (!(IsGround()) && !realGravity)
        {
            StartCoroutine(RealGravity());
        }

    }

    private IEnumerator RealGravity()
    {
        realGravity = true;
        yield return new WaitForSeconds(0.1f);
        for (float i = 1.1f; true; i += 0.3f)
        {
            if (IsGround() || IsPoTan())
            {
                _rigid.gravityScale = 1;
                break;
            }
            yield return new WaitForSeconds(0.05f);
            _rigid.gravityScale = i;
            if (_rigid.gravityScale > 3.5f)
            {
                _rigid.gravityScale = Mathf.Clamp(_rigid.gravityScale, 3.5f, 3.5f);
            }
            if (IsGround() || IsPoTan()) 
            {
                _rigid.gravityScale = 1;
                break;
            }
        }
        realGravity = false;
    }

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnJump(InputValue value)
    {
        if (value.isPressed && (IsGround() || IsPoTan()) && CanMove )
        {
            
            _rigid.gravityScale = 1;
            Jump();
        }
    }
    private void Jump()
    {
        _rigid.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
    }
    private bool IsGround()
    {
        RaycastHit2D ray = Physics2D.BoxCast(_rayPosition.position, new Vector2(0.5f, 0.07f), 0, transform.position, 0, _groundLayer);
        return ray;
    }
    private bool IsPoTan()
    {
        RaycastHit2D ray = Physics2D.BoxCast(_rayPosition.position, new Vector2(0.5f, 0.07f), 0, transform.position, 0, _poTanLayer);
        return ray;
    }

    private void OnDrawGizmos()                     
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_rayPosition.position, new Vector3(0.5f, 0.07f, 0));
    }
}
