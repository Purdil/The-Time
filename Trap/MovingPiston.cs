using System.Collections;
using UnityEngine;

public class MovingPiston : MonoBehaviour
{
    [SerializeField] private GameObject _piston;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float speed;

    public int pistionGravity;
    private Rigidbody2D _pistonRigid;
    private Piston _pistonScript;
    private HingeJoint2D _hinge;
    private Rigidbody2D _rigid;
    

    private void Awake()
    {
        _pistonRigid = _piston.GetComponent<Rigidbody2D>();
        _pistonScript = _piston.GetComponent<Piston>();
        _rigid = GetComponent<Rigidbody2D>();
        _hinge = GetComponent<HingeJoint2D>();
    }
    private void Start()
    {
        StartCoroutine(Moving());
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minValue, _maxValue), 0);
    }
    public IEnumerator Moving()
    {
        while (true)
        {
            for (; true;) // ¿Ã¶ó°©´Ï´Ù.
            {
                yield return null;
                for (; !(_pistonScript.ImMoving);)
                {
                    _rigid.linearVelocity = Vector2.zero;
                    _pistonRigid.linearVelocity = Vector2.zero;
                    yield return null;
                }
                _pistonRigid.gravityScale = 0;
                _rigid.AddForce(Vector2.up * speed, ForceMode2D.Force);
                if (transform.localPosition.y == _maxValue)
                {
                    _rigid.linearVelocity = Vector2.zero;
                    break;
                }
            }
            for (; !(_pistonScript.ImMoving);)
            {
                _rigid.linearVelocity = Vector2.zero;
                _pistonRigid.linearVelocity = Vector2.zero;
                yield return null;
            }
            _hinge.enabled = false;
            _pistonRigid.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
            _pistonScript.ImDroping = true;
            _pistonRigid.gravityScale = pistionGravity;
            StartCoroutine(CheackMoving());
            yield return new WaitForSeconds(0.1f);
            for (; !(_piston.GetComponent<Piston>().GroundRay());)
            {
                yield return null;
            }
            _pistonScript.ImDroping = false;
            for (; transform.localPosition.y > _minValue;) // ³»·Á°©´Ï´Ù.
            {
                yield return null;
                for (; !(_pistonScript.ImMoving);)
                {
                    _rigid.linearVelocity = Vector2.zero;
                    _pistonRigid.linearVelocity = Vector2.zero;
                    yield return null;
                }
                _pistonRigid.gravityScale = 0;
                _rigid.AddForce(Vector2.down * speed, ForceMode2D.Force);
                if (transform.localPosition.y == _minValue)
                {
                    _rigid.linearVelocity = Vector2.zero;
                   break;
                }
            }
            _pistonScript.ImDroping = false;
            _pistonRigid.gravityScale = 0;
            _hinge.enabled = true;
        }

    }
    private IEnumerator CheackMoving()
    {
        yield return null;
        for (; !(_pistonScript.ImMoving);)
        {
            _rigid.linearVelocity = Vector2.zero;
            _pistonRigid.linearVelocity = Vector2.zero;
            yield return null;
        }
    }
}
            
           

            


