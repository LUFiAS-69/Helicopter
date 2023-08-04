//
// set this on camera
// assign the target object on lookAt transform
// assign the offset position of target object on follow transform
// follow transform is needed only for 3D
// you can leave it (follow transform) empty when working with 2D or top down view
//

using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public WorkType workType;
    public Method method;

    public Transform follow;
    public Transform lookAt;
    public float speed;

    Camera cam;
    Vector3 offset;
    private void Start()
    {
        cam = GetComponent<Camera>();
        switch (workType)
        {
            case WorkType._2D:
                cam.orthographic = true;
                offset = cam.transform.position;
                break;
            case WorkType._3D:
                cam.orthographic = false;
                break;
            case WorkType._TopDown:
                cam.orthographic = true;
                offset = cam.transform.position;
                break;
            default:
                break;
        }

    }

    private void Update()
    {
        if (method != Method.Update) return;

        //2d
        if (workType == WorkType._2D)
        {
            WorkType2D(Time.deltaTime);
        }

        //3d
        if (workType == WorkType._3D)
        {
            WorkType3D(Time.deltaTime);
        }

        //top down
        if (workType == WorkType._TopDown)
        {
            WorkTypeTopDown(Time.deltaTime);
        }

    }

    private void LateUpdate()
    {
        if (method != Method.LateUpdate) return;

        //2d
        if (workType == WorkType._2D)
        {
            WorkType2D(Time.deltaTime);
        }

        //3d
        if (workType == WorkType._3D)
        {
            WorkType3D(Time.deltaTime);
        }

        //top down
        if (workType == WorkType._TopDown)
        {
            WorkTypeTopDown(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (method != Method.FixedUpdate) return;

        //2d
        if(workType == WorkType._2D)
        {
            WorkType2D(Time.fixedDeltaTime);
        }

        //3d
        if(workType == WorkType._3D)
        {
            WorkType3D(Time.fixedDeltaTime);
        }

        //top down
        if(workType == WorkType._TopDown)
        {
            WorkTypeTopDown(Time.fixedDeltaTime);
        }
    }

    void WorkType3D(float _time)
    {
        transform.position = Vector3.Lerp(
            transform.position, follow.position, speed * _time);
        transform.LookAt(lookAt);
    }

    void WorkTypeTopDown(float _time)
    {
        transform.position = Vector3.Lerp(
            transform.position, lookAt.position + offset, speed * _time);
    }

    void WorkType2D(float _time)
    {
        transform.position = Vector3.MoveTowards(transform.position,
            lookAt.position + offset, speed * _time);
    }
}

public enum WorkType
{ _2D, _3D, _TopDown }

public enum Method
{   Update, FixedUpdate, LateUpdate }