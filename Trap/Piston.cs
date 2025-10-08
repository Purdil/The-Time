using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Piston : MonoBehaviour , IStoping
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _text;
    [SerializeField] private MovingPiston _movingPiston;

    private SpriteRenderer _spriteRen;
    private BoxCollider2D _collider;
    private Rigidbody2D _rigid;
    private PlayerSkill _playerSkill;
    private int _damage = 120;
    
    private int sec;
    public bool ImMoving { get; set; } = true;
    public bool ImDroping { get; set; } = false;

    private void Awake()
    {
        _playerSkill = GameObject.FindFirstObjectByType<PlayerSkill>();
        _collider = GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRen = GetComponent<SpriteRenderer>();
    }
    public bool GroundRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(new Vector3(transform.position.x,transform.position.y-0.7f), new Vector2(_collider.size.x, _collider.size.y), 0, transform.position, 0, _groundLayer);
        return ray;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - 0.7f), new Vector2(2.524708f, 1.237313f));
    }
    private IEnumerator TimeSystem()
    {
        
        for (; sec > 0;)
        {
            _text.GetComponent<TextMeshPro>().text = $"{sec / 60} : {sec % 60}";
            yield return new WaitForSeconds(0.1f);
            sec -= 1;
        }
        if (sec <= 0)
        {
            sec = 0;
            _text.SetActive(false);
            Moving();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime playerTime))
        {
            if (ImDroping && ImMoving)
                playerTime.GetDamage(_damage, "Piston");
        }


    }



    private void OnMouseDown()
    {
        if (_playerSkill.onSkill && ImMoving && TimeSkillUIManager.Inctance.sec > 0)
        {
            _text.SetActive(true);
            Stoping(0,TimeSkillUIManager.Inctance.sec);        
        }
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

    public void Stoping(int minute, int sec)
    {
        ImMoving = false;
        this.sec = sec;
        if (ImDroping)
        {
            _rigid.linearVelocity = Vector2.zero;
            _rigid.gravityScale = 0;
            StartCoroutine(TimeSystem());
        }
        else
        {
            StartCoroutine(TimeSystem());
        }
        
    }
    public void Moving()
    {
        ImMoving = true;
        _rigid.gravityScale = 0;
        if (ImDroping)
        {
            _rigid.gravityScale = _movingPiston.pistionGravity;
        }
            
    }
   
}
