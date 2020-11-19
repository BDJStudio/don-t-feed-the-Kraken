using UnityEngine;

public class MovesCamera : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float posY;
    public Vector2 boundaryX, boundaryZ;

    private Vector3 startPos;
    private Camera cam;
    private float targetPosX, targetPosZ;

    public Vector3 offset;
    public bool isTrigger;
    public bool isTarget = false;
    private void Start()
    {
        cam = GetComponent<Camera>();

        //posY = target.position.x;
        //offset = new Vector3(2,posY,-4);
        isTrigger = true;
        targetPosX = transform.position.x;
        targetPosZ = transform.position.z;
    }

    public void Update()
    {
        if (isTarget && isTrigger == false)
            transform.position = target.position + offset;
        else if(isTarget == false && isTrigger)
        {
            if (Input.GetMouseButtonDown(0)) 
                startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            else if (Input.GetMouseButton(0))
            {
                float posX = cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
                float posZ = cam.ScreenToWorldPoint(Input.mousePosition).z - startPos.z;

                targetPosX = Mathf.Clamp(transform.position.x - posX, boundaryX.x, boundaryX.y); //границы камеры
                targetPosZ = Mathf.Clamp(transform.position.z - posZ, boundaryZ.x, boundaryZ.y);
            }

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosX, speed * Time.deltaTime), transform.position.y, Mathf.Lerp(transform.position.z, targetPosZ, speed * Time.deltaTime));

        }
    }
}
