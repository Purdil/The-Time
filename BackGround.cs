using UnityEngine;

public class BackGround : MonoBehaviour
{
   public void OnBackGround()
    {
        gameObject.SetActive(true);
    }

    public void OffBackGround()
    {
        gameObject.SetActive(false);
    }
}
