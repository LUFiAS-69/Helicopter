using UnityEngine;


public class FlyController : MonoBehaviour
{
    [SerializeField] float flySpeed;
    [SerializeField] float steerAmount;
    [SerializeField] float gravity;
    [SerializeField] Transform body;

    float rollMagnitude = 20;
    float pitchMagnitude = 30;
    float steer, pitch, roll;
    float horizontalInput, verticalInput;

    private void Update()
    {
        //input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //smooth
        steer += horizontalInput * steerAmount * Time.deltaTime;
        pitch = Mathf.Lerp(0, rollMagnitude, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        roll = Mathf.Lerp(0, pitchMagnitude, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //movement
        transform.position += transform.forward * verticalInput * flySpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(Vector3.up * steer + Vector3.forward * roll);
        body.localRotation = Quaternion.Euler(Vector3.right * pitch);

        //up-down
        if (Input.GetKey(KeyCode.E)) transform.Translate(Vector3.up * (flySpeed / 5) * Time.deltaTime);
        else if (Input.GetKey(KeyCode.Q)) transform.Translate(Vector3.down * (flySpeed / 5) * Time.deltaTime);

        //gravity
        else transform.Translate(Vector3.down * (gravity) * Time.deltaTime);
    }

}