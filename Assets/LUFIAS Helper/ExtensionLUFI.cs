using System.Collections.Generic;
using UnityEngine;

public static class ExtensionLUFI
{
    public static void ResetTransform(this Transform transform)
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public static void KillAllChild(this Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }

    public static List<Transform> GetAllChild(this Transform transform)
    {
        List<Transform> list = new List<Transform>(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i));
        }

        return list;
    }

    #region Transform Position

    public static void SetPosX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetPosY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetPosZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    //public static void SetPosXY(this Transform transform, float x, float y)
    //{
    //    transform.position = new Vector3(x,y, transform.position.z);
    //}

    //public static void SetPosXZ(this Transform transform, float x, float z)
    //{
    //    transform.position = new Vector3(x, transform.position.y, z);
    //}

    //public static void SetPosYZ(this Transform transform, float y, float z)
    //{
    //    transform.position = new Vector3(transform.position.x, y, z);
    //}

    public static void ResetPos(this Transform transform)
    {
        transform.position = Vector3.zero;
    }

    public static void ResetPosX(this Transform transform)
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    public static void ResetPosY(this Transform transform)
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public static void ResetPosZ(this Transform transform)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    //public static void ResetPosXY(this Transform transform)
    //{
    //    transform.position = new Vector3(0, 0, transform.position.z);
    //}

    //public static void ResetPosXZ(this Transform transform)
    //{
    //    transform.position = new Vector3(0, transform.position.y, 0);
    //}

    //public static void ResetPosYZ(this Transform transform)
    //{
    //    transform.position = new Vector3(transform.position.x, 0, 0);
    //}


    #endregion

    #region Transform Rotation
    public static void SetRotX(this Transform transform, float x)
    {
        Quaternion q = Quaternion.Euler(x, transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);

        transform.rotation = q;
    }

    public static void SetRotY(this Transform transform, float y)
    {
        Quaternion q = Quaternion.Euler(transform.rotation.eulerAngles.x, y,
            transform.rotation.eulerAngles.z);

        transform.rotation = q;
    }

    public static void SetRotZ(this Transform transform, float z)
    {
        Quaternion q = Quaternion.Euler(transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y, z);

        transform.rotation = q;
    }

    public static void ResetRot(this Transform transform)
    {
        transform.rotation = Quaternion.identity;
    }

    #endregion

    #region Transform Scale
    public static void SetLocalScaleX(this Transform transform, float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y,
            transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform transform, float scaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform transform, float scaleZ)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);
    }

    public static void ResetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }

    #endregion

}
