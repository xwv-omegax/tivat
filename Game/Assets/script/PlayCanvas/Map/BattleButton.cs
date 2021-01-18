using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonState {Normal, HighLight,OnCurse ,Selected,Disabled};
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

    public void UpdateState()//更新state的状态
    {
        switch (state)
        {
            case ButtonState.Disabled:
                this.gameObject.GetComponent<Image>().color = new Color(25f / 255, 25f / 255, 25f / 255, 255f / 255);
                break;
            case ButtonState.Selected:
                this.gameObject.GetComponent<Image>().color = new Color(255f / 255, 215f / 255, 215f / 255, 255f / 255);
                break;
            case ButtonState.HighLight:
                this.gameObject.GetComponent<Image>().color = new Color(255f / 255, 215f / 255, 215f / 255, 255f / 255);
                break;
            case ButtonState.Normal:
                this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case ButtonState.OnCurse:
                this.gameObject.GetComponent<Image>().color = new Color(253.0f / 255, 173.0f / 255, 173.0f / 255, 1.0f);
                break;
            default:
                break;
        }
    }
    public override void ChangeState(ButtonState state)
    {
        base.ChangeState(state);
        UpdateState();
    }
}
