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
        obj.transform.localPosition = new Vector3(pos.x - 3.5f, pos.y - 3.5f, -1);
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        render.sprite = lisa.parent.GetComponent<Player>().sprites.GetComponent<AllSprites>().characterAvatar_Fischl;
        Rose rose = obj.GetComponent<Rose>();
        rose.sprites = lisa.parent.GetComponent<Player>().sprites;
        rose.Init();
        rose.lisa = lisa;
        rose.ShowNormalState();
        return obj;
    }

    public Lisa lisa;

    public void newRound()
    {
        remainTime--;
        if (remainTime < 1)
        {
            lisa.DestroyRose();
            return;
        }

        Character[] characters = lisa.parent.GetComponent<Player>().GetEnemyCharacters();
        Character cha = characters[0];
        foreach(Character character in characters)
        {
            if (character.HP < cha.HP)
            {
                cha = character;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
