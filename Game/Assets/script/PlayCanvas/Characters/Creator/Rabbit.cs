using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : GameBase
{
    public int timeRemain;
    public Vector2Int position;
    public Amber amber;

    public static GameObject CreatRabbit(GameObject parent, Vector2Int pos, Amber owner)
    {
        GameObject obj = CreatObject<Rabbit>(parent);
        obj.transform.localPosition = BattleArea.GetLocalPosition(pos);
        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
        obj.transform.localScale = new Vector3(1f, 1f, 1f);

        Rabbit rabbit = obj.GetComponent<Rabbit>();
        rabbit.sprites = parent.GetComponent<Player>().sprites;
        rabbit.ChangeApprence(rabbit.sprites.GetComponent<AllSprites>().Creator_Rabbit);
        rabbit.position = pos;
        rabbit.timeRemain = 1;
        rabbit.amber = owner;
        return obj;
    }
    public void NewRound()
    {
        timeRemain--;
        if (timeRemain < 1)
        {
            Burst();
            amber.RabbitDestroy();
        }
    }

    public void Burst()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                GameObject obj = CreatObject<Attack>(parent);
                Attack atk = obj.GetComponent<Attack>();
                atk.Initial(new Vector2Int(i,j)+ position, 2, AttackType.ElementalSkill, ElementType.Pyro, amber);
                atk.transform.localPosition = BattleArea.GetLocalPosition(position);
            }
        }
    }

    public string StringGet()
    {
        return "" + (char)timeRemain + (char)position.x + (char)position.y;
    }
    public void StringSet(string msg)
    {
        timeRemain = msg[0];
        position = new Vector2Int(msg[1], msg[2]);
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
