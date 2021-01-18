using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum ListType { Character, Item, Selected}//角色、物品、已选择
public class CardList : MonoBehaviour//卡牌列表
{
    public static GameObject Alive;//当前活动的未选择卡牌类型

    public GameObject characterList;//角色列表
    public GameObject itemList;//物品列表
    public GameObject selectList;//已选择的列表

    public GameObject ListCanvas;//绘制界面

    public ListType type;//类型

    public string[] cards = new string[100];//卡牌列表
    public int cardCount;//数量

    public bool deleteCard(string name)
    {
        int i;
        for (i = cardCount - 1; i >-1 && cards[i] != name; i--) ;
        if (i != -1)
        {
            for (; i < cardCount - 1; i++)
            {
                cards[i] = cards[i + 1];
            }
            cardCount--;
            return true;
        }
        return false;
    }//从列表中删除元素

    public void addCard(string name)
    {
        cards[cardCount] = name;
        cardCount++;
    }//增加元素到末尾

    public void addCardAt(string name, int pos)
    {
        for(int i = cardCount; i > pos; i--)
        {
            cards[i] = cards[i - 1];
        }
        cards[pos] = name;
        cardCount++;
    }//指定位置增加元素


    public void addSelectCard(string name, ListType type)
    {
        switch (type)
        {
            case ListType.Character:
                if (selectCharacterCount < MaxCharacter)
                {
                    addCardAt(name, selectCharacterCount);
                    selectCharacterCount++;
                }
                break;
            case ListType.Item:
                if (selectItemCount < MaxItem)
                {
                    addCard(name);
                    selectItemCount++;
                }
                break;
        }
    }//已选择类型专用的增加元素

    public int selectCharacterCount;
    public int MaxCharacter = 4;
    public int MaxItem = 20;
    public int selectItemCount;

    public void updateCanvas()
    {
        ListCanvas.GetComponent<ListCanvas>().updateObjects(Alive.GetComponent<CardList>().cards, Alive.GetComponent<CardList>().cardCount, Alive,
            selectList.GetComponent<CardList>().cards, selectList.GetComponent<CardList>().cardCount, selectList);
    }//更新绘制界面

    public void OnBDButtonClick(string name)
    {
        switch (type)//按自身的类型调用不同方法
        {
            case ListType.Character:
                CharacterClick(name);
                break;
            case ListType.Item:
                ItemClick(name);
                break;
            case ListType.Selected:
                SelectedClick(name);
                break;
            default:
                break;
        }
        updateCanvas();
    }//隶属于自身的按键被点击

    public void CharacterClick(string name)
    {
        if (selectList.GetComponent<CardList>().selectCharacterCount < selectList.GetComponent<CardList>().MaxCharacter && characterList.GetComponent<CardList>().deleteCard(name) )//该类型未选择满，并且可以从列表中删除，第一项熔断
        {
            selectList.GetComponent<CardList>().addSelectCard(name, type);
        }
    }//如果自身类型为角色，调用

    public void ItemClick(string name)
    {
        if (selectList.GetComponent<CardList>().selectItemCount< selectList.GetComponent<CardList>().MaxItem && itemList.GetComponent<CardList>().deleteCard(name))//该类型未选择满，并且可以从列表中删除。第一项熔断
        {
            selectList.GetComponent<CardList>().addSelectCard(name, type);
        }
    }//如果自身为物品，调用

    public void SelectedClick(string name)
    {
        if (selectList.GetComponent<CardList>().deleteCard(name))//如果可以删除
        {
            switch (BDButton.findBDbutton(name).type)//分删除的元素类型，分别往不同的列表添加元素
            {
                case ListType.Character:
                    selectCharacterCount--;
                    characterList.GetComponent<CardList>().addCard(name);
                    break;
                case ListType.Item:
                    selectItemCount--;
                    itemList.GetComponent<CardList>().addCard(name);
                    break;
                default:
                    break;
            }
        }
    }//如果自身为已选择，调用

    public void OnClick()
    {
        if(Alive != this.gameObject)
        {
            Alive = this.gameObject;
            updateCanvas();
        }
    }//自身被点击，则激活自身

    public void ReadFile(string path)
    {
        if (!File.Exists(path)) return;
        BinaryReader br = new BinaryReader(File.OpenRead(path));
        type = (ListType)br.ReadInt32();
        cardCount = br.ReadInt32();
        for(int i = 0; i < cardCount; i++)
        {
            cards[i] = br.ReadString();
        }
        if(type == ListType.Selected)
        {
            selectCharacterCount = br.ReadInt32();
            selectItemCount = br.ReadInt32();
        }
        br.Close();
        Debug.Log("ReadEnd");
    }//从文件读取到自身

    public void SaveFile(string path)
    {
        if (!File.Exists(path)) { File.Create(path).Close(); }
        BinaryWriter bw = new BinaryWriter(File.OpenWrite(path));
        bw.Write((int)type);
        bw.Write(cardCount);
        for(int i = 0; i < cardCount; i++)
        {
            bw.Write(cards[i]);
        }
        if(type == ListType.Selected)
        {
            bw.Write(selectCharacterCount);
            bw.Write(selectItemCount);
        }
        bw.Close();
        Debug.Log("write");
    }//写入文件到本地
    
}
