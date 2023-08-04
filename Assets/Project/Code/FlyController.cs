using UnityEngine;


public class FlyController : MonoBehaviour
{
    [SerializeField] float flySpeed;
    [SerializeField] float steerAmount;
    [SerializeField] Transform body;

    float rollMagnitude = 20;
    float pitchMagnitude = 30;
    float steer;

    private void Update()
    {
        //input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");

        //smooth
        steer += horizontalInput * steerAmount * Time.deltaTime;
        float pitch = Mathf.Lerp(0, rollMagnitude, Mathf.Abs(verticallInput)) * Mathf.Sign(verticallInput);
        float roll = Mathf.Lerp(0, pitchMagnitude, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //movement
        transform.position += transform.forward * verticallInput * flySpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(Vector3.up * steer + Vector3.forward * roll);
        body.localRotation = Quaternion.Euler(Vector3.right * pitch);

        //up-down
        if (Input.GetKey(KeyCode.E)) transform.Translate(Vector3.up * (flySpeed / 3) * Time.deltaTime);
        else if (Input.GetKey(KeyCode.Q)) transform.Translate(Vector3.down * (flySpeed / 3) * Time.deltaTime);
    }

}