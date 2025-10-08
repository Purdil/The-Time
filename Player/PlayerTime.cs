using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerTime : MonoBehaviour , IDamageable
{
    [SerializeField] private TextMeshProUGUI _timeTXT;
    [SerializeField] private GameObject _hitPar;
    [SerializeField] private GameObject _hitPar2;
    [SerializeField] private GameObject _gameOverUI;

    private SpriteRenderer _sprite;
    private PlayerMove _playerMove;
    private PlayerAni _playerAni;
    public bool stopSec { get; set; } = false;

    [SerializeField] private int settingSec;
 
   public int sec { get; private set; } 
    public bool ImDead { get; set; } = false;
  
    IEnumerator timeCoroutine;
    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _playerAni = GetComponentInChildren<PlayerAni>();
        sec = settingSec;
    }



    private void Start()
    {
        timeCoroutine = TimeSystem();
        StartCoroutine(TimeSystem());
    }


    private void Update()
    {
        _timeTXT.text = $"{sec/60}:{sec%60}";
    }
    private IEnumerator TimeSystem()
    {
        for (;sec > 0;)
        {
            yield return new WaitForSeconds(0.1f);
            if(!stopSec)
               sec -= 1;
        }
        if (sec <= 0)
        {
            sec = 0;
            if (!ImDead)
                StartCoroutine(PlayerDie());
        }
        else
        {
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(TimeSystem());
        }

    }
        
      
       


    
    public void GetDamage(int damage, string target)
    {
        
       if (sec - damage > 0)
        {
            sec -= damage;
            StartCoroutine(OnHitPar());
        }
       else
        {
            sec = 0;
            if(!ImDead)
                StartCoroutine(PlayerDie());
        }
    }

    public IEnumerator PlayerDie()
    {
        ImDead = true;
        _playerMove.CanMove = false;
        _playerAni.CanAni = false;
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, i);
            yield return new WaitForSeconds(0.02f);
        }
        _gameOverUI.SetActive(true);
    }
    public IEnumerator OnHitPar()
    {
        if (!_hitPar.activeSelf)
        {
            _hitPar.SetActive(true);
            _hitPar2.SetActive(true);
            yield return new WaitForSeconds(1f);
            _hitPar2.SetActive(false);
            _hitPar.SetActive(false);
        }
    }


}
