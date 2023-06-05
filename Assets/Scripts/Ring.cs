using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ring : MonoBehaviour
{
    public static Ring instance { get; private set; }
    public Image m_Image;
    public Text m_Text;
    void Awake()
    {
        instance = this;
    }
    public void UpdateEnergy(int curenergy,int maxenergy)
    {
        if (curenergy <= maxenergy)
        {
            m_Text.text = ((int)curenergy).ToString();
            m_Image.fillAmount = curenergy / 100.0f;
        }
    }
}
