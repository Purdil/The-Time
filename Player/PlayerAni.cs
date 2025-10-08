using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAni : MonoBehaviour
{
  
    private Animator _ani;
    public bool CanAni { get; set; } = true; 
    private Vector3 _origneScale;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _origneScale = transform.localScale;
    }
    private void Update()
    {
       if ((Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed) && CanAni) _ani.SetFloat("Blend", 1);
       else _ani.SetFloat("Blend", 0);
        if (Keyboard.current.aKey.isPressed && CanAni) transform.localScale = new Vector3(-_origneScale.x, _origneScale.y, _origneScale.z);
        else if (Keyboard.current.dKey.isPressed && CanAni) transform.localScale = _origneScale;
    }

 
   
}
