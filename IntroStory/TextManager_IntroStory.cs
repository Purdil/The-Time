using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TextManager_IntroStory : MonoBehaviour
{
    public List<String> _texts = new List<String>();
    [SerializeField] private giragiraArrow _giraArrow;
    private TextMeshProUGUI _text;

    [SerializeField] private int SceneNumber;
    private int textCount = 0;
    private float delay = 0.1f;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        StartCoroutine(TextOn());
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneNumber);
        }
    }

    public IEnumerator TextOn()
    {
        if (_texts.Count != textCount)
        {
            for (int i = 0; i < _texts[textCount].Length; i++)
            {
                _text.text += _texts[textCount][i];
                yield return new WaitForSeconds(delay);
            }
            _giraArrow.GiraOn = true;
            StartCoroutine(_giraArrow.GiraArrow());
            while (true)
            {
                yield return null;
                if (Keyboard.current.anyKey.wasPressedThisFrame)
                {
                    _giraArrow.GiraOn = false;
                    _text.text = "";
                    textCount++;
                    StartCoroutine(TextOn());
                    break;
                }
            }
        }
        else
        {
            _text.text = "The End.";
            yield return new WaitForSeconds(1);
            for (float i = 1; i > 0; i -= 0.01f)
            {
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, i);
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(1.5f);
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }



    }


}
