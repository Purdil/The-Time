using System;
using System.Collections;
using UnityEngine;

public class LifeTimerAdd : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private bool moving = false;
    private float speed = 3;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(!moving)
           StartCoroutine(UpDown());
    }

    private IEnumerator UpDown()
    {
        moving = true;
        _rigid.AddForce(Vector2.up * speed, ForceMode2D.Force);
        yield return new WaitForSeconds(2);
        _rigid.linearVelocity = Vector2.zero;
        _rigid.AddForce(Vector2.down * speed, ForceMode2D.Force);
        yield return new WaitForSeconds(2);
        _rigid.linearVelocity = Vector2.zero;
        moving = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTime>(out PlayerTime player))
        {
            player.GetDamage(-180,gameObject.name);
            Destroy(gameObject);
        }
    }
}
