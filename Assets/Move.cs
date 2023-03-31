using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    [SerializeField] private float velocity = 0;
    [SerializeField] private float rotateSpeed = 100;

    [SerializeField] private float maxSpeed = 5;

    [SerializeField] private float reverseSpeed = 5;

    [SerializeField] private float acceleration = 2;
    [SerializeField] private float breakingForce = 2;

    private Vector3 direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveLoop();
        RotateLoop();
    }

    void RotateLoop()
    {
        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                gameObject.transform.Rotate(new Vector3(0, -rotateSpeed, 0) * Time.deltaTime * (velocity / maxSpeed));
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                gameObject.transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime * (velocity / maxSpeed));
            }
        }
    }

    void MoveLoop()
    {
        if (Input.GetButton("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0) //forward
            {
                velocity = Mathf.Lerp(velocity, maxSpeed, Time.deltaTime * acceleration);
            }
            if (Input.GetAxisRaw("Vertical") < 0) //backwards
            {
                velocity = Mathf.Lerp(velocity, reverseSpeed, Time.deltaTime * breakingForce);
            }
        }
        gameObject.transform.Translate(direction * velocity * Time.deltaTime);
    }
}

