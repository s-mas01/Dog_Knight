using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Enemy‚ÌUI‚ðCamera‚Ì•û‚ÉŒü‚¯‚½‚¢
* 
*/

public class LookAtCamera : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}