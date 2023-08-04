using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] Vector3 axis;

    private void FixedUpdate()
    {
        transform.Rotate(axis * speed);
    }
}
