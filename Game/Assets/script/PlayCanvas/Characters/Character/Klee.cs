using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klee : Character
{
    public override void Initial()
    {
        Initial("可莉", 3, 2);
        sprites = GameObject.Find("Sprites");
        appearance = sprites.GetComponent<AllSprites>().characterAvatar_Klee;
    }

    public static GameObject CreatObject(GameObject parent)
    {
        GameObject obj = CreatObject<Klee>(parent);
        obj.name = "Klee";
        obj.transform.localPosition = new Vector3(0.5f, 0.5f, 0.0f);
        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
        return obj;
    }


    private void Start()
    {
    }
}
