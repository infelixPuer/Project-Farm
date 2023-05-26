using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float _speed;
    
    [SerializeField]
    private float _cameraRotation;

    private Camera _cam;
    
    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey("w"))
            transform.position += _speed * Time.deltaTime * transform.forward;

        if (Input.GetKey("s"))
            transform.position += _speed * Time.deltaTime * -transform.forward;

        if (Input.GetKey("a"))
            transform.position += _speed * Time.deltaTime * -transform.right;

        if (Input.GetKey("d"))
            transform.position += _speed * Time.deltaTime * transform.right;

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up, mouseX * _cameraRotation * Time.deltaTime, Space.World);
        _cam.transform.Rotate(new Vector3(-1f, 0, 0), mouseY * _cameraRotation * Time.deltaTime, Space.Self);
    }
}
