using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Scripting;

[RequireComponent(typeof(PathCreator))]
public class FenceCollision : MonoBehaviour
{ 
    private PathCreator pathCreator;

    [SerializeField] private int frequency;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsTrue(frequency > 0);
        pathCreator = GetComponent<PathCreator>();
        for (var i = 1; i < pathCreator.path.localPoints.Length; i += frequency)
        {
            Vector3 point1 = pathCreator.path.localPoints[i -1] + pathCreator.transform.position;
            Vector3 point2 = pathCreator.path.localPoints[i] + pathCreator.transform.position;

            GameObject pointObject = new GameObject();
            pointObject.transform.position = (point1 + point2) / 2;
            pointObject.AddComponent<BoxCollider>();

            Vector3 direction = point2 - point1;
            float distance = direction.magnitude;
            pointObject.transform.localScale = new Vector3(distance * 1.2f, 300, 6);
            pointObject.transform.rotation = Quaternion.LookRotation(direction);
            pointObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
        }
    }
}
