using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //引用音頻apI
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //定義欄位(宣告變數)
    //類型 名稱 結尾
    //public 公開、private 私人
    public AudioMixer mixer;

    public Text loadingText;
    public Slider loading;

    //定義方法 (宣告函式)
    //修飾詞 類型 名稱 (參數) { 敘述 }
    public void SetVBGM(float v)
    {
        mixer.SetFloat("VBGM", v);
    }

    public void SetVSFX(float value)
    {
        mixer.SetFloat("VSFX", value);
    }

    public void Play()
    {
        // SceneManager.LoadScene("場景");
        StartCoroutine(Loading());
    }

    // 協同程序 Coroutine
    private IEnumerator Loading()
    {
        //print("測試 1");
        //yield return new WaitForSeconds(1);     // 等待秒數(秒數);
        //print("測試 2");

        AsyncOperation ao = SceneManager.LoadSceneAsync("play");    // 取得場景資訊
        ao.allowSceneActivation = false;                            // 取消載入場景
                                                                    // 當 (場景載入 == 未完成)
        while (ao.isDone == false)
        {
            // 更新介面並等待
            loadingText.text = ((ao.progress / 0.9f) * 100).ToString();                  // 更新文字 = 載入.進度
            loading.value = ao.progress / 0.9f;
            yield return new WaitForSeconds(0.0001f);                   // 等待秒數(秒數)

            //如果 仔入進度==0.9並且 按下任意鍵
            if (ao.progress == 0.9f && Input.anyKey)
            {
                ao.allowSceneActivation = true;
            }
        }

    }

    public void Replay()
    {
        SceneManager.LoadScene("選單");
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

    

    
