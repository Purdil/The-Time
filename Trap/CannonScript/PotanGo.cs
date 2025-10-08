using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PotanGo : MonoBehaviour , IStoping
{
    private Animator _ani;
    private Rigidbody2D _rigid;
    private Collider2D _collider;
    private PlayerSkill _playerSkill;
    private SpriteRenderer _sprite;
    private TextMeshPro _timeText;

    private bool imMoving = true;
    private int sec = 0;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _playerSkill = FindAnyObjectByType<PlayerSkill>().GetComponent<PlayerSkill>();
        _sprite = GetComponent<SpriteRenderer>();
        _timeText = GetComponentInChildren<TextMeshPro>();
        _timeText.gameObject.SetActive(false);
    }
    private void Start()
    {
      Invoke("ImDie", 4);
    }
        
        
        
        


    public void ImDie()
    {
        if (imMoving)
        {
          _timeText.gameObject.SetActive(false);
          Destroy(gameObject);
        }
        else
          Invoke("ImDie", 4);
    }

    private void Update()
    {
        _timeText.text = $"{sec / 60}:{sec % 60}";
    }
    private IEnumerator TimeSystem()
    {
        for (; sec > 0;)
        {
            yield return new WaitForSeconds(0.1f);
            sec -= 1;
        }
        if (sec <= 0)
        {
            sec = 0;
            ImMoving();
        }
        else
        {
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(TimeSystem());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            return;
        }
        if (imMoving)
        {
            if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime player))
            {
                player.GetDamage(15, gameObject.name);
            }

            _ani.SetBool("OnDie", true);
            _collider.isTrigger = true;
            _rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
            _rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            _rigid.linearVelocity = Vector2.zero;
        }
    }

       
        
    private void OnMouseDown()
    {
        if (_playerSkill.onSkill && imMoving && !_ani.GetBool("OnDie"))
        {
            _playerSkill.gameObject.GetComponent<PlayerTime>().GetDamage(TimeSkillUIManager.Inctance.sec, gameObject.name);
            Stoping(0,TimeSkillUIManager.Inctance.sec);
        }
    }
    private void OnMouseEnter()
    {
        if (_playerSkill.onSkill)
          _sprite.color = Color.yellow;
        else
            _sprite.color = Color.black;
    }
    private void OnMouseExit()
    {
        _sprite.color = Color.black;
    }

    public void Stoping(int minute, int sec)
    {
        this.sec = sec;
        imMoving = false;
        _rigid.linearVelocity = Vector2.zero;
        _timeText.gameObject.SetActive(true);
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigid.freezeRotation = true;
        StartCoroutine(TimeSystem());
    }

    public void ImMoving()
    {

        _timeText.gameObject.SetActive(false);
        _rigid.constraints = RigidbodyConstraints2D.None;
        _rigid.gravityScale = 1;
        _rigid.freezeRotation = false;
        imMoving = true;
    }

}
