using System.Collections;
using UnityEngine;

public class giragiraArrow : MonoBehaviour
{

    public bool GiraOn { get; set; } = false;
    private SpriteRenderer _sprite;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    public IEnumerator GiraArrow()
   {
        float giraTime = 0.5f;
        for (; GiraOn;)
        {
            _sprite.enabled = true;
            yield return new WaitForSeconds(giraTime);
            _sprite.enabled = false;
            yield return new WaitForSeconds(giraTime);
        }
        _sprite.enabled = false;
    }
}


