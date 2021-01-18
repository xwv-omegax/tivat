using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ExitGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isHighLighted = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }

    public GameObject text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHighLighted)
        {
            text.GetComponent<Text>().color = new Color(1, 1, 1);
            isHighLighted = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHighLighted)
        {
            text.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0.8f);
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
