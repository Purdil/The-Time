using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private GameObject _nodePrePab;
    [SerializeField] private Transform[] _nodePostition;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _SpinBladeUI;

    public Stack<GameObject> _nodes = new Stack<GameObject>();
    private NodeSongManager _songManager;
  

    private int nodeCount = 30;
    
    public static NodeManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        for (int i = 0; i < nodeCount; i++)
        {
            CreateNode();
        }
        _songManager = _SpinBladeUI.GetComponent<NodeSongManager>();
   
    }

    public bool CheackUIActive()
    {
        return _SpinBladeUI.activeSelf;
    }

    public void CreateNode()
    {
        GameObject node = Instantiate(_nodePrePab, _parent);
       _nodes.Push(node);
        node.SetActive(false);
    }


    public IEnumerator OffNode()
    {

       
            NodeSongManager.Inctance.StopSong();
            for (; GameObject.FindAnyObjectByType<Node>()?.gameObject != null;)
            {
                GameObject.FindAnyObjectByType<Node>().gameObject.SetActive(false);

            }
            _SpinBladeUI.SetActive(false);
            _songManager.enabled = false;
            Debug.Log("³ª ÀÛµ¿");
        


        yield return null;
    }

    public void OnNode(int nodeNum)
    {
        if (_nodes.Count != 0)
        {
            GameObject node = _nodes.Pop();
            switch (nodeNum)
            {
                case 1:
                    node.transform.localPosition = _nodePostition[0].localPosition;
                    break;
                case 2:
                    node.transform.localPosition = _nodePostition[1].localPosition;
                    break;
                case 3:
                    node.transform.localPosition = _nodePostition[2].localPosition;
                    break;
                case 4:
                    node.transform.localPosition = _nodePostition[3].localPosition;
                    break;
            }
            node.SetActive(true);
         
        }
        else
        {
            CreateNode();
        }
    }
       
                
           

        


}
