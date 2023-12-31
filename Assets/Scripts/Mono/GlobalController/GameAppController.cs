using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//this class sets the frame rate, graphic, and windows resolution
public class GameAppController : MonoBehaviour
{
    public enum frameRate
    {
        Thirty = 30, Sixty = 60, OneTwenty = 120,
    }
    public void ChangeFrameRate(frameRate rate)
    {
        Debug.Log("！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！");
        Application.targetFrameRate = (int)rate;
    }

    void Start()
    {
        //SetResizeavkeWindow();
        ChangeScreenSize(Level.full);
    }
    //#if UNITY_WIN
    public enum Level
    {
        one, two, three, full
    }
    public void ChangeScreenSize(Level level)
    {
        if (level == Level.one)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else if (level == Level.two)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (level == Level.three)
        {
            Screen.SetResolution(2560, 1440, false);
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }
    public void setFixedUpdate(float f)
    {
      //  Time.fixedtime = 1;
    }
    public void SetResizeavkeWindow()
    {
        // PlayerSettings.resizableWindow=true;

    }

    //#endif

}
