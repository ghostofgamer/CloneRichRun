using DG.Tweening;
using UnityEngine;

public class DootweenMovement : MonoBehaviour
{
    public Transform[] waypoints; // Массив точек, по которым будет перемещаться персонаж
    public float moveSpeed = 1f; // Скорость перемещения
    public float rotationSpeed = 1f; // Скорость поворота

    private int currentWaypointIndex = 0;

    void Start()
    {
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.DORotateQuaternion(lookRotation, rotationSpeed);

        // Перемещаем персонажа к следующей точке
        transform.DOMove(targetWaypoint.position, moveSpeed).OnComplete(() =>
        {
            currentWaypointIndex++;
            MoveToNextWaypoint();
        });
    }
}
