using UnityEngine;
using UnityEngine.UI;

namespace GroupCharacter.cHARACTER.Character
{
    public class SettinScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public Slider sliderSound;
        public AudioSource audioSource;
        private float _oldsound = 1;
        private float _sound;

        // Update is called once per frame
        private void Update()
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