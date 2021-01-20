using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject[] cardObjects = new GameObject[10];
    public int maxCards=8;//最大手牌数
    public int count;//手牌数量
    public GameObject parent;//父节点
    public GameObject deck;//牌库

    public Card[] selectCards = new Card[10];//选择的卡
    public int selectCount;

    public int myHash()
    {
        int hash = 0;
        for(int i = 0; i < count; i++)
        {
            hash += cardObjects[i].GetComponent<Card>().cardName.GetHashCode();
        }
        return hash;
    }

    public void SelectCard(Card card)//选择一张卡
    {
        selectCards[selectCount++] = card;
        int index = FindCard(card);
        if (parent.GetComponent<Player>().isPlayer)
        {
            Send("BBAA" + (char)(index + '0'));
        }
    }

    public void DeSelect(Card card)//取消选择一张卡
    {
        int index=selectCount-1;
        while (index >= 0 && selectCards[index] != card) 
        { 
            index--;
        }
        if(index != -1)
        {
            if (parent.GetComponent<Player>().isPlayer)
            {
                Send("BBAB" + (char)(index + '0'));
            }
            for (; index < selectCount-1; index++)
            {
                selectCards[index] = selectCards[index+1];
            }
            selectCards[--selectCount ] = null;
        }
    }

    public bool IsPlayer;//是否是玩家
    // Start is called before the first frame update
    void Start()
    {
    }

    public int FindCard(string name)
    {
        int index = count - 1;
        while(index >= 0 && cardObjects[index].GetComponent<Card>().cardName != name)
        {
            index--;
        }
        return index;
    }

    public int FindCard(Card card)//从手牌中找到一张卡的位置
    {
        int index = count - 1;
        while (index >= 0 && cardObjects[index].GetComponent<Card>() != card)
        {
            index--;
        }
        return index;
    }

    public void DeleteCard(Card card)//从手牌中删除这张卡
    {
        int index = FindCard(card);
        if (index != -1)
        {
            Destroy(cardObjects[index].gameObject);
            for (; index < count - 1; index++)
            {
                cardObjects[index] = cardObjects[index + 1];
            }
            cardObjects[--count] = null;
        }
    }

    public void DeleteCard(string name)
    {
        int index = FindCard(name);
        if (index != -1)
        {
            Destroy(cardObjects[index].gameObject);
            for (; index < count - 1; index++)
            {
                cardObjects[index] = cardObjects[index + 1];
            }
            cardObjects[--count] = null;
        }
    }

    public bool Send(string msg) {
        return parent.GetComponent<Player>().Send(msg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh() //更新位置
    {
        selectCount = 0;
        float interval = 0.6f;
        for(int i = count-1; i >=0; i--)
        {
            cardObjects[i].transform.localPosition = new Vector3(8.5f - i * interval, -3.5f,  - i/10.0f-0.1f);
            cardObjects[i].GetComponent<Card>().NormalPos = cardObjects[i].transform.localPosition;
            if(cardObjects[i].GetComponent<Card>().state != ButtonState.Disabled) 
                cardObjects[i].GetComponent<Card>().ForceChangeState(ButtonState.Normal);
        }
    }

    public void AddCard(Card card)//往手牌中增加一张牌
    {
        if (count < maxCards)//卡牌数未超上限
        {
            cardObjects[count++] = Card.CreatObject(this.gameObject, card);
        }
        else
        {
            parent.GetComponent<Player>().battleArea.GetComponent<BattleArea>().CardUsed(card);
        }
        Refresh();
    }

    public void ForceAddCard(Card card)
    {
        parent.GetComponent<Player>().ResetButtonDown();
        if (count >= maxCards)
        {
            DeleteCard(cardObjects[count -1].GetComponent<Card>());
        }
        AddCard(card);
    }

    public void ForceAddCard(string name)
    {
        ForceAddCard(Card.GetCard(name));
    }

    public void AddCard(string name)
    {
        AddCard(Card.GetCard(name));
    }

    public bool GetCard(string name)
    {
        if (deck.GetComponent<Deck>().DeleteCard(name)) {
            AddCard(name);
            return true;
        }
        return false;
    }

    public bool GetCard(int index)//从牌库中获取一张位置在index的牌
    {
        if(deck.GetComponent<Deck>().DeleteCard(index, out string name))
        {
            AddCard(name);
            return true;
        }
        return false;
    }

    public void GetCard()//从牌库中获取一张牌
    {
        int pos = deck.GetComponent<Deck>().GetCard(out string geta);
        //if(parent.GetComponent<Player>().isPlayer)Send("BBB" + (char)(pos/10+'0')+(char)(pos%10 +'0'));
        if ( geta != null)//牌库有牌
        {
            AddCard(geta);
        }
        else//牌库无牌
        {

        }
    }


}
