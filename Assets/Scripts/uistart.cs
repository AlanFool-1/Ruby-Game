using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uistart : MonoBehaviour
{
    public GameObject MissionBar;
    private float showTimer = 2f;
    public Button menuButton,button1,button2;
    public GameObject panel,win,lose;
    private bool flag=false;

    public static uistart instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        MissionBar.SetActive(true);
        menuButton.onClick.AddListener(ShowPanel);
        button1.onClick.AddListener(ToMainMenu);
        button2.onClick.AddListener(HiddenPanel);
        panel.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            MissionBar.SetActive(false);
        }
        if (RubyController.instance != null)
        {
            if (RubyController.instance.health==0||RubyController.instance.Lefttime<0)
            {
                lose.SetActive(true);
                Time.timeScale = 0f; // ‘›Õ£”Œœ∑
                Debug.Log("”Œœ∑ ß∞‹");
            }
            if(UIHealthBar.instance.no_enemyleft==true)
            {
                win.SetActive(true);
                Time.timeScale = 0f; // ‘›Õ£”Œœ∑
                Debug.Log("”Œœ∑ §¿˚");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if(Time.timeScale==1f)
                ShowPanel();
            else
                HiddenPanel();
        }
        if(Input.GetKeyUp(KeyCode.Tab) )
        {
            EnemyController[] allEnemies = FindObjectsOfType<EnemyController>();
            flag = !flag;
            foreach (EnemyController enemy in allEnemies)
            {
                enemy.BloodTip.SetActive(flag);
            }

        }
    }

    void ShowPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; // ‘›Õ£”Œœ∑
    }
    void HiddenPanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f; // ª÷∏¥”Œœ∑
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

}
