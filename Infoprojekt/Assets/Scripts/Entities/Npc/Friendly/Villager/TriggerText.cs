using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerText : MonoBehaviour
{
    public Text text;
    bool enter = false;

    private void OnTriggerEnter(Collider other)
    {
        print("test");
        enter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        enter = false;
    }

    void Update()
    {
        if (enter)
        {
            text.text = "test";
           // if (this.tag.Equals("HausUnten"))
            //{
                //text.gameObject.SetActive(true);
                //text.text = "Pff ... mit dir will ich gar nichts zu tun haben";
            //}
        }
    }
}
