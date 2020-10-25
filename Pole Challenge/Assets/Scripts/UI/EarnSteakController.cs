using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarnSteakController : MonoBehaviour
{
    public Button CloseButton;
    public TMP_Text EarnedText;
    public Image ShineBackground;

    private float angle = 0;

    private readonly int[] countRanges = { 1, 10 };  
    private void Start()
    {
        CloseButton.onClick.AddListener(CloseUI);

        int count = Random.Range(countRanges[0], countRanges[1] + 1);
        ProfileManager.instance.callBackOnSteakChanged.Invoke(count);
        EarnedText.text = "+  " + count + "  x";
    }

    private void Update()
    {
        ShineBackground.transform.localEulerAngles = new Vector3(0, 0, angle);
        angle += Time.deltaTime * 15;
    }

    private void CloseUI()
    {
        gameObject.SetActive(false);
    }


}
