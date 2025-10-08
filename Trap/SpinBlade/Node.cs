using UnityEngine;

public class Node : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField] private float moveSpeed = 2.5f;
  //  [SerializeField] private GameObject _particl;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        _rigid.linearVelocity = new Vector2(0, moveSpeed);
    }

    private void OnDisable()
    {
        _rigid.linearVelocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            NodeManager.Instance._nodes.Push(gameObject);
            SpinBladeAniManager.Inctance.DownSpeed(-0.05f);
            gameObject.SetActive(false);
            
        }
    }

}
