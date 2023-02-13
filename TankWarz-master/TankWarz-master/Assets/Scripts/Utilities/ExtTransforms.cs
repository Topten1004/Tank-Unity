using UnityEngine;

public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediateyl = false)
    {
        foreach(Transform child in t)
        {
            if (destroyImmediateyl)
                MonoBehaviour.DestroyImmediate(child.gameObject);
            else
                MonoBehaviour.Destroy(child.gameObject);
        }
    }
}