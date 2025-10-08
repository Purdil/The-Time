using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private GameObject _blackBackGround;
    [SerializeField] private TopPillar[] _topPillar;
    [SerializeField] private GameObject _timer;

    private PlayerTime _playerTime;
    public bool onSkill { get; private set; } = false;
    public bool CanSkil { get; set; } = true;

    private void Awake()
    {
        _playerTime = GetComponent<PlayerTime>();
    }
    private void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame && !onSkill && !_playerTime.ImDead && CanSkil)
        {
            StartCoroutine(OnSkill());
            
        }

    }
    public IEnumerator OnSkill()
    {
        float stopTime = 0;
        float stop = 0.11f;
        Time.timeScale = 0.01f;
        _blackBackGround.SetActive(true);
        _timer.SetActive(true);
        onSkill = true;
        yield return null;
        while (true)
        {
            
            stopTime += Time.deltaTime;
            if (stopTime > stop)
            {
                onSkill = false;
                OffSkill();
                break;
            }
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                onSkill = false;
                OffSkill();
                break;
            }
            yield return null;
        }
        
    }
    public void OffSkill()
    {
        Time.timeScale = 1f;
        _blackBackGround.SetActive(false);
        _timer.SetActive(false);
        onSkill =false;
    }
}
