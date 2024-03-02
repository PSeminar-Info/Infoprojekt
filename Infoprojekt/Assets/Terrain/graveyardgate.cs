using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graveyardgate : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText; //text that shows that you can interact with the door when in interaction distance

    public string doorOpenAnimName, doorCloseAnimName;

    public GameObject player;

    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            GameObject doorParent = hit.collider.transform.root.gameObject; //root is main parent object
            Animator doorAnim = doorParent.GetComponent<Animator>();
            intText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (doorAnim.GetCurrentAnimatorStateInfo(0).isName(doorOpenAnimName))
                {
                    doorAnim.ResetTrigger("open");
                    doorAnim.SetTrigger("close");
                }

                if (doorAnim.GetCurrentAnimatorStateInfo(0).isName(doorOpenAnimName))
                {
                    doorAnim.ResetTrigger("close");
                    doorAnim.SetTrigger("open");
                }

            }
        }





    }
}
