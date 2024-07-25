using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 lastUnitPos;
    void Start()
    {
        Screen.SetResolution(Screen.height / 16 * 9, Screen.height, true);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, lastUnitPos  +2*Vector3.up, Time.deltaTime * 10);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
    }
}
