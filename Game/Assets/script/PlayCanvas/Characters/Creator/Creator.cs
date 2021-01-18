using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : GameBase
{

    public int blood;
    public int MaxBlood;

    public int remainTime;

    public int MaxRemainTine;

    public void ShowNormalState()
    {
        if (normalState != null) DeleteNormalState();

        normalState = new GameObject();
        normalState.transform.parent = transform;
        normalState.transform.localPosition = new Vector3(-0.4f, -0.4f, -3);
        normalState.transform.localRotation = new Quaternion(0, 0, 0, 0);
        normalState.transform.localScale = new Vector3(0.15f, 0.25f, 1);

        for (int i = 0; i < blood; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = normalState.transform;
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
            obj.AddComponent<SpriteRenderer>().sprite = sprites.GetComponent<AllSprites>().Bar_Red;
            obj.transform.localPosition = new Vector3(i, 0);
        }
        for (int i = 0; i < remainTime; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = normalState.transform;
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
            obj.AddComponent<SpriteRenderer>().sprite = sprites.GetComponent<AllSprites>().Bar_Blue;
            obj.transform.localPosition = new Vector3(i, 0.5f);
        }
    }

    public void DeleteNormalState()
    {
        if (normalState == null) return;
        int count = normalState.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(normalState.transform.GetChild(i).gameObject);
        }
        Destroy(normalState);
        normalState = null;
    }

    public GameObject normalState=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
