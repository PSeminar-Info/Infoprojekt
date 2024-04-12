using UnityEngine;
using UnityEngine.SceneManagement;

namespace GroupCharacter.SceneMenu
{
    public class SzeneLader : MonoBehaviour
    {
        public void LadeAndereSzene()
        {
            // Lade die Szene mit dem Namen "MeineAndereSzene".
            SceneManager.LoadScene("SceneNow");
        }
    }
}