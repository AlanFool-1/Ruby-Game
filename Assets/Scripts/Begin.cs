using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour
{
    //����ѡ�ؽ���
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

    //�˳���Ϸ
    public void Quit()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //�������˵�
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //��Ϸ˵������
    public void Todescription()
    {
        SceneManager.LoadScene("Description");
    }
    //���¿�ʼ
    public void restart()
    {
        // ��ȡ��ǰ����ĳ���
        Scene currentScene = SceneManager.GetActiveScene();

        // ��ȡ��ǰ����������
        int buildIndex = currentScene.buildIndex;
        // ���¼��ص�ǰ����
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

}