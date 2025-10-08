using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinBladeAniManager : MonoBehaviour
{
   [SerializeField] private Animator _bladeAni;
    public static SpinBladeAniManager Inctance;
    [SerializeField] private List<SpinBladeUI> Crear = new List<SpinBladeUI>();

    private int uiCount = 0;

    private void Awake()
    {
        if (Inctance == null)
        {
            Inctance = this;
        }
    }


    
   
    public float speed { get; set; } = 1f;
    

    private void Update()
    {
       speed = Mathf.Clamp(speed, 0, 1);
      if (Crear[uiCount] != null && !Crear[uiCount].Crear && NodeManager.Instance.CheackUIActive())
      {
            if (speed <= 0)
            {
           
                _bladeAni.speed = 0;
                Crear[uiCount].CrearBlade();
                speed = 0;
                uiCount++;
          
            }

            else
            {
                _bladeAni.speed = speed;
            }
      }
    }
    



    public void DownSpeed(float value)
    {
        if (!Crear[uiCount].Crear)
        {
            speed -= value;
            speed = Mathf.Clamp(speed, 0, 1);
        }
    }
}
