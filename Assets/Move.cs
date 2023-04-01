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
    [SerializeField] private AnimationCurve velocityRoationCurve;
    [SerializeField] private AnimationCurve motorAudioPitchVsVelocity;
    [SerializeField] private AudioSource motorAudio;

    [SerializeField] private Animator anim;

    private Vector3 direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        motorAudio.pitch = motorAudioPitchVsVelocity.Evaluate(velocity / maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveLoop();
        RotateLoop();
    }

    void RotateLoop()
    {
        if (velocity < 0.5f)
        {
            return;
        }
        if (Input.GetButton("Horizontal"))
        {
            var velocityRationMultiplier = velocityRoationCurve.Evaluate(velocity / maxSpeed);
            var rotationVector = Vector3.zero;
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                anim.CrossFade("left", 0.25f);
                rotationVector = new Vector3(0, -rotateSpeed, 0);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.CrossFade("right", 0.25f);
                rotationVector = new Vector3(0, rotateSpeed, 0);
            }
            gameObject.transform.Rotate(rotationVector * Time.deltaTime * velocityRationMultiplier);
        }
        else
        {
            anim.CrossFade("center", 0.25f);
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
            motorAudio.pitch = motorAudioPitchVsVelocity.Evaluate(velocity / maxSpeed);
        }
        gameObject.transform.Translate(direction * velocity * Time.deltaTime);
    }
}

