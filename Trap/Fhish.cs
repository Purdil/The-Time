using NUnit.Framework.Internal;
using System.Collections;
using TMPro;
using UnityEngine;

public class Fhish : MonoBehaviour, IStoping
{
    [SerializeField] private LayerMask _player;
    [SerializeField] private float power;

    private PlayerSkill _playerSkill;
    private Rigidbody2D _rigid;
    private SpriteRenderer _sprite;
    private TextMeshPro _text;

    private bool imGoing = true;
    private int sec;
    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        _rigid = GetComponent<Rigidbody2D>();
        _playerSkill = FindAnyObjectByType<PlayerSkill>().GetComponent<PlayerSkill>();
        _text.gameObject.SetActive(false);
        _sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (transform.position.y > 50)
            Die();
        _text.text = $"{sec / 60}:{sec % 60}";
        if (imGoing && OverLepBox())
        {
            Goinggggg(false);
        }
    }
    private void OnMouseDown()
    {
        if (!imGoing && _playerSkill.onSkill)
        {
            Stoping(0,TimeSkillUIManager.Inctance.sec);
        }
    }
    private void OnMouseEnter()
    {
        if (_playerSkill.onSkill)
            _sprite.color = Color.yellow;
        else
            _sprite.color = Color.white;
        
        
    }
    private void OnMouseExit()
    {
        _sprite.color = Color.white;
    }

    private bool OverLepBox()
    {
        Collider2D overlepBox = Physics2D.OverlapBox(transform.position, new Vector2(1.5f, 200), 0, _player);
        return overlepBox;
    }
    private void Die()
    {
        _text.gameObject.SetActive(false);
        Destroy(gameObject);
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
            Goinggggg(false);
        }
        else
        {
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(TimeSystem());
        }
    }
    
    private void Goinggggg(bool going)
    {
        _text.gameObject.SetActive(false);
        imGoing = going;
        _rigid.AddForce(new Vector2(0, power), ForceMode2D.Impulse);
        
    }
    public void Stoping(int minute, int sec)
    {
        this.sec = sec;
        Debug.Log(this.sec);
        imGoing = false;
        _rigid.linearVelocity = Vector2.zero;
        _text.gameObject.SetActive(true);
        StartCoroutine(TimeSystem());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime player) && !imGoing)
        {
            player.GetDamage(50, gameObject.name);
        }
    }

}
