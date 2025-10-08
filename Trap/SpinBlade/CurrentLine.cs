
using UnityEngine;
using UnityEngine.InputSystem;

public class CurrentLine : MonoBehaviour
{
    [SerializeField] private string key;


 
    private bool _ontuch = false;

    private void Update()
    {
        PanGongOverlap();
    }



    public void PanGongOverlap()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(transform.position, new Vector2(17.49f, 20f) / 40, 0);
        foreach (Collider2D col in collider)
        {
            if (col.gameObject.CompareTag("Node") && !_ontuch)
            {

                CheckKey(key, col);
            }
                
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
      Gizmos.DrawCube(transform.position, new Vector3(17.49f, 20f, 0) / 40);
    }
    private void CheckKey(string key,Collider2D collider)
    {
        GameObject node =  collider.gameObject;
      
        if (Keyboard.current.aKey.wasPressedThisFrame && key == "A")
        {
            _ontuch = true;
            CheckPanGoung(node);
        }
 
      
        if (Keyboard.current.sKey.wasPressedThisFrame && key == "S")
        {
            _ontuch = true;
            CheckPanGoung(node);
        }
         

        if (Keyboard.current.semicolonKey.wasPressedThisFrame && key == ";")
        {
             _ontuch = true;
             CheckPanGoung(node);
        }
    
       
        if (Keyboard.current.quoteKey.wasPressedThisFrame && key == "'")
        {
              _ontuch = true;
              CheckPanGoung(node);
        }
         
        
    }
                
            





    private void CheckPanGoung(GameObject node)
    {
        node.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            _ontuch = false;
        if (node.transform.localPosition.y < -29.3f && node.transform.localPosition.y > -32.5f)
        {
          //  Debug.Log("¹è¸® ±Â");
            SpinBladeAniManager.Inctance.DownSpeed(0.035f);
            node.SetActive(false);
           

            NodeManager.Instance._nodes.Push(node);
        }
        else if (node.transform.localPosition.y < -27.5f && node.transform.localPosition.y > -36f)
        {
          //  Debug.Log("±Â");
            SpinBladeAniManager.Inctance.DownSpeed(0.01f);
            node.SetActive(false);
            NodeManager.Instance._nodes.Push(node);
      
        }
        else
        {
            SpinBladeAniManager.Inctance.DownSpeed(-0.02f);
            node.SetActive(false);
            NodeManager.Instance._nodes.Push(node);
        }
    }

   



   


}
