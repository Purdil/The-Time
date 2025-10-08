using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pilgrim_ : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _leftPosition;
    [SerializeField] private GameObject _rightPosition;
 
    private TextMeshPro _text;
    private SpriteRenderer _sprite;
    private ParticleSystem[] _particls;
    private GameObject _rightPar;
    private GameObject _leftPar;
    private CinemachineCamera _cinemachineCam;
    private BackGround _backGround;
    private PlayerMove _playerMove;
    private PlayerAni _playerAni;
    private BollScript _bollScript;
    private PlayerSkill _playerSkill;

    private bool _textOn = false;
    private bool startKill = false;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        _text.gameObject.SetActive(false);

        _sprite = GetComponent<SpriteRenderer>();

        _particls = GetComponentsInChildren<ParticleSystem>();

        _rightPar = _particls[0].gameObject;
        _rightPar.SetActive(false);

        _leftPar = _particls[1].gameObject;
        _leftPar.SetActive(false);

        _cinemachineCam = FindAnyObjectByType<CinemachineCamera>();

        _backGround = FindAnyObjectByType<BackGround>();
        _backGround.gameObject.SetActive(false);

        _playerMove = FindAnyObjectByType<PlayerMove>();
        _playerAni = FindAnyObjectByType<PlayerAni>();

        _bollScript = FindAnyObjectByType<BollScript>();
        _playerSkill = _playerMove.gameObject.GetComponent<PlayerSkill>();
    }

    private void Update()
    {
        if (OnTextRay())
            _text.gameObject.SetActive(true);
        else
            _text.gameObject.SetActive(false);
        
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            PlayerPosition();
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime player) && OnTextRay())
        {
            StartCoroutine(OnKillCutScene(player));
        }
       

    }

    private bool OnTextRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(1.55f, 1.5f), 0, transform.position, 0, _playerLayer);
        return ray;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
       // Gizmos.DrawCube(transform.position, new Vector3(1.55f, 1.5f));
    }

    private IEnumerator OnKillCutScene(PlayerTime playerTime)
    {
     
      
        for (; OnTextRay() ;)
        {
            yield return null;
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                StartCoroutine(KillAni());
                //닿을랑 말랑 한 시점에서 슬로우 + 카메라 줌인 + 이펙트 활성화 2초 뒤 원상 복구
                break;
            }
        }
    }

    private IEnumerator KillAni()
    {
        startKill = true;
        _playerMove.CanMove = false;
        _sprite.color = Color.black;
        _playerAni.CanAni = false;
        _bollScript.CanAtt = false;
        _playerSkill.CanSkil = false;
        _backGround.gameObject.SetActive(true);
        PlayerPosition();
        yield return null;
    }

    private void PlayerPosition()
    {
        if (transform.position.x - _playerMove.gameObject.transform.position.x > 0)
        {
            _playerMove.gameObject.transform.position = _leftPosition.transform.position;
            _playerAni.gameObject.transform.localScale = new Vector3(1, 1, 1);
            //왼쪽
        }
        else if (transform.position.x - _playerMove.gameObject.transform.position.x < 0)
        {
            _playerMove.gameObject.transform.position = _rightPosition.transform.position;
            _playerAni.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            //오른쪽
        }
    }
        



}
