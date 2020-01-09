using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 暫停 : MonoBehaviour
{
    [Header("暫停介面")]
    public Image  imagePause;
    public Sprite spritePause, spritePlay;
    bool pause;

    /// <summary>
    /// 暫停方法
    /// </summary>
	// Use this for initialization
	private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("暫停~");
            pause = !pause;     // ! 相反：將布林值改為相反

            // 條件運算子
            // 布林值 ? 結果一 : 結果二
            // 布林值 true 會執行結果一
            // 布林值 salse 會執行結果二

            imagePause.sprite = pause ? spritePlay : spritePause;

            Time.timeScale = pause ? 0 : 1;
        }


        }
    //更新:一秒執行約60次
    private void Update()
    {
        Pause();
    }
}

