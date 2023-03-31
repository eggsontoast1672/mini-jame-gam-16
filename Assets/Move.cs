using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float rotateSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                gameObject.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                gameObject.transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                gameObject.transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime);
            }
        }
    }
}
