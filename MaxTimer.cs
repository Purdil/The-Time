using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaxTimer : MonoBehaviour
{
    public int EmaSec;
    public int MaxSec;

    public static MaxTimer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (EmaSec > MaxSec)
                MaxSec = EmaSec;
            
            
            EmaSec = 0;
            FindAnyObjectByType<maxTime>().gameObject.GetComponent<TextMeshProUGUI>().text = $"당신의 최고 시간은 {MaxSec / 60}:{MaxSec % 60}";
        }
    }

    public void AddMaxSec()
    {
        if (SceneManager.GetActiveScene().buildIndex != 7)
            EmaSec += FindAnyObjectByType<PlayerTime>().sec;
    }
}
