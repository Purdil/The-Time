using System.Collections;
using TMPro;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    
    private int slot1;
    private int slot2;
    private int slot3;
    private bool _setSlot1;
    private bool _setSlot2;
    private bool _setSlot3;
    private bool _onSlotUI = false;

    private SpriteRenderer _spriteRen;
    private PlayerTime _playerTime;
    private PlayerSkill _playerSkill;
    private GameObject _Kotoba;
    private TextMeshPro _slot1;
    private TextMeshPro _slot2;
    private TextMeshPro _slot3;


    private void Awake()
    {
        _Kotoba = transform.GetChild(0).GetComponent<TextMeshPro>().gameObject;
       _slot1 = transform.GetChild(1).GetComponent<TextMeshPro>();
       _slot2 = transform.GetChild(2).GetComponent<TextMeshPro>();
        _slot3 = transform.GetChild(3).GetComponent<TextMeshPro>();
        _playerSkill = GameObject.FindAnyObjectByType<PlayerSkill>();
        _playerTime = GameObject.FindAnyObjectByType<PlayerTime>();
        _spriteRen = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        KoToBaActive();
    }

    private void OnMouseDown()
    {
        if (_playerSkill.onSkill && TextRay())
        {
           StartCoroutine(OnSlotUI());
        }
    }
    private void OnMouseEnter()
    {
        if (_playerSkill.onSkill)
        {
            _spriteRen.color = Color.yellow;
        }
    }
    private void OnMouseExit()
    {
       _spriteRen.color = Color.white;
    }
    private void KoToBaActive()
    {
        if (TextRay() &&  !_onSlotUI)
            _Kotoba.SetActive(true);
        else
            _Kotoba.SetActive(false);
    }
    
    private IEnumerator OnSlotUI()
    {
        int sec = TimeSkillUIManager.Inctance.sec;
        _playerTime.GetDamage(sec, gameObject.name);
        StartCoroutine(Slot1());
        StartCoroutine(Slot2());
        StartCoroutine(Slot3());
        for (; !_setSlot1 && !_setSlot2 && !_setSlot3;)
        {
            yield return null;
        }
        yield return null;
        _setSlot1 = false;
        _setSlot2 = false;
        _setSlot3 = false;
        if (slot1 == slot2 && slot1 == slot3)
        { 
            // 성공 사운드
            _playerTime.GetDamage(-(sec * 3), gameObject.name);
        }
        else
        {
            // 실패 사운드
        }


    }
      
    private IEnumerator Slot1()
    {
        for (float i = 0; i < 0.3f; i += 0.007f)
        {
            slot1 = Random.Range(1, 10);
            _slot1.text = slot1.ToString();
            yield return new WaitForSeconds(i);
        }
        _setSlot1 = true;
    }
    private IEnumerator Slot2()
    {
        for (float i = 0; i < 0.3f; i += 0.007f)
        {
            slot2 = Random.Range(1, 10);
            _slot2.text = slot2.ToString();
            yield return new WaitForSeconds(i);
        }
        _setSlot2 = true;    }
    private IEnumerator Slot3()
    {
        for (float i = 0; i < 0.3f; i += 0.007f)
        {
            slot3 = Random.Range(1, 10);
            _slot3.text = slot3.ToString();
            yield return new WaitForSeconds(i);
        }
        _setSlot3 = true;
    }






    public bool TextRay()
    {
        RaycastHit2D ray = Physics2D.BoxCast(transform.position, new Vector2(3f, 2), 0, transform.position, 0, _playerLayer);
        return ray;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
      //  Gizmos.DrawCube(transform.position, new Vector2(2.5f, 2));
    }

}
