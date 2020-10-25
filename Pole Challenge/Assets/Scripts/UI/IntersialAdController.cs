using UnityEngine;
using UnityEngine.SceneManagement;

public class IntersialAdController : MonoBehaviour
{
    #region Singleton
    public static IntersialAdController instance;
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
    public int AdScene;
    private int sceneToLoad;
    private bool adCalled;
        
    public void ShowIntersialAd(int Scene)
    {
        SceneManager.LoadScene(AdScene);
        sceneToLoad = Scene;
        AdMobController.instance.callBackOnInterstitalAdClosed += LoadScene;
        adCalled = true;
        AdMobController.instance.ShowInterstital();
    }

    private void LoadScene()
    {
        AdMobController.instance.callBackOnInterstitalAdClosed -= LoadScene;
        adCalled = false;
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Update()
    {
        #if UNITY_ANDROID || UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Escape) && adCalled )
        {
            SceneManager.LoadScene(sceneToLoad);
            adCalled = false;
        }
        #elif UNITY_IPHONE

        #endif
    }


}
