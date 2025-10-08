using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private int nextSceneNum;
   
   

    private void Start()
    {
        StartCoroutine(PanelrCH());
        StartCoroutine(TextCH());
    }

    private IEnumerator SizeUP()
    {
        for (int i = 0; i <= 200; i++)
        {
            _mainText.fontSize = i;
            yield return new WaitForSeconds(0.03f);
        }
    }
    private IEnumerator TextCH()
    {
        float delayTime = 0.03f;

       // StartCoroutine(SizeUP());
        for (float i = 0; i < 1; i += 0.01f)
        {
            _mainText.color = new Color(_mainText.color.r, _mainText.color.g, _mainText.color.b, i);
            yield return new WaitForSeconds(delayTime);
        }

        yield return new WaitForSeconds(1f);


        for (float i = 1; i > 0; i -= 0.01f)
        {
            _mainText.color = new Color(i, i, i, _mainText.color.a);

            yield return new WaitForSeconds(delayTime);
        }
    }
    private IEnumerator PanelrCH()
    {
        float delayTime = 0.03f;


        for (float i = 0; i < 1; i += 0.01f)
        {
           
            _panel.color = new Color(_panel.color.r, _panel.color.g, _panel.color.b, i);
            yield return new WaitForSeconds(delayTime);
        }

        yield return new WaitForSeconds(1f);


        for (float i = 0; i < 1; i += 0.01f)
        {
            _panel.color = new Color(i, i, i, _panel.color.a);

            yield return new WaitForSeconds(delayTime);
        }

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(nextSceneNum);
    }
}
