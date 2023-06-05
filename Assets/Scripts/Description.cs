using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public Button  button1, button2;
    public GameObject panel;

    public static Description instance { get; private set; }

    void Awake()
    {
        instance = this;
        button1.onClick.AddListener(Background);
        button2.onClick.AddListener(Buttoms);
        panel.SetActive(true);
        Text[] texts = panel.GetComponentsInChildren<Text>();
        texts[0].enabled = true;
        texts[1].enabled = false;
    }

    // Update is called once per frame

    void Background()
    {
        Text[] texts = panel.GetComponentsInChildren<Text>();
        texts[0].enabled= true;
        texts[1].enabled= false;

    }
    void Buttoms()
    {
        Text[] texts = panel.GetComponentsInChildren<Text>();
        texts[0].enabled = false;
        texts[1].enabled = true;
    }

}
