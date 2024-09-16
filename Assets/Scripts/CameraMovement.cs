using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Transform target; // Ссылка на пустой объект
    public float zOffset = 5f;
    public float yOffset = 3f;
    
    void LateUpdate()
    {
        if (player != null && target != null)
        {
            // Следование за игроком по оси X, за пустым объектом по оси Z с учетом смещения и по оси Y
            Vector3 newPosition = transform.position;
            newPosition.x = player.position.x;
            /*newPosition.y = target.position.y+yOffset;
            newPosition.z = target.position.z - zOffset;
            transform.position = newPosition;

            // Поворот вслед за пустым объектом только по оси Y
            Vector3 targetEulerAngles = target.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetEulerAngles.y, transform.rotation.eulerAngles.z);*/
        }
    }
}
