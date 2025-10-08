using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _groundLayer;

    private GameObject _text;
    private EnemyAni _ani;
    private GameObject _player;
    private Transform _playerTran;
    private Rigidbody2D _rigid;
    private PlayerSkill _playerSkill;
    private PlayerTime _playerTime;

   
    // 멈추게 하면 공격 애니메이션 멈춤,(5초) 다시 애니메이션 돌아감.
    private float moveSpeed = 6; 
    private float Distance = 8;
    public bool stopAtt { get; set; } = false;
    public bool OnAtt { get; set; } = false;

    Vector3 _rn;
    Vector3 _originSize;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerTran = _player.transform;
        _rigid = GetComponent<Rigidbody2D>();
        _ani = GetComponentInChildren<EnemyAni>();
        _originSize = transform.localScale;
        _text = GetComponentInChildren<TextMeshPro>().gameObject;
        _playerSkill = _player.GetComponent<PlayerSkill>();
        _playerTime = _player.GetComponent<PlayerTime>();
    }

    private void Start()
    {
        StartCoroutine(Idle());
    }
   
    private IEnumerator Idle()
    {
        for (; true;)
        {
            yield return null;
            if ( (transform.localScale.x > 0 && OverlepBoxRight()) || (transform.localScale.x < 0 && OverlepBoxLeft())  && !OnAtt)
            {
                StartCoroutine(Att());
                break;
            }
            if ( Vector2.Distance(transform.position, _playerTran.position) < Distance)
            {
                _ani.WallkAni = true;
                _rn = (_playerTran.transform.position - transform.position);
                if (!GroundRay1())
                {
                   
                    if (_rn.x > 0)
                    {
                        _rigid.linearVelocityX = _rn.normalized.x * moveSpeed;
                    }
                    else
                    {
                        _ani.WallkAni = false;
                        _rigid.linearVelocity = Vector2.zero;
                    }
                }
                else if(!GroundRay2())
                {
                    if (_rn.x < 0)
                    {
                        _rigid.linearVelocityX = _rn.normalized.x * moveSpeed;
                    }
                    else
                    {
                        _ani.WallkAni = false;
                        _rigid.linearVelocity = Vector2.zero;
                    }
                }
                else
                {
                    _rigid.linearVelocityX = _rn.normalized.x * moveSpeed;
                }
                if (_playerTran.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector2(-_originSize.x, _originSize.y);
                    _text.transform.localScale = new Vector3(-1, 1, 0);
                }
                else
                {
                    _text.transform.localScale = new Vector3(1, 1, 0);
                    transform.localScale = _originSize;
                }
            }
            else
            {
                _ani.WallkAni = false;
                _rigid.linearVelocity = Vector2.zero;
            }
           
          
        }
    }
    private void OnMouseDown()
    {
        if (stopAtt && _playerSkill.onSkill && TimeSkillUIManager.Inctance.sec >= 5 && OnAtt)
        {
            StartCoroutine(StopAtt());
            _playerTime.GetDamage(TimeSkillUIManager.Inctance.sec, gameObject.name);
        }
    }

    private IEnumerator StopAtt()
    {
        _ani.StopAni();
        yield return new WaitForSeconds(1);
        _ani.StartAni();
    }

  
    private IEnumerator Att()
    {
        int a = UnityEngine.Random.Range(0, 3);
   
        _rigid.linearVelocity = Vector2.zero;
        _ani._ani.SetBool("OnAtt", true);
        OnAtt = true;
        for (; true;)
        {
            yield return null;
            if (!OnAtt)
                break;
            
        }
        OnAtt = false;
        _ani._ani.SetBool("OnAtt", false);
        if (a == 0)
        {
                 StartCoroutine(Att());
        }
        else
        {
            StartCoroutine(Idle());
        }

      

    }










    private bool GroundRay1()
    {
        RaycastHit2D ray1 = Physics2D.BoxCast(transform.position + new Vector3(-0.5f, -1), new Vector3(0.05f, 0.5f), 0,transform.position,0,_groundLayer);
        return ray1;
    }
    private bool GroundRay2()
    {
        RaycastHit2D ray2 = Physics2D.BoxCast(transform.position + new Vector3(0.8f, -1), new Vector3(0.05f, 0.5f), 0, transform.position, 0, _groundLayer);
        return ray2;
    }
    public bool OverlepBoxRight()
    {
      
        Collider2D overLepBox = Physics2D.OverlapBox(transform.position + new Vector3(1.2f, 0.5f), new Vector3(1.8f, 3), 0, _playerLayer);
        return overLepBox;
    }
    public bool OverlepBoxLeft()
    {

        Collider2D overLepBox = Physics2D.OverlapBox(transform.position + new Vector3(-1.2f, 0.5f), new Vector3(1.8f, 3), 0, _playerLayer);
        return overLepBox;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + new Vector3(1.2f, 0.5f), new Vector3(1.7f, 3));
        Gizmos.DrawCube(transform.position + new Vector3(-1.2f, 0.5f), new Vector3(1.7f, 3));
    }








}
