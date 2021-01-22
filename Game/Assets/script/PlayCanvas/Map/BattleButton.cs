using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonState {Normal, HighLight,OnCurse ,Selected,Disabled,NormalHighlighted };
public class BattleButton : ButtonBase
{
    public int row;//所在行数
    public int col;//所在列数
    public GameObject battleArea;//父节点
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()//当按钮按下时
    {
        BattleArea Area = battleArea.GetComponent<BattleArea>();
        Area.AreaSelect(row, col);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        OnClick();
        Debug.Log("row:" + row.ToString() + "col:" + col.ToString());
    }

    public void ChangeAnimatorState(int state)
    {
        if (gameObject.TryGetComponent<Animator>(out Animator animator))
        {
            animator.SetInteger("state", state);
        }
    }

    public void UpdateState()//更新state的状态
    {
        switch (state)
        {
            case ButtonState.Disabled:
                ChangeAnimatorState(0);
                break;
            case ButtonState.Selected:
                ChangeAnimatorState(2);
                break;
            case ButtonState.HighLight:
                ChangeAnimatorState(1);
                break;
            case ButtonState.Normal:
                ChangeAnimatorState(0);
                break;
            case ButtonState.OnCurse:
                ChangeAnimatorState(3);
                break;
            case ButtonState.NormalHighlighted:
                ChangeAnimatorState(4);
                break;
            default:
                ChangeAnimatorState(0);
                break;
        }
    }
    public override void ChangeState(ButtonState state)
    {
        base.ChangeState(state);
        UpdateState();
    }
}
