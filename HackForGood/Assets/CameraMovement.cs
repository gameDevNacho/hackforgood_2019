using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 direction = Vector3.zero;
    direction.x = Input.GetAxis("Horizontal") * -5;
    direction.z = Input.GetAxis("Vertical") * -5;
    transform.position = transform.position + direction * Time.deltaTime;
    }
}
