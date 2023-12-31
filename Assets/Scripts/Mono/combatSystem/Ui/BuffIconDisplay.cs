using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Image Icon, TimeBar;
    float refreshTime = 0.1f;
    WaitForSeconds thing;
    private void Awake()
    {
        thing = new WaitForSeconds(refreshTime);
    }
    public void Set(DisplayInfo info)
    {
        Icon.sprite = info.icon;
        float amount = refreshTime / info.totalTime;
        float startPersent = info.nowTime / info.totalTime;
        text.text = info.buffNumber.ToString();
        StartCoroutine(adding(amount, startPersent));
    }
    IEnumerator adding(float increment,float current)
    {
        TimeBar.fillAmount = 1-current;
        yield return thing;
        while (current < 1)
        {
            current += increment;
            TimeBar.fillAmount=1-current;
            yield return thing;
        }
    }
    public struct DisplayInfo
    {
        public float totalTime;
        public float nowTime;
        public int buffNumber;
        public Sprite icon;
    }
}
