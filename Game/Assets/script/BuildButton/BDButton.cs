using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BDButton: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler//元素按钮
{
    public string cardName;//名字

    public GameObject parentList;//父列表

    public ListType type;//类型

    public Sprite head;//头像
    public Sprite Info;//详细信息

    public GameObject info;

    private void Start()
    {
    }

    public void Init(BDButton other)
    {
        cardName = other.cardName;
        head = other.head;
        Info = other.Info;
        type = other.type;
        parentList = other.parentList;
    }//从其他BDbutton初始化自身

    public void Init()//初始化
    {
        if(!this.TryGetComponent<RectTransform>(out RectTransform rectTransform))
        {
            rectTransform = this.gameObject.AddComponent<RectTransform>();
        }
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);

        if (!this.gameObject.TryGetComponent<Image>(out Image rend))
        {
            rend = this.gameObject.AddComponent<Image>();
        }
        rend.sprite = head;
    }

    public void ShowInfo()//展现详细信息
    {
        info = new GameObject();
        info.name = "info";

        info.transform.parent = this.gameObject.transform;
        if (this.transform.localPosition.x >= 1000)
        {
            info.transform.localPosition = new Vector3(-200, -180, -5);
        }
        else
        {
            info.transform.localPosition = new Vector3(200, -180, -5);
        }
        info.transform.localScale = new Vector3(250, 250, 150);

        SpriteRenderer spr =  info.AddComponent<SpriteRenderer>();
        spr.sprite = Info;

    }

    public void DeleteInfo()
    {
        Destroy(info);
    }//删除展现的详细信息

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo();
    }//指针进入界面

    public void OnPointerExit(PointerEventData eventData)
    {
        DeleteInfo();
    }//指针离开界面

    public void OnPointerClick(PointerEventData eventData)
    {
        if(parentList.gameObject.TryGetComponent<CardList>(out CardList cardList))
        {
            cardList.OnBDButtonClick(cardName);
        }
    }//点击

    public static BDButton findBDbutton(string name)
    {
        GameObject obj =  GameObject.Find("Cards/"+name);
        return obj.GetComponent<BDButton>();
    }//用名字找预制件

}
