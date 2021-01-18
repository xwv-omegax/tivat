using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keaya : Hero
{
    public override void Heroinit()
    {
        if (Inited) return;
        Inited = true;
        Initial("Character_Keaya", 4, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Heroinit();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
