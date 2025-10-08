using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mon : MonoBehaviour
{
    [SerializeField] private int currentSec;
    [SerializeField] private GameObject _Mon;

    private PlayerTime _playerTime;
    private MonUI _monUI;
    private Collider2D _collider;

    private int inputSec;
    public int sec { get; set; }
    public bool MonOpen { get; private set; } = false;

    private void Awake()
    {
        _monUI = GetComponent<MonUI>();
        _collider = GetComponent<Collider2D>();
        _playerTime = GameObject.FindAnyObjectByType<PlayerTime>().GetComponent<PlayerTime>();

    }

    private void Update()
    {
        if (_monUI.OnMonUI && !MonOpen && Keyboard.current.fKey.wasPressedThisFrame)
        {
            _monUI.OnOpenUI();
            StartCoroutine(Choise());
        }
        if (_monUI.OpenUIOn && Keyboard.current.escapeKey.wasPressedThisFrame || !_monUI.OnMonUI)
        {
            _monUI.OffOpenUI();
        }
        if (_monUI.OpenUIOn)
        {
            
        }

    }
    private IEnumerator Choise()
    {
        int banhang = 2;
        while (true)
        {
            yield return null;
            if (!_monUI.OpenUIOn)
            {
                break;
            }
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                banhang = 1;
                _monUI.ChoiseUIMove(banhang);
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                switch (banhang)
                {
                    
                    case 1:
                        sec += 60;
                        break;
                    case 2:
                        sec += 1;
                        break;
                }
                _monUI.ChoiseCountUI();
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                switch (banhang)
                {
                    case 1:
                        if(sec - 60 >= 0)
                            sec -= 60;
                        break;
                    case 2:
                        if(sec - 1 >= 0)
                            sec -= 1;
                        break;
                }
                _monUI.ChoiseCountUI();
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                banhang = 2;
                _monUI.ChoiseUIMove(banhang);
            }
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                inputSec = sec;
                cheackOpen();
            }
            
        }
    }

    private void cheackOpen()
    {
       if (currentSec == inputSec)
        {
            _playerTime.GetDamage(inputSec, "Mon");
            _collider.enabled = false;
            MonOpen = true;
            _Mon.SetActive(true);
            // 대충 문 여는 거
        }
       else
        {
            _playerTime.GetDamage(inputSec, "Mon");

        }

    }

}
