using System.Collections;
using TMPro;
using UnityEngine;

public class MonUI : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _OpenUI;
    [SerializeField] private GameObject _ChoiseUI;
    [SerializeField] private TextMeshProUGUI _min;
    [SerializeField] private TextMeshProUGUI _sec;

    private TextMeshPro _text;
    private Mon _mon; 

    public bool OpenUIOn { get; private set; }
    public bool OnMonUI { get; private set; }

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        _mon = GetComponent<Mon>();
    }

    private void Update()
    {
        if (!_mon.MonOpen && OnUIRay())
        {
            _text.enabled = true;
            OnMonUI = true;
        }
        else
        {
            _text.enabled = false;
            OnMonUI = false;
        }
        

    }
    public void OnOpenUI()
    {
        OpenUIOn = true;
        _OpenUI.SetActive(true);
        _ChoiseUI.SetActive(true);
    }

    public void OffOpenUI()
    {
        OpenUIOn = false;
        _OpenUI.SetActive(false);
        _ChoiseUI.SetActive(false);
    }

    public void ChoiseCountUI()
    {       
        _min.text = $"{_mon.sec / 60}";
        _sec.text = $"{_mon.sec % 60}";
    }

       
    public void ChoiseUIMove(int banghang)
    {
        switch (banghang)
        {
            case 1:
                _ChoiseUI.transform.localPosition = new Vector3(-200, 123);
                break;
            case 2:
                _ChoiseUI.transform.localPosition = new Vector3(232, 123);
                break;
            
        }

    }

    public bool OnUIRay()
    {
        RaycastHit2D uiRay = Physics2D.BoxCast(transform.position, new Vector2(3, 2), 0, transform.position, 0, _playerLayer);
        return uiRay;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
      //  Gizmos.DrawCube(transform.position, new Vector3(3, 2, 0));
    }


}
