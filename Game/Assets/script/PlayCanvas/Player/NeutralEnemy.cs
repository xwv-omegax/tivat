using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralEnemy : MonoBehaviour
{

    public Character[] characters;
    public int characterCount;
    public int MaxCount;
    public void Initial()
    {
        MaxCount = 8;
        characters = new Character[10];
        characterCount = 0;
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
