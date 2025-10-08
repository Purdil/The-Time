using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class BollScript : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _combo1Impulse;
    [SerializeField] private CinemachineImpulseSource _combo2Impulse;
    [SerializeField] private CinemachineImpulseSource _combo3Impulse;

    private Animator _ani;
    private PlayerTime _playerTime;
    private PlayerSkill _playerSkill;
    
    public bool CanAtt { get; set; } = true;
    private bool startAtt = true;
    private bool combo1 = false;
    private bool combo2 = false;
    private bool combo3 = false;

 
    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _playerTime = FindAnyObjectByType<PlayerTime>();
        _playerSkill = _playerTime.gameObject.GetComponent<PlayerSkill>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && startAtt && !_playerTime.ImDead && CanAtt && !_playerSkill.onSkill)
        {
            StartCoroutine(Combo1());
            combo1 = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyTime>(out EnemyTime enemy))
        {
           
          
            if (combo1)
            {
                Debug.Log(combo1);
                enemy.GetDamage(20,gameObject.name);
                _combo1Impulse.GenerateImpulse();
            }
            else if (combo2)
            {
                Debug.Log(combo2);
                enemy.GetDamage(30, gameObject.name);
                _combo2Impulse.GenerateImpulse();
            }
            else if (combo3)
            {
                Debug.Log(combo3);
                enemy.GetDamage(3, gameObject.name);
                _combo3Impulse.GenerateImpulse();
            }


        }
    }

    private IEnumerator Combo1()
    {
        float time = 0;
        float end = 3;
        startAtt = false;
        _ani.SetInteger("Combo", 1);
        combo1 = true;
         for (; true;)
         {
            yield return null;
          
            if (!combo1)
            {
                yield return new WaitForSeconds(0.01f);
               for (; true;)
               {
                    time += 0.05f;
                    yield return null;
                    if (time >= end)
                    {
                        
                        ResetAni();
                        break;
                    }
                    else if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        StartCoroutine(Combo2());
                        break;
                    }
                    if (!CanAtt)
                    {
                        break;
                    }
               }
               break;
               
            }
         }
        
    }
    public void CheakCombo1()
    {
        combo1 = false;
    }
    public void CheakCombo2()
    {
        combo2 = false;
    }
    public void CheakCombo3()
    {
        combo3 = false;
    }
        

        


    private IEnumerator Combo2()
    {
        float time = 0;
        float end = 3;
        _ani.SetInteger("Combo", 2);
        combo2 = true;
        for (; true;)
        {
            yield return null;
            if (!combo2)
            {
          
                yield return new WaitForSeconds(0.01f);
                for (; true;)
                {
                    time += 0.05f;
                    yield return null;
                    if (time >= end)
                    {
                        ResetAni();
                        break;
                    }
                    else if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        StartCoroutine(Combo3());
                        break;
                    }
                    if (!CanAtt)
                    {
                        break;
                    }
                }
                break;

            }
        }

    }
    private IEnumerator Combo3()
    {
        combo3 = true;
        for (; true;)
        {
           yield return null;
          if (!combo2)
          {
             _ani.SetInteger("Combo", 3);
             break;
          }

        }
    }
       




    public void ResetAni()
    {
        _ani.SetInteger("Combo", 0);
        _ani.SetBool("Reset", true);
        startAtt = true;
    }
    public void FalseReset()
    {
        _ani.SetBool("Reset", false);
    }

    public void TimeSlow()
    {
        Time.timeScale = 0.1f;
    }
}
