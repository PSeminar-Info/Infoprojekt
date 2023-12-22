using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SpielerBewegung : MonoBehaviour
{
    public float geschwindigkeit = 5f;
    public float springschub = 8f;
    public float gravitation = 20f;
    public float sprungHöhe = 3f;

    private CharacterController characterController;
    private float vertikaleGeschwindigkeit = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Die horizontalen und vertikalen Eingaben abrufen
        float horizontal = Input.GetAxis("Horizontal");
        float vertikal = Input.GetAxis("Vertical");

        // Die Bewegungsrichtung berechnen
        Vector3 bewegung = transform.right * horizontal + transform.forward * vertikal;

        // Die Geschwindigkeit auf die Bewegungsrichtung und Geschwindigkeit setzen
        characterController.Move(bewegung * geschwindigkeit * Time.deltaTime);

        // Wenn der Spieler am Boden ist und die Leertaste gedrückt wird, springen
        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vertikaleGeschwindigkeit = Mathf.Sqrt(2 * sprungHöhe * gravitation);
            }
        }

        // Gravitation auf den Spieler anwenden
        vertikaleGeschwindigkeit -= gravitation * Time.deltaTime;

        // Die vertikale Bewegung auf das Objekt anwenden
        Vector3 vertikaleBewegung = new Vector3(0f, vertikaleGeschwindigkeit, 0f) * Time.deltaTime;
        characterController.Move(vertikaleBewegung);
    }
}
