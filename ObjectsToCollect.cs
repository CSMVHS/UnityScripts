using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToCollect : MonoBehaviour
{
    public static int objects = 0;

    private void Awake()
    {
        objects++;
    }

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            objects--;
            
        }
        gameObject.SetActive(false);

    }
}