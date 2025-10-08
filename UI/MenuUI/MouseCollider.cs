using UnityEngine;

public class MouseCollider : MonoBehaviour
{

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = mousePos;    
    }
}
