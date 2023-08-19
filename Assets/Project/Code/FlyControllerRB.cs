using UnityEngine;

public class FlyControllerRB : MonoBehaviour
{
    [SerializeField] float flySpeed = 25;
    [SerializeField] float steerAmount = 45;
    [SerializeField] float gravity = 50;
    [Space(10)]
    [SerializeField] Transform body;
    [SerializeField] Rigidbody rb;

    float rollMagnitude = 20;
    float pitchMagnitude = 30;
    float steer, pitch, roll;
    float horizontalInput, verticalInput;

    private void Update()
    {
        // Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Smooth moves
        steer += horizontalInput * steerAmount * Time.deltaTime;
        pitch = Mathf.Lerp(0, rollMagnitude, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        roll = Mathf.Lerp(0, pitchMagnitude, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        // Rotate and steer
        rb.MoveRotation(Quaternion.Euler(Vector3.up * steer + Vector3.forward * roll));
        body.localRotation = Quaternion.Euler(Vector3.right * pitch);
    }

    private void FixedUpdate()
    {
        // Forward movement
        rb.velocity = (transform.forward * verticalInput * flySpeed);

        if (Input.GetKey(KeyCode.E)) rb.AddForce(Vector3.up * (flySpeed/5), ForceMode.Impulse);
        else if (Input.GetKey(KeyCode.Q)) rb.AddForce(Vector3.down * (flySpeed/5), ForceMode.Impulse);
        else rb.AddForce(Vector3.down * gravity);
    }
}
