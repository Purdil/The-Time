using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime player))
        {
            StartCoroutine(player.PlayerDie());
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
