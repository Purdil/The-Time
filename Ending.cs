using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject _Kill;
    [SerializeField] private GameObject _NoKill;
    private void Start()
    {
        if (EnemyKillCount.Instance.Count != 0)
        {
            _Kill.SetActive(true);
        }
        else
        {
            _NoKill.SetActive(true);
        }
    }
}
