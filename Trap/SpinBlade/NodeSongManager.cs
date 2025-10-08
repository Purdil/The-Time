using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSongManager : MonoBehaviour
{
    public static NodeSongManager Inctance;

    private void Awake()
    {
        if (Inctance == null)
        {
            Inctance = this;
        }
    }

    private void OnEnable()
    {
        StartCoroutine("TestSong");
    }
    private void OnDisable()
    {
        StopCoroutine(TestSong());
        List<GameObject> List = new List<GameObject>();
        for (; GameObject.FindAnyObjectByType<Node>();)
        {
            List.Add(GameObject.FindAnyObjectByType<Node>().gameObject);
        }
        foreach (GameObject node in List)
        {
            node.SetActive(false);
        }
    }

    public void StopSong()
    {
        StopCoroutine("TestSong");
    }


    private IEnumerator TestSong()
    {
        while (true)
        {
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(Random.Range(0f, 0.4f));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            NodeManager.Instance.OnNode(Random.Range(1, 5));
            yield return new WaitForSeconds(0.5f);

        }
    }




}
