using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // [SerializeField] private float speed = 1f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 0.5f;
    // [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float maxRotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 100f;
    
    [Header("Speed Boost Settings")]
    [SerializeField] private float speedIncreaseAmount = 0.5f;
    [SerializeField] private float speedIncreaseInterval = 5f;
    
    [Header("Acceleration Boost Settings")]
    [SerializeField] private float accelerationIncreaseAmount = 0.1f;
    [SerializeField] private float accelerationIncreaseInterval = 10f;
    
    private float currentRotation = 0f;
    private float currentSpeed = 1f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(IncreaseSpeedOverTime());
        StartCoroutine(IncreaseAccelerationOverTime());
    }

    void Update()
    {
        HandleInput();
    }
    
    void FixedUpdate()
    {
        Move();
    }
    
    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Calculate target rotation based on input
        float targetRotation = horizontalInput * maxRotationAngle;
        
        // Smoothly rotate towards target rotation
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        // Apply rotation around Y-axis
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
    }
    
    private void Move()
    {
        // Always move forward (in the direction the car is facing)

        Vector3 forwardMovement = transform.forward * currentSpeed * Time.fixedDeltaTime;
        
        // Apply movement
        rb.MovePosition(rb.position + forwardMovement);
    }
    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            currentSpeed = Mathf.Min(currentSpeed + speedIncreaseAmount, maxSpeed);
            Debug.Log($"Speed increased! Current speed: {currentSpeed}");
        }
    }
    
    private IEnumerator IncreaseAccelerationOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(accelerationIncreaseInterval);
            acceleration += accelerationIncreaseAmount;
            Debug.Log($"Acceleration increased! Current acceleration: {acceleration}");
        }
    }
}
