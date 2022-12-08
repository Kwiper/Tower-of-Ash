using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text1;

    [SerializeField]
    TextMeshProUGUI text2;

    [SerializeField]
    TextMeshProUGUI text3;

    [SerializeField]
    TextMeshProUGUI text4;

    [SerializeField]
    TextMeshProUGUI text5;

    [SerializeField]
    int sceneToLoad = -1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(2f);
        text1.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
        text1.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        text2.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
        text2.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        text3.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
        text3.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        text4.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);
        text4.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        text5.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);
        text5.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

    }


}
