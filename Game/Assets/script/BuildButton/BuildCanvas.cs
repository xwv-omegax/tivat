using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    public GameObject character;
    public GameObject item;
    public GameObject selected;
    public string characterPath = "save/build/characters";
    public string itemPath = "save/build/item";
    public string selectPath = "save/build/select";

    public void Init()//从本地文件初始化build界面
    {
        character.GetComponent<CardList>().ReadFile(characterPath);
        item.GetComponent<CardList>().ReadFile(itemPath);
        selected.GetComponent<CardList>().ReadFile(selectPath);

        item.GetComponent<CardList>().OnClick();
    }

    public void Default()
    {
        CardList cha =  character.GetComponent<CardList>();
        CardList ite = item.GetComponent<CardList>();
        CardList sel = selected.GetComponent<CardList>();

        cha.cardCount = 7;
        string[] vs = new string[]
        {
            "Character_Amber",
            "Character_Lisa",
            "Character_Noelle",
            "Character_Diluc",
            "Character_Ningguang",
            "Character_Keqing",
            "Character_Jean"
        };
        for(int i = 0; i < 7; i++)
        {
            cha.cards[i] = vs[i];
        }
        ite.cardCount = 26;
        vs = new string[]
        {
            "Item_Advice",
            "Item_Advice",
            "Item_Clock",
            "Item_Clock",
            "Item_Trap",
            "Item_Trap",
            "Item_Tea",
            "Item_Tea",
            "Item_Sunsettia",
            "Item_Sunsettia",
            "Item_Teaport",
            "Item_Teaport",
            "Item_Sigil",
            "Item_Sigil",
            "Item_Chill",
            "Item_Chill",
            "Item_CrystalCore",
            "Item_CrystalCore",
            "Item_Sword",
            "Item_Sword",
            "Item_Book",
            "Item_Book",
            "Item_Bow",
            "Item_Bow",
            "Item_Claymore",
            "Item_Claymore"
        };
        for(int i = 0; i < 26; i++)
        {
            ite.cards[i] = vs[i];
        }
        sel.cardCount = 0;
    }

    private void Start()
    {
        Init();
    }

}
