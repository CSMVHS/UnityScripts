using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountObjects : MonoBehaviour
{
    public GameObject objUI;

    // Start is called before the first frame update
    void Start()
    {
        objUI = GameObject.Find("Text");
    }

    // Update is called once per frame
    void Update()
    {
        objUI.GetComponent<Text>().text = ObjectsToCollect.objects.ToString();
        if (ObjectsToCollect.objects == 0)
        {
            objUI.GetComponent<Text>().text = "All objects collected.";
        }

    }
}
