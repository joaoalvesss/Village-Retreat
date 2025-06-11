using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody; // Refer�ncia ao corpo do jogador (objeto que roda)
    public Camera cam;

    public float cameraFollowSpeed = 5f; // Controla qu�o lenta � a c�mara

    private float xRotation = 0f;

    void LateUpdate()
    {
        // Interpolar suavemente a rota��o Y da c�mara para seguir o jogador
        Quaternion targetRotation = Quaternion.Euler(xRotation, playerBody.eulerAngles.y, 0);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetRotation, Time.deltaTime * cameraFollowSpeed);
    }
}
