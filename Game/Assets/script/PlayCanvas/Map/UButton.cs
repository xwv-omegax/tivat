using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIButtonType {Reset, Use, NextRound }
public class UButton : MonoBehaviour
{
    public GameObject area;
    public UIButtonType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        area.GetComponent<BattleArea>().UIButtonClick(type);
    }
}
