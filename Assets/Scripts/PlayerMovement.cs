using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения вперед
    public float mouseSensitivity = 0.1f; // Чувствительность мыши для сдвига влево и вправо
    public float maxRotationAngle = 45f; // Максимальный угол поворота второго объекта
    public Transform secondObject; // Второй объект, который будет поворачиваться
    public Transform rotateObject; // Второй объект, который будет поворачиваться
    private float currentRotationAngle = 0f; // Текущий угол поворота второго объекта
    private bool isRotating = false; // Флаг для отслеживания поворота
    private Quaternion targetRotation;
    public float rotationSpeed = 5f;

    [SerializeField] private float _positionSecondMax;
    [SerializeField] private float _factorSensivity;
    public List<Transform> targetTransforms = new List<Transform>();
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    private void Start()
    {
        if (targetTransforms.Count > 0)
        {
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        /*else
        {
            HandleMouseInput();
        }*/

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        secondObject.Translate(Vector3.right * mouseX);
       /*Debug.Log(secondObject.position.x);
       float clampedX = Mathf.Clamp(secondObject.position.x, 12, 16);
       secondObject.position = new Vector3(clampedX, secondObject.position.y, secondObject.position.z);*/
        
        
        float clampedX = Mathf.Clamp(secondObject.localPosition.x, -_positionSecondMax, _positionSecondMax);
        secondObject.localPosition = new Vector3(clampedX, secondObject.localPosition.y, secondObject.localPosition.z);

        if (Input.GetMouseButton(0)) 
        {
            float mouseDeltaX = Input.GetAxis("Mouse X")*_factorSensivity;
            currentRotationAngle = Mathf.Clamp(currentRotationAngle + mouseDeltaX, -maxRotationAngle, maxRotationAngle);
            rotateObject.localRotation = Quaternion.Euler(0, currentRotationAngle, 0);
            // secondObject.localRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = true;
            targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 90, 0));
        }
        
        if (isRotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }

    private void MoveToTarget()
    {
        Transform currentTarget = targetTransforms[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentTarget.position)
        {
            currentTargetIndex++;
            if (currentTargetIndex >= targetTransforms.Count)
            {
                isMoving = false;
            }
        }

        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleMouseInput()
    {
        /*float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Translate(Vector3.right * mouseX);*/

        if (Input.GetMouseButton(0))
        {
            float mouseDeltaX = Input.GetAxis("Mouse X");
            currentRotationAngle = Mathf.Clamp(currentRotationAngle + mouseDeltaX, -maxRotationAngle, maxRotationAngle);
            secondObject.localRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }
    }
}