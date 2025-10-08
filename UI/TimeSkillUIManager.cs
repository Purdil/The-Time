using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TimeSkillUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;
  
    public int sec { get; private set; } = 0;
    [SerializeField] private GameObject _Buttons;
    [SerializeField] private Animator _timerAni;
    public static TimeSkillUIManager Inctance;
    private void Awake()
    {
        if (Inctance == null) Inctance = this;
        else Destroy(gameObject);

    }
    private void Update()
    {
        _textTimer.text = $"{sec/60}:{sec%60}";
    }
        
        
            
        
    public void OnTimer()
    {
        if (_timerAni.GetBool("OnTimer") == false)
        {
            _timerAni.SetBool("OnTimer", true);
            _Buttons.SetActive(true);
        }
        else
        {
            _timerAni.SetBool("OnTimer", false);
            _Buttons.SetActive(false);
        }


    }
    public void UpMin()
    {
        sec += 60;
    }
    public void DownMin()
    {
        if (sec - 60 >= 0) sec -= 60;
        else sec = 0;
    }
    public void UpSec()
    {
        sec++;
    }
    public void DownSec()
    {
        if (sec - 1 >= 0) sec--;
        else sec = 0;
    }



    


}
