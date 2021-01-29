using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : Creator
{
    public void Init()
    {
        blood = 4;
        MaxBlood = 4;
        remainTime = 5;
        MaxRemainTine = 5;
    }

    public static GameObject CreatRose(Vector2Int pos ,Lisa lisa)
    {
        GameObject obj = GameBase.CreatObject<Rose>(lisa.parent);
        obj.transform.localPosition = BattleArea.GetLocalPosition(pos);
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        render.sprite = lisa.parent.GetComponent<Player>().sprites.GetComponent<AllSprites>().Creator_Rose;
        Rose rose = obj.GetComponent<Rose>();
        rose.sprites = lisa.parent.GetComponent<Player>().sprites;
        rose.Init();
        rose.lisa = lisa;
        rose.ShowNormalState();
        rose.positon = pos;
        return obj;
    }

    public Lisa lisa;
    public Vector2Int positon;
    public void newRound()
    {
        remainTime--;
        if (remainTime < 1)
        {
            lisa.DestroyRose();
            return;
        }

        Character[] characters = lisa.parent.GetComponent<Player>().GetEnemyCharacters();
        if (characters.Length < 1) return;
        Character cha = characters[0];
        foreach(Character character in characters)
        {
            if (character.HP < cha.HP)
            {
                cha = character;
            }
            else if (character.HP == cha.HP)
            {
                if(character.shield < cha.shield)
                {
                    cha = character;
                }
            }
        }
        Attack.CreateAttack(parent, new Vector2Int(7, 7) - cha.position, 1, AttackType.ElementalBurst, ElementType.Electro, lisa).transform.localPosition = transform.localPosition;
        ShowNormalState();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.transform.parent != parent)
        {
            if(collision.TryGetComponent<Attack>(out Attack atk) && atk.active)
            {
                if (atk.Damage >= blood)
                {
                    blood = 0;
                    lisa.DestroyRose();
                }
                else
                {
                    blood -= atk.Damage;
                }
                Destroy(atk.gameObject);
                ShowNormalState();
            }
        }
    }

    public string StringGet()
    {
        return "" + blood + remainTime + positon.x + positon.y;
    }
    public void StringSet(string msg)
    {
        blood = msg[0];
        remainTime = msg[1];
        positon = new Vector2Int(msg[2], msg[3]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
