using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Terrain.See.Scripts
{
    public class TeleportScript : MonoBehaviour
    {
        [FormerlySerializedAs("Canvas")] public GameObject canvas;
        [FormerlySerializedAs("PlayerCanvas")] public GameObject playerCanvas;
        [FormerlySerializedAs("togglemanager")] public ToggleManager toggleManager;
        public MapDiscover mapdiscover;
        [FormerlySerializedAs("Player")] public GameObject player;
        [FormerlySerializedAs("LazyLake")] public GameObject lazyLake;
        [FormerlySerializedAs("StartMap")] public GameObject startMap;
        [FormerlySerializedAs("TundraLake")] public GameObject tundraLake;

        [FormerlySerializedAs("ToggleGroup")] public ToggleGroup toggleGroup;
        [FormerlySerializedAs("TeleportText")] public Text teleportText;

        private bool _enter;

        private bool _showGUI;

        private Dictionary<string, (GameObject activeMap, Vector3 position)> _teleportLocations;

        private void Start()
        {
            canvas.SetActive(false);

            _teleportLocations = new Dictionary<string, (GameObject, Vector3)>
            {
                { "lazylake", (lazyLake, new Vector3(-154, 15, -19)) },
                { "fisherslake", (tundraLake, new Vector3(223, 31, 201)) },
                { "village", (startMap, new Vector3(861, 68, -147)) },
                { "tundracastle", (tundraLake, new Vector3(493, 56, 219)) },
                { "forest", (startMap, new Vector3(423, 41, -309)) },
                { "graveyard", (startMap, new Vector3(345, 27, -564)) },
                { "castle", (startMap, new Vector3(156, 16, -205)) }
            };
        }

        private void Update()
        {
            if (teleportText && _enter)
            {
                teleportText.gameObject.SetActive(true);
            } else
            {
                teleportText.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.F) && _enter && canvas)
            {
                OpenCanvas();
            }
        }

        private void OpenCanvas()
        {
            canvas.SetActive(true);
            playerCanvas.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            player.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _enter = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) _enter = false;
        }

        public void Teleport()
        {
            var mapName = toggleManager.GetMap();
            if (_teleportLocations.TryGetValue(mapName, out var location))
            {
                ActivateLocation(location.activeMap);
                player.transform.position = location.position;
            }
            else
            {
                Debug.Log("Fehler");
            }
            Cancel();
            _enter = false;
        }

        private void ActivateLocation(GameObject activeLocation)
        {
            lazyLake.SetActive(activeLocation == lazyLake);
            startMap.SetActive(activeLocation == startMap);
            tundraLake.SetActive(activeLocation == tundraLake);
        }


        public void Cancel()
        {
            canvas.SetActive(false);
            playerCanvas.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            player.SetActive(true);
        }
    }
}