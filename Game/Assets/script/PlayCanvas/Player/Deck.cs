using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public string[] cards=new string[100];
    public int MAXCardsCount=100;
    public int count;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        Initial();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initial()
    {
        
    }
    public Card GetCard()
    {
        if (count > 0)
        {
            int pos = Random.Range(0, count);
            Card tmp = Card.GetCard(cards[pos]);
            for (int i = pos; i < count - 1; i++)
            {
                cards[i] = cards[i + 1];
            }
            count -= 1;
            UpdateText();
            return tmp;
        }
        UpdateText();
        return null;
    
    }//取牌

    public int GetCard(out Card card)//带位置取牌
    {
        if(count > 0)
        {
            int pos = Random.Range(0, count);
            card = Card.GetCard(cards[pos]);
            for (int i = pos; i < count - 1; i++)
            {
                cards[i] = cards[i + 1];
            }
            count -= 1;
            UpdateText();
            return pos;
        }
        card = null;
        UpdateText();
        return -1;
    }

    public int GetCard(out string name)//带位置取字符串牌
    {
        if (count > 0)
        {
            int pos = Random.Range(0, count);
            name = cards[pos];
            for (int i = pos; i < count - 1; i++)
            {
                cards[i] = cards[i + 1];
            }
            count -= 1;
            UpdateText();
            return pos;
        }
        name = null;
        UpdateText();
        return -1;
    }

    public bool DeleteCard(Card card) {
        return DeleteCard(card.cardName);
    }

    public bool DeleteCard(string name)
    {
        int index = count;
        while( index > -1 && (cards[index] != name))index--;
        if (index == -1) return false;
        while (index < count - 1)
        {
            cards[index] = cards[index + 1];
            index++;
        }
        count--;
        UpdateText();
        return true;
    }

    public bool DeleteCard(int index, out string name) {
        if (index < 0 || index > count) { name = null; return false; }
        name = cards[index];
        while (index < count - 1)
        {
            cards[index] = cards[index + 1];
            index++;
        }
        cards[count-1] = null;
        count--;
        UpdateText();
        return true;
    }//删除index的牌，并返回删除了什么


    public void PutCard(string name)
    {
        if (count < MAXCardsCount)//牌库牌数未超上限
        {
            cards[count++] = name;
            UpdateText();
        }
        else//牌库排数超过上限
        {

        }
    }

    public void PutCard(Card other) {
        if (count < MAXCardsCount)//牌库牌数未超上限
        {
            cards[count++] = other.cardName;
            UpdateText();
        }
        else//牌库排数超过上限
        {

        }
    }//放牌

    public void UpdateText()
    {
        text.GetComponent<Text>().text = count.ToString();
    }
}
