using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private LayerMask _Mouse;
    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _Start;
    [SerializeField] private GameObject _Exit;
    [SerializeField] private GameObject _Tuto;
    private void Update()
    {
        if (OverLepBox())
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
    public void StartButton()
    {
        _Exit.SetActive(false);
        _Tuto.SetActive(false);
        StartCoroutine(FideAndLoadScene(2));
    }
    public void RhythmButton()
    {
        _Exit.SetActive(false);
        _Start.SetActive(false);
        StartCoroutine(FideAndLoadScene(7));
    }

    public void ExitButton()
    {
        _Start.SetActive(false);
        _Tuto.SetActive(false);
        Application.Quit();
    }

    private IEnumerator FideAndLoadScene(int sceneNumber)
    {
        _panel.gameObject.SetActive(true);
       for (float i = 0; i <= 1; i += 0.01f)
        {
            _panel.color = new Color(_panel.color.r, _panel.color.g, _panel.color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneNumber);
    }

    public bool OverLepBox()
    {
        Collider2D overLap = Physics2D.OverlapBox(transform.position, new Vector2(3, 2),0,_Mouse);
        return overLap;

    }
  
}
