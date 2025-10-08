using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _poTan;
    [SerializeField] private string direction;
    [SerializeField] private float power;
    private Transform _fierForce;
    private void Awake()
    {
        _fierForce = GetComponentInChildren<Transform>();
    }
    public void ShotPoTan()
    {
        GameObject Po = Instantiate(_poTan, transform);
        Rigidbody2D pogid = Po.GetComponent<Rigidbody2D>();
        Po.transform.position = _fierForce.position;
        switch (direction)
        {
            case "위":
                pogid.AddForce(Vector2.up * power, ForceMode2D.Impulse);
                pogid.constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case "아래":
                pogid.AddForce(Vector2.down * power, ForceMode2D.Impulse);
                pogid.constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case "왼쪽":
                pogid.AddForce(Vector2.left * power, ForceMode2D.Impulse);
                pogid.constraints = RigidbodyConstraints2D.FreezePositionY;
                break;
            case "오른쪽":
                pogid.AddForce(Vector2.right * power, ForceMode2D.Impulse);
                pogid.constraints = RigidbodyConstraints2D.FreezePositionY;
                break;



        }


    }

}
