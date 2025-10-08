using NUnit.Framework.Constraints;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingObject : MonoBehaviour , IStoping
{
    [SerializeField] private int select;
    [SerializeField] private SpriteRenderer[] _spriteRenderer;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private GameObject _text;
   
    IEnumerator _movingCorutine;
    private PlayerSkill _playerSkill;
    private Rigidbody2D _rigid;
    [SerializeField] private float _pwoer = 3;
    private bool imMoving { get; set; } = true;
    private int sec;
    private void Awake()
    {
        _playerSkill = FindAnyObjectByType<PlayerSkill>().GetComponent<PlayerSkill>();
        _rigid = GetComponent<Rigidbody2D>();
        _movingCorutine = Moving();
    }
    private void Start()
    {
        
        StartCoroutine("Moving");
      
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minValue, _maxValue), -3.38f, 0);
    }
    public IEnumerator Moving()
    {
        while(imMoving)
        { 
            _rigid.linearVelocity = Vector2.zero;
            for (; transform.position.x < _maxValue;) 
            {
                _rigid.AddForce(Vector2.right * _pwoer, ForceMode2D.Impulse);
                _rigid.linearVelocityX = Mathf.Clamp(_rigid.linearVelocityX, -7.5f, 7f);
                yield return null;
                if (transform.position.x == _maxValue || !imMoving)
                    break;
            }
            _rigid.linearVelocity = Vector2.zero;
            for (; transform.position.x > _minValue;)
            {
                _rigid.AddForce(Vector2.left * _pwoer, ForceMode2D.Impulse);
                _rigid.linearVelocityX = Mathf.Clamp(_rigid.linearVelocityX, -7.5f, 7f);
                yield return null;
                if (transform.position.x == _minValue || !imMoving)
                    break;
            }

                
            _rigid.linearVelocity = Vector2.zero;
        } 
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
            imMoving = true;
            _text.SetActive(false);
            StartCoroutine(Moving());
         }
        else
        {
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(TimeSystem());
        }

    }


    private void OnMouseDown()
    {
      if (imMoving && _playerSkill.onSkill && TimeSkillUIManager.Inctance.sec > 0)
        {
            StopCoroutine("Moving");
            imMoving = false;
            _rigid.linearVelocity = Vector2.zero;
            Stoping(0,TimeSkillUIManager.Inctance.sec);
            _text.SetActive(true);
            _rigid.linearVelocity = Vector2.zero;
        }
        
    }
    private void OnMouseEnter()
    {
        if (_playerSkill.onSkill)
        {
            for (int i = 0; i < _spriteRenderer.Length; i++)
            {
                _spriteRenderer[i].color = Color.yellow;
            }
        }
    }
       
    private void OnMouseExit()
    {
        for (int i = 0; i < _spriteRenderer.Length; i++)
        {
            _spriteRenderer[i].color = Color.white;
        }
    }

    public void Stoping(int minute, int sec)
   {
        _rigid.linearVelocity = Vector2.zero;
        this.sec = sec;
        imMoving = false;
        _rigid.linearVelocity = Vector2.zero;
        StartCoroutine(TimeSystem());
        _rigid.linearVelocity = Vector2.zero;

    }
}
