using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigil : GameBase
{
    public bool active = false;//是否激活

    public Vector2Int target;//目标位置
    public float speed = 10.0f;//速度

    public float aliveTime = 0.3f;

    public float activeTime;//激活的时间点

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static GameObject CreatSigil(GameObject parent,Vector2Int target)
    {
        GameObject obj = GameBase.CreatObject<Sigil>(parent);
        obj.transform.localPosition = new Vector3(0, -4, -1);
        Sigil sigil = obj.GetComponent<Sigil>();
        sigil.target = target;
        obj.GetComponent<SpriteRenderer>().sprite = parent.GetComponent<Player>().sprites.GetComponent<AllSprites>().Creator_Sigil;
        return obj;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 tar = new Vector3(target.x - 3.5f, target.y - 3.5f, -1.0f);
        Vector3 dis = tar - transform.localPosition;
        if (dis.sqrMagnitude > 0.001)
        {//如果没到目的地，则运动
            float delta = Time.deltaTime;
            dis.Normalize();
            dis *= speed * delta;
            transform.localPosition += dis;
        }
        else
        {
            if (active)//到达目的地，激活
            {
                if (Time.time - activeTime > aliveTime)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                active = true;
                activeTime = Time.time;
                speed = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)//与其他物体触碰
    {
        if (active == true)//如果激活
        {
            other.gameObject.TryGetComponent<Creator>(out Creator another);
            if (another != null  && another.parent == parent)
            {
                another.blood = another.MaxBlood;
                Destroy(this.gameObject);
            }
        }
    }

}
