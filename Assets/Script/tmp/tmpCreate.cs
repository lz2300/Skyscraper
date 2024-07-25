using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpCreate : MonoBehaviour
{
    public bool relice = false;
    public GameObject unit;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!relice)
        {
            relice = true;
            GameObject obj= Instantiate(unit);
            obj.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
            obj.name = "Unit" + index;
            index++;
        }
    }
}
