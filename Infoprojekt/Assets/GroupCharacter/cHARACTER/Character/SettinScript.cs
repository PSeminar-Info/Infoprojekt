using UnityEngine;
using UnityEngine.UI;

namespace GroupCharacter.cHARACTER.Character
{
    public class SettinScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public Slider sliderSound;
        private float _sound;
        private float _oldsound = 1;
        public AudioSource audioSource;

        // Update is called once per frame
        void Update()
        {
            _sound = sliderSound.value;
       
        }
        public void UpdateSound()
        {
            _oldsound = _sound;
            audioSource.volume = _sound;
        }
        public void DontupdateSound()
        {
            _sound = _oldsound;
            sliderSound.value = _sound;
        }
    }
}
