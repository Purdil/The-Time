using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTime : MonoBehaviour,IDamageable
{
    [SerializeField] private int sec = 240;

    private int maxSec;
    private SpriteRenderer _sprite;
    private PlayerTime _playerTime;
    private TextMeshPro _timeText;

    private bool ImDie = false;
    private void Awake()
    {
        maxSec = sec;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _playerTime = FindAnyObjectByType<PlayerTime>();
        _timeText = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        _timeText.text = $"{sec / 60}:{sec % 60}";
        if (sec <= 0 && !ImDie)
        {
            StartCoroutine(EnemyDie());
        }
    }


    private void LateUpdate()
    {
        sec = Mathf.Clamp(sec, 0, sec);
    }
    public void GetDamage(int damage, string target)
    {
          if(!ImDie)
             sec -= damage;

    }
        

    private IEnumerator EnemyDie()
    {
        ImDie = true;
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            yield return new WaitForSeconds(0.005f);
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, i);
        }
        _playerTime.GetDamage(-(maxSec / 2), gameObject.name);
        EnemyKillCount.Instance.Count++;
        Destroy(gameObject);
    }
        


}
