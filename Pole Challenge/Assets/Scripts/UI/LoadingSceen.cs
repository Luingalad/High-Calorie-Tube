using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceen : MonoBehaviour
{
    public Image LoadingIcon;
    public CanvasGroup canvasGroup;

    private float _time;
    private float timeOut = 25f;

    #region singleton
    public static LoadingSceen instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ang = LoadingIcon.transform.localEulerAngles;
        LoadingIcon.transform.localEulerAngles = new Vector3(ang.x, ang.y, ang.z + 360 * Time.deltaTime);

        if(_time > timeOut)
        {
            CloseLoadingScene();
        }
        _time += Time.deltaTime;
    }

    public void CloseLoadingScene()
    {
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        for(int i = 0; i < 25; i++)
        {
            canvasGroup.alpha -= 0.04f;
            yield return null;
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
