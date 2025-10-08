using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinBladeUI : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] public GameObject _SpinBladeUI;

    private Animator _selfAnimator;
    private PlayerMove _playerMoveScript;
    private NodeSongManager _SongManager;
    private GameObject _text;
  
    public bool Crear { get; set; } = false;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>().gameObject;
        _SongManager = _SpinBladeUI.GetComponent<NodeSongManager>();
        _playerMoveScript = GameObject.FindAnyObjectByType<PlayerMove>().GetComponent<PlayerMove>();
        _selfAnimator = GetComponent<Animator>();
    }
    public void CrearBlade()
    {
        Crear = true;
        _SpinBladeUI.SetActive(false);
        _playerMoveScript.CanMove = true;
        _selfAnimator.speed = 0;
        NodeManager.Instance.OffNode();
    }



    private void Update()
    {
        if (TextRay() && !Crear)
        {
            _text.SetActive(true);
        }
        else
        {
            _text.SetActive(false);
        }

        if (Keyboard.current.fKey.wasPressedThisFrame && TextRay() && !Crear) 
        {
            _playerMoveScript.CanMove = false;
            _SpinBladeUI.SetActive(true);
            _SongManager.enabled = true;
        }
        if (!TextRay() && _SpinBladeUI.activeSelf || Keyboard.current.escapeKey.wasPressedThisFrame && _SpinBladeUI.activeSelf)
        {
            _playerMoveScript.CanMove = true;
            StartCoroutine(NodeManager.Instance.OffNode());
            _SpinBladeUI.SetActive(false);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime player))
        {
            if (!Crear)
            {
                player.GetDamage(player.sec, gameObject.name);
            }
        }
    }









    public bool TextRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(3, 5), 0, transform.position, 0, _playerLayer);
        return ray;
    }

}
