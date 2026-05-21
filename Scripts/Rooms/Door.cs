using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;

    private CameraController cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Player datang dari kiri
        if (collision.transform.position.x < transform.position.x)
        {
            cam.MoveToNewRoom(nextRoom);

            // Aktifkan room baru
            nextRoom.GetComponent<Room>().ActivateRoom(true);

            // Nonaktifkan room lama
            previousRoom.GetComponent<Room>().ActivateRoom(false);
        }
        // Player datang dari kanan
        else
        {
            cam.MoveToNewRoom(previousRoom);

            // Aktifkan room sebelumnya
            previousRoom.GetComponent<Room>().ActivateRoom(true);

            // Nonaktifkan room sekarang
            nextRoom.GetComponent<Room>().ActivateRoom(false);
        }
    }
}