using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    protected float xRotation, zRotation;

    private Vector3 velocity;
    private Vector3 angularVelocity;
    public float factor;

    // Start is called before the first frame update
    void Start()
    {
        factor = 0.99f;
        xRotation = transform.rotation.x;
        zRotation = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        transform.Rotate(angularVelocity * Time.deltaTime);

        velocity *= factor;

        angularVelocity *= factor;

        var oldY = transform.rotation.y;

        transform.rotation.eulerAngles.Set(0, oldY, 0);

        //transform.rotation = new Quaternion(0, oldY, 0, 0);
    }

    public void AddForce(Vector3 force) {
        velocity += force * Time.deltaTime;
    }

    public void AddTorque(Vector3 torque) {
        angularVelocity += torque * Time.deltaTime;
    }

}
