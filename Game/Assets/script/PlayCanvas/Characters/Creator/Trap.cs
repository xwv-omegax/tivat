using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : GameBase
{
    public static GameObject CreatTrap(GameObject parent, Vector2Int pos)
    {
        GameObject obj = CreatObject<Trap>(parent);
        obj.transform.localPosition = new Vector3(pos.x - 3.5f, pos.y - 3.5f, -1.0f);
        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Trap trap = obj.GetComponent<Trap>();
        trap.sprites = parent.GetComponent<Player>().sprites;
        trap.ChangeApprence(trap.sprites.GetComponent<AllSprites>().Creator_Trap);

        return obj;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(parent != collision.gameObject.transform.parent.gameObject)
        {
            if(collision.TryGetComponent<Character>(out Character character))
            {
                character.SelfDamege(2);
                Destroy(this.gameObject);
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
