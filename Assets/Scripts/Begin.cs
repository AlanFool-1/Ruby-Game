using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour
{
    //进入选关界面
    public void OnClick()
    {
        SceneManager.LoadScene("MainScene1");
    }

    public void Enter_2()
    {
        SceneManager.LoadScene("MainScene2");
    }
    public void Enter_3()
    {
        SceneManager.LoadScene("MainScene3");
    }

    //退出游戏
    public void Quit()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //返回主菜单
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //游戏说明界面
    public void Todescription()
    {
        SceneManager.LoadScene("Description");
    }
    //重新开始
    public void restart()
    {
        // 获取当前激活的场景
        Scene currentScene = SceneManager.GetActiveScene();

        // 获取当前场景的索引
        int buildIndex = currentScene.buildIndex;
        // 重新加载当前场景
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

}