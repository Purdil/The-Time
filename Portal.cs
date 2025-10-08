using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private GameObject _nextSceneUI;
    private GameObject _text;
    private PlayerTime _playerTime;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>().gameObject;
        _text.SetActive(false);
        _playerTime = FindAnyObjectByType<PlayerTime>();
    }

    private void Update()
    {
        if (TextRay())
        {
            _text.SetActive(true);
            if (Keyboard.current.fKey.wasPressedThisFrame && !_playerTime.ImDead)
            {
                _playerTime.stopSec = true;
                FindAnyObjectByType<AudioSource>().Stop();
                FindAnyObjectByType<MaxTimer>().AddMaxSec();
                NextScene();
            }

        }
        else if(_text.activeSelf)
            _text.SetActive(false);
    }

    private void  NextScene()
    {
        _nextSceneUI.SetActive(true); 
    }
        

    private bool TextRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(1.5f, 2), 0, transform.position, 0, _playerLayer);
        return ray;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1.5f, 2));
    }

}
