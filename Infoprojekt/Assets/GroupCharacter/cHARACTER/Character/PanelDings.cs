using UnityEngine;

namespace GroupCharacter.cHARACTER.Character
{
    public class PanelDings : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject panelSettings;
        public GameObject panelInfos;
        public GameObject panelCredits;
        public GameObject texte;

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void OpenPanel(GameObject abc)
        {
            abc.SetActive(true);
        }

        public void ClosePanel(GameObject abc)
        {
            abc.SetActive(false);
            texte.SetActive(true);
        }
    }
}