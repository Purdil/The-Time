using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAni : MonoBehaviour
{
    public Animator _ani;
    private PlayerTime _playerTime;
    private EnemyState _enemyState;
    private float _aniSpeed;
    private CinemachineImpulseSource _impulseSource;
    private Transform _parentTran;

    private int damage = 60;
    public bool WallkAni { get; set; } = false;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _playerTime = FindAnyObjectByType<PlayerTime>();
        _enemyState = GetComponentInParent<EnemyState>();
        _aniSpeed = _ani.speed;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _parentTran = _enemyState.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        if (WallkAni)
        {
            _ani.SetFloat("Blend", 1);
        }
        else if(_ani.GetFloat("Blend") == 1)
        {
            _ani.SetFloat("Blend", 0);
        }
    }
    // 타이밍에 맞춰 5초의 시간을 넣으면 애니메이션 멈추고 시간 지나면 다시 실행되서 공격 함.

    public void StopAni()
    {
        _ani.speed = 0;
    }

    public void StartAni()
    {
        _ani.speed = _aniSpeed;
    }
    public void Att()
    {
        int dir = 0;
        if (_parentTran.localScale.x > 0)
        {
            dir = _enemyState.OverlepBoxRight() ? 1 : 0;
        }
        else if (_parentTran.localScale.x < 0)
        {
            dir = _enemyState.OverlepBoxLeft() ? 2 : 0;        }
        if (dir == 1 ||  dir == 2)
        {
            _playerTime.GetDamage(damage, gameObject.name);
            _impulseSource.GenerateImpulse();
        }

    }
    public void CanStopAttOn()
    {
        _enemyState.stopAtt = true;
    }

    public void CanStopAttOff()
    {
        _enemyState.stopAtt = false;
    }
    public void OnAttFalse()
    {
        _enemyState.OnAtt = false;
    }


}
