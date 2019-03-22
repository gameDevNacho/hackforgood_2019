using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Limita la posicion de la camara
[RequireComponent(typeof(Camera))]
public class CameraLimits : MonoBehaviour
{
    [SerializeField]
    Transform upLeftLimit;
    [SerializeField]
    Transform downRightLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x, downRightLimit.transform.position.x, upLeftLimit.transform.position.x);
        pos.z = Mathf.Clamp(pos.z, upLeftLimit.transform.position.z, downRightLimit.transform.position.z);

        this.transform.position = pos;
    }
}
