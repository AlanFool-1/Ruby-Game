using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconManager : MonoBehaviour
{
    public Image[] buffIcon;
    public Sprite[] Icon;
    void Start()
    {
        for(int i=0; i < buffIcon.Length; i++)
            buffIcon[i].gameObject.SetActive(false);
    }

    public void ShowBuffIcon(Sprite icon, float duration,int idx,int id)
    {
        buffIcon[idx].sprite = icon;
        buffIcon[idx].gameObject.SetActive(true);
        buffIcon[idx].name = id.ToString();
        StartCoroutine(HideBuffIcon(duration,idx));
    }

    private IEnumerator HideBuffIcon(float duration,int idx)
    {
        yield return new WaitForSeconds(duration);
        buffIcon[idx].sprite = null;
        buffIcon[idx].gameObject.SetActive(false);
    }
}
