using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour//保存键
{
    public GameObject character;
    public GameObject item;
    public GameObject selected;

    public void onClick()
    {
        BuildCanvas buildCanvas = GetComponentInParent<BuildCanvas>();
        CardList select = selected.GetComponent<CardList>();
        if (select.selectCharacterCount == select.MaxCharacter && select.selectItemCount == select.MaxItem)//如果符合保存的规则（角色和物品卡都选满）
        {
            character.GetComponent<CardList>().SaveFile(buildCanvas.characterPath);//角色存到..
            item.GetComponent<CardList>().SaveFile(buildCanvas.itemPath);//物品存到..
            selected.GetComponent<CardList>().SaveFile(buildCanvas.selectPath);//选择的角色存到..
            Massage.CreateMsg("保存成功", new Vector3(0, 45, 0), gameObject, 1);
        }
        else
        {
            Massage.CreateMsg("请选满4+20张卡", new Vector3(0, 45, 0), gameObject, 1);
            Debug.LogError("保存失败");
        }
    }
}
