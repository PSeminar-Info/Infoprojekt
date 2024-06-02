using UnityEngine;

namespace GroupCharacter.cHARACTER.Character.Book
{
    public class GeneralEffectScript : MonoBehaviour
    {
        public float distance;

        public float timetilldeath;

        // Update is called once per frame
        private void Update()
        {
            Destroy(gameObject, timetilldeath);
        }
    }
}