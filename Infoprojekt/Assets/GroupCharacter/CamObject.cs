using UnityEngine;

namespace GroupCharacter
{
    public class CamObject : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
        }
    }
}