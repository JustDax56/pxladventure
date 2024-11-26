using UnityEngine;

public class Movement360 : MonoBehaviour
{
    [SerializeField, Range(0f, 360f)]
    private float rotationSpeed = 90f;

    [SerializeField]
    private bool rotateClockwise = true;

    private float initialRotation;

    private void Start()
    {
        InitializeRotation();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void InitializeRotation()
    {
        initialRotation = Random.Range(0f, 360f);
    }

    private void UpdateRotation()
    {
        float rotationAngle = CalculateRotationAngle();
        ApplyRotation(rotationAngle);
    }

    private float CalculateRotationAngle()
    {
        float angle = Time.time * rotationSpeed;
        return rotateClockwise ? -angle : angle;
    }

    private void ApplyRotation(float rotationAngle)
    {
        float totalRotation = (rotationAngle + initialRotation) % 360f;
        transform.rotation = Quaternion.Euler(0f, 0f, totalRotation);
    }
}