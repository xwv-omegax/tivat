using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenuButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public bool isHighLighted = false;

    public string scene;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(scene);
    }

    public GameObject text;
    public SpriteRenderer render;
    public Sprite normal;
    public Sprite oncurse;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHighLighted)
        {
            if(text!=null)text.GetComponent<Text>().color = new Color(1, 1, 1);
            if (render != null && oncurse != null) render.sprite = oncurse;
            isHighLighted = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHighLighted)
        {
            if (text != null) text.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0.8f);
            if (render != null && normal != null) render.sprite = normal;
            isHighLighted = false;
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
