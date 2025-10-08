using UnityEngine;

public class SetSkillTimeAndUI : MonoBehaviour
{
    public int minute = 1;
    public int sec = 20;
    private TopPillar _topPillar;
    public void StartStoping()
    {
       _topPillar =  gameObject.GetComponentInParent<GameObject>().GetComponentInParent<GameObject>().GetComponent<TopPillar>();
        _topPillar.Stoping(minute, sec);
       
    }
}
