using UnityEngine;
using UnityEngine.Serialization;

public class NewBehaviourScript : MonoBehaviour
{
    [FormerlySerializedAs("LazyLake")] public GameObject lazyLake;
    [FormerlySerializedAs("StartMap")] public GameObject startMap;
    [FormerlySerializedAs("Player")] public GameObject player;
    public GameObject info;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player.SetActive(false);
        lazyLake.SetActive(true);
        player.transform.position = new Vector3(-154, 15, -19);
        startMap.SetActive(false);
        player.SetActive(true);
        info.text = "Gehe zum Villager am Haus";
    }
}
