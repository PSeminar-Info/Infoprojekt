using UnityEngine;

public class GeneralEffectScript : MonoBehaviour
{
    public float distance;

    public float timetilldeath;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Destroy(gameObject, timetilldeath);
    }
}