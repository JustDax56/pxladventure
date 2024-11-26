using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField, Range(0f, 30f)]
    private float swingSpeed = 5f;

    [SerializeField, Range(-90f, 90f)]
    private float maxSwingAngle = 45f;

    private float currentAngle = 0f;
    private float swingTimer;

    private void Start()
    {
        InitializeSwingTimer();
    }

    private void Update()
    {
        UpdateSwing();
    }

    private void InitializeSwingTimer()
    {
        swingTimer = Random.Range(0f, Mathf.PI * 2);
    }

    private void UpdateSwing()
    {
        swingTimer += Time.deltaTime * swingSpeed;
        float swingAngle = Mathf.Sin(swingTimer) * maxSwingAngle;
        ApplyRotation(swingAngle);
    }

    private void ApplyRotation(float swingAngle)
    {
        Vector3 rotationAngle = new Vector3(0f, 0f, swingAngle + currentAngle);
        transform.rotation = Quaternion.Euler(rotationAngle);
    }
}