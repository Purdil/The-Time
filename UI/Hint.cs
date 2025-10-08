using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject _hintUI;
    [SerializeField] private LayerMask _playerLayer;
    private GameObject _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>().gameObject;
        _text.SetActive(false);
    }

    private void Update()
    {
        if (TextRay())
        {
            _text.SetActive(true);
            if (Keyboard.current.fKey.wasPressedThisFrame && !_hintUI.activeSelf)
            {
                _hintUI.SetActive(true);
            }
        }
        else
        {
            _text.SetActive(false);
            _hintUI.SetActive(false);
        }
        if (_hintUI.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _hintUI.SetActive(false);
        }

    }

    private bool TextRay()
    {

        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, transform.position, 0, _playerLayer);
        return ray;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1));
    }

}
