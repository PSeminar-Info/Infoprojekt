using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelSettings;
    public GameObject panelInfos;
    public GameObject panelCredits;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPanel(GameObject abc)
    {
        abc.SetActive(true);
    }
    public void ClosePanel(GameObject abc)
    {
        abc.SetActive(false);
    }


}
