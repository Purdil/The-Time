
using System.Collections;
using TMPro;

using UnityEngine;


public class TopPillar : MonoBehaviour , IStoping 
{
    [SerializeField] private LayerMask _whatGround;
    [SerializeField] private LayerMask _whatPlayer;

    private SpriteRenderer _spriteRen;
    private TextMeshPro _TMP;
    private GameObject _Text;
    private Collider2D _collider;
    private Rigidbody2D _rigid;
    private PlayerSkill _playerSkill;
    private PlayerTime _playerTime;
    public int leftRight = 0;
    public bool CanDroping { get; set; } = true;
    public bool imDroping { get; set; } = false;
    
    private int sec;
    private void Awake()
    {
        _Text = GetComponentInChildren<TextMeshPro>().gameObject;
        _TMP = _Text.GetComponent<TextMeshPro>();
        _collider = GetComponent<Collider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _playerSkill = GameObject.FindAnyObjectByType<PlayerSkill>().GetComponent<PlayerSkill>();
        _playerTime = GameObject.FindAnyObjectByType<PlayerTime>().GetComponent<PlayerTime>();
        _spriteRen = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
    
    }
        
    private void Update()
    {
        if (sec != 0) _Text.GetComponentInChildren<TextMeshPro>().text = $"{sec/60}:{sec%60}";
        if (OnDropingRay() && CanDroping)
        {
            Droping();
        }        
    }

    private void OnMouseDown()
    {
        if (_playerSkill.onSkill)
        {
            _playerTime.GetDamage(TimeSkillUIManager.Inctance.sec, "Pillar");
            Stoping(0,TimeSkillUIManager.Inctance.sec);
        }
    }
              
               
              
            
              
               

    public void Droping()
    {
        CanDroping = false;
        _Text.SetActive(false);
        _collider.enabled = true;
        imDroping = true;
        _rigid.constraints = RigidbodyConstraints2D.None;
        _rigid.gravityScale = 14f;
    }
    public IEnumerator Time()
    {
        for (; sec != 0;)
        {
            yield return new WaitForSeconds(0.1f);
            sec -= 1;
        }
        _TMP.text = $"{sec/60}:{sec%60}";
        if (sec <= 0)
        {
            _TMP.enabled = true;
            Droping();
        }
        else StartCoroutine(Time());
    }
            

    public void Stoping(int minute, int sec)
    {
        _collider.enabled = false;
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        imDroping = false;
        _rigid.freezeRotation = true;
        _rigid.gravityScale = 0;
        _rigid.linearVelocity = Vector2.zero;
        this.sec = sec;
        _collider.enabled = true;
        _Text.SetActive(true);
        _Text.GetComponent<TextMeshPro>().enabled = true;
        CanDroping = false;
        StartCoroutine(Time());
    }

   
       
    public bool OnDropingRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(gameObject.GetComponentInParent<Transform>().position + (Vector3.down * 2) + (leftRight == 0 ? (Vector3.left * 2f): (Vector3.right * 2f)), new Vector2(1, 3), 0, transform.position, 0, _whatPlayer);
        return ray;
    }
    private bool GroundRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(2, 2f), 0, transform.position, 0, _whatGround);
        return ray;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gameObject.GetComponentInParent<Transform>().position + (Vector3.down * 2) + (leftRight == 0 ? (Vector3.left * 2f) : (Vector3.right * 2f)), new Vector3(1, 3, 0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && imDroping)
        {
           _playerTime.GetDamage(120, "sec");
            imDroping = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke("FreezeRigid", 0.3f);
        }
      
         
    }
    private void FreezeRigid()
    {
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        imDroping = false;
    }

    private void OnMouseEnter()
    {
        if(_playerSkill.onSkill)
            _spriteRen.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        _spriteRen.color = Color.white;
    }
    
    
}
