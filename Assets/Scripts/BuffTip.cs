using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image uiText;
    public string text,Name;

    public void Start()
    {
        uiText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Name=eventData.pointerEnter.GetComponent<Image>().name;
        text=chooseTip(text, Name);
        uiText.transform.GetComponentInChildren<Text>().text= text;
        uiText.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        uiText.gameObject.SetActive(false);
    }

    string chooseTip(string text,string Name)
    {
        if (Name == "0")
        {
            text = "普攻齿轮增加1点攻击力，持续15秒";
        }else if (Name == "1")
        {
            text = "角色处于无敌状态，持续15秒";
        }else if(Name== "2")
        {
            text = "生命恢复：每3秒恢复1点生命值，持续15秒";
        }else if (Name == "3")
        {
            text = "能量恢复：每3秒恢复5点能量值，持续15秒";
        }else if(Name== "4")
        {
            text = "齿轮恢复：每3秒恢复6颗齿轮，持续15秒";
        }
        return text;
    }
}
