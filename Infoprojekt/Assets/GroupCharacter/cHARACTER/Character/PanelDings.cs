using UnityEngine;

namespace GroupCharacter.cHARACTER.Character
{
    public class PanelDings : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject panelSettings;
        public GameObject panelInfos;
        public GameObject panelCredits;

        public void OpenPanel(GameObject abc)
        {
            abc.SetActive(true);
        }

        public void ClosePanel(GameObject abc)
        {
            abc.SetActive(false);
        }
    }
}