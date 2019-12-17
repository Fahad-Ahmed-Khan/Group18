using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 4f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) & this.transform.position.x > -6)
        {
            this.transform.position += new Vector3(-speed, 0, 0);
            Debug.Log(this.transform.position.x);
        }
        if (Input.GetMouseButtonDown(1) & this.transform.position.x < 10)
        {
            this.transform.position += new Vector3(speed, 0, 0);
        }
    }
}
