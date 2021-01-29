using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealType { Normal, Jean_Burst}
public class Heal : GameBase //治疗
{
    public bool active;//是否激活
    public int HPHeal;//血量
    public int shieldHeal;//护盾
    public Character healOwner;//释放者
    public Character healTarget;//受治疗者
    public Sprite activeSprite = null;
    public Vector2Int target;
    public float speed = 10f;
    public float activeTime;
    public HealType type;

    public void Initial(Vector2Int targ, int Hp, int Shield, HealType typ , Character owner)
    {
        target = targ;
        HPHeal = Hp;
        shieldHeal = Shield;
        type = typ;
        healOwner = owner;
    }

    private void Update()
    {
        Vector3 tar = BattleArea.GetLocalPosition(target);
        Vector3 dis = tar - transform.localPosition;
        if (dis.sqrMagnitude > 0.001)
        {
            float delta = Time.deltaTime;
            dis.Normalize();
            dis *= speed * delta;
            transform.localPosition += dis;
        }
        else
        {
            if (active)
            {
                if (Time.time - activeTime > 0.1)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                active = true;
                ChangeApprence(activeSprite);
                activeTime = Time.time;
                speed = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (active == true)
        {
            other.gameObject.TryGetComponent<Character>(out Character another);
            if (another != null && another.parent == parent)
            {
                another.OnHeal(this);
                Destroy(this.gameObject);
            }
        }
    }
}//治疗
