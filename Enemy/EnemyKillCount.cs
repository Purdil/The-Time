using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyKillCount : MonoBehaviour
{
    public int Count;
    private bool reset = true;
    public static EnemyKillCount Instance;

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
    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6 && reset)
        {
            Count = 0;
            reset = false;
        }
        else if(!reset)
        {
            reset = true;
        }
    }

}
