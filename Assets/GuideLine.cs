using UnityEngine;

public class GuideLine : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + offset;
    }
}
