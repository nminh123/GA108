using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    public float FollowSpeed;
    public float Height;
    public Transform target;
    bool isBounded;
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y + Height, -10f);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
    }

    void Bounding()
    {

    }
}
