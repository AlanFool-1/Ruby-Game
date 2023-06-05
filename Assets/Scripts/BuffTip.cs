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
            text = "�չ���������1�㹥����������15��";
        }else if (Name == "1")
        {
            text = "��ɫ�����޵�״̬������15��";
        }else if(Name== "2")
        {
            text = "�����ָ���ÿ3��ָ�1������ֵ������15��";
        }else if (Name == "3")
        {
            text = "�����ָ���ÿ3��ָ�5������ֵ������15��";
        }else if(Name== "4")
        {
            text = "���ָֻ���ÿ3��ָ�6�ų��֣�����15��";
        }
        return text;
    }
}
