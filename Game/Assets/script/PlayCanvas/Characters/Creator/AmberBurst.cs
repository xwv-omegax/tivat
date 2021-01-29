using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberBurst : Creator
{
    public void Init()
    {
        remainTime = 3;
        MaxRemainTine = 3;
    }

    public static AmberBurst CreateSelf(Vector2Int pos, Amber amber)
    {
        GameObject obj = CreatObject<AmberBurst>(amber.parent);
        obj.transform.localPosition = BattleArea.GetLocalPosition(pos);
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        render.sprite = amber.parent.GetComponent<Player>().sprites.GetComponent<AllSprites>().Creator_Rose;
        AmberBurst rose = obj.GetComponent<AmberBurst>();
        rose.sprites = amber.parent.GetComponent<Player>().sprites;
        rose.Init();
        rose.amber = amber;
        rose.ShowNormalState();
        rose.burnPos = pos;
        return rose;
    }
    public Amber amber;

    public Vector2Int burnPos;

    public void newRound()
    {
        remainTime--;
        amber.BurstBurn(burnPos);
        if (remainTime < 1)
        {
            amber.DestroyBurstBurn();
        }
    }

    public string StringGet()
    {
        return "" + (char)remainTime + (char)burnPos.x + (char)burnPos.y;
    }

    public void StringSet(string msg)
    {
        remainTime = msg[0];
        burnPos = new Vector2Int(msg[1], msg[2]);
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
