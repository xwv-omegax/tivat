using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCanvas : MonoBehaviour//绘制build界面
{
    public GameObject[] lists = new GameObject[100];//未选择的列出元素
    public int listCount;//数量
    public GameObject[] selected = new GameObject[100];//已选择的列出元素
    public int selectCount;//数量

    public void clear()//清除所有元素数据
    {
        for (int i = 0; i < listCount; i++) Destroy(lists[i]);
        listCount = 0;
        for (int i = 0; i < selectCount; i++) Destroy(selected[i]);
        selectCount = 0;
    }
    public void updateObjects(string[] cardList, int listCardCount,GameObject Alive, string[] cardSelect, int selectCardCount, GameObject Selects)//从字符串中更新列出元素数据
    {
        clear();

        listCount = listCardCount;
        for(int i = 0; i < listCount; i++)
        {
            lists[i] = new GameObject();
            lists[i].transform.parent = this.transform;
            BDButton bDButton =  lists[i].AddComponent<BDButton>();
            bDButton.Init(BDButton.findBDbutton(cardList[i]));
            bDButton.parentList = Alive;
            bDButton.Init();
            bDButton.name = cardList[i];
            lists[i].transform.localPosition = new Vector3((i%10)*100,-((i/10)*100),0);
        }

        selectCount = selectCardCount;
        for(int i = 0; i < selectCount; i++)
        {
            selected[i] = new GameObject();
            selected[i].transform.parent = this.transform;
            BDButton bDButton = selected[i].AddComponent<BDButton>();
            bDButton.Init(BDButton.findBDbutton(cardSelect[i]));
            bDButton.parentList = Selects;
            bDButton.Init();
            bDButton.name = cardSelect[i];
            bDButton.transform.localPosition = new Vector3((i % 4) * 100+1200, -((i / 4) * 100), 0);
        }
    }
}
