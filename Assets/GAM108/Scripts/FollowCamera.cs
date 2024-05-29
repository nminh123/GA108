using Unity.Mathematics;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    public float FollowSpeed;
    public float Height;
    public Transform target;
    public Vector2 Max;
    public Vector2 Min;
    bool isBounded;
    // Update is called once per frame
    void Update()
    {
        if (transform.position != target.position)
        {
        Vector3 newPos = new Vector3(target.position.x,target.position.y + Height, -10f);

        newPos.x = Mathf.Clamp(target.position.x,Min.x,Max.x);
        newPos.y = Mathf.Clamp(target.position.y,Min.y,Max.y);

        transform.position = Vector3.Lerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
            
        }
    }

    void Bounding()
    {

    }
}
