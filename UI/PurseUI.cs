using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PurseUI : MonoBehaviour
{
    [SerializeField] private GameObject _stopUI;
    [SerializeField] private GameObject _helpUI;

    private PlayerTime _playerTime;
    private void Awake()
    {
        _playerTime = FindAnyObjectByType<PlayerTime>();
    }

    public bool CanStopUIOn { get; set; } = true;
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && CanStopUIOn && !_stopUI.activeSelf && !_playerTime.ImDead)
        {
            StartCoroutine(OnStopUI());
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame && _helpUI.activeSelf)
        {
            _helpUI.SetActive(false);
        }
    }

    private IEnumerator OnStopUI()
    {
        Time.timeScale = 0;
        _stopUI.SetActive(true);
        for (; true;)
        {
            yield return null;
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                OffStopUI();
                break;
            }
        }

    }

    public void OffStopUI()
    {
        Time.timeScale = 1;
        _stopUI.SetActive(false);
    }

    public void HelpUI()
    {
        _helpUI.SetActive(true);
    }

   

}
