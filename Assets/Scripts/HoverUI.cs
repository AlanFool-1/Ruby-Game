using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image uiText;

    public void Start()
    {
        uiText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiText.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        uiText.gameObject.SetActive(false);
    }
}
