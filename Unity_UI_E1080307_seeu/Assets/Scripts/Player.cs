using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("血量與血條")]
    public float hp = 100;     //血量
    public Slider hpSlider;  //血量滑桿
    [Header("雞腿區域")]
    public Text textChicken; //雞腿文字介面
    public int chickencount; //雞腿數量
    public int chickenTotal; //雞腿總數
    [Header("時間區域")]
    public Text textTime;    //時間文字介面
    public float gameTime;   //時間

    [Header("結束畫布")]
    public GameObject final;
    public Text textBest;
    public Text textCurrent;




    //觸發事件:碰到勾選 IsTrigger 物件會執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap" && other.GetComponent<trap>().DOT == false)    //血量 扣10
        {
            float d = other.GetComponent<trap>().damage;
            hp -= d;               // 血量滑 值 = 血量  
            hpSlider.value = hp;
            if (hp <= 0) Dead();
        }

        if (other.tag == "Score")
        {
            chickencount++;
            textChicken.text = "CHICKEN : " + chickencount + " / " + chickenTotal;
            Destroy(other.gameObject);
        }

        if (other.name == "終點" && chickencount == chickenTotal)
        {
            print("過關");
            GameOver();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Trap" && other.GetComponent<trap>().DOT)    //血量 扣10
        {
            float d = other.GetComponent<trap>().damage;
            hp -= d;               // 血量滑 值 = 血量  
            hpSlider.value = hp;
            if (hp <= 0) Dead();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Trap")                            //如果 碰到 標籤 等於 "陷阱"
        {
            float d = other.GetComponent<trap>().damage;      //取得元件<泛型>().成員              
            hp -= d;                                        //血量 扣 傷害直   
            hpSlider.value = hp;                            //血量滑桿.值 = 血量     
        }
    }


    private void Start()
    {
        chickenTotal = GameObject.FindGameObjectsWithTag("Score").Length;          // 雞腿總數 = 遊戲物件
        textChicken.text = "CHICKEN : 0 / " + chickenTotal;

        if (PlayerPrefs.GetFloat("最佳紀錄") == 0)
        {
            PlayerPrefs.SetFloat("最佳紀錄", 99999.0f);
        }
    }

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        gameTime += Time.deltaTime;
        textTime.text = gameTime.ToString("F2");
    }

    private void GameOver()
    {
        final.SetActive(true);
        textCurrent.text = "TIME : " + gameTime.ToString("F2");

        if (gameTime < PlayerPrefs . GetFloat("最佳紀錄"))
        {
            PlayerPrefs.SetFloat("最佳紀錄", gameTime);
                
        }

        textBest.text = "BEST : " + PlayerPrefs.GetFloat("最佳紀錄").ToString("F2");

        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Dead()
    {
        final.SetActive(true);
        textCurrent.text = "TIME : " + gameTime.ToString("F2");

        textBest.text = "BEST : " + PlayerPrefs.GetFloat("最佳紀錄").ToString("F2");

        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
