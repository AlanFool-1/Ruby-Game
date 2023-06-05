using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    float originalSize;
    public Text BulletCountText;
    public Text EnemyCountText;
    public Text LeftTimeText;
    int curEnemyAmount, maxEnemyAmount;
    public bool no_enemyleft { get { return (curEnemyAmount == maxEnemyAmount); } }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    public void UpdateBulletCount(int curAmount,int maxAmount)
    {
        BulletCountText.text=curAmount.ToString()+"/"+maxAmount.ToString();
    }
    public void UpdateEnemyCount(int add, int maxAmount=10)
    {
        curEnemyAmount+=add;
        if(maxAmount!=10)
            maxEnemyAmount=maxAmount;
        EnemyCountText.text = curEnemyAmount.ToString() + "/" + maxEnemyAmount.ToString();
    }

    public void UpdateLeftTime(int lefttime)
    {
        int min = lefttime / 60;
        int sec = lefttime % 60;
        if (sec < 10 && min < 10)
        {
            LeftTimeText.text = "0" + min.ToString() + ":0" + sec.ToString();
        }
        if (sec > 10 && min < 10)
        {
            LeftTimeText.text = "0" + min.ToString() + ":" + sec.ToString();
        }
    }
}
