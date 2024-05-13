using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettinScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderSound;
    private float sound;
    private float oldsound = 1;
    public AudioSource audioSource;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sound = sliderSound.value;
       
    }
    public void UpdateSound()
    {
        oldsound = sound;
        audioSource.volume = sound;
    }
    public void dontupdateSound()
    {
        sound = oldsound;
        sliderSound.value = sound;
    }
}
