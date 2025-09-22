using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] wheelMeshes = new Transform[4];
    public float maxTorque = 500f;
    public float maxSteerAngle = 30f;
    public float driftFactor = 0.95f;
    private InputSystem_Actions controls;
    public float steer;
    public float torque;


    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        controls.axis.Enable();
    }

    void OnDisable()
    {
        controls.axis.Disable();
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            UpdateWheelPosition(i);
        }
    }

    void UpdateWheelPosition(int i)
    {
        Vector3 pos;
        Quaternion quat;
        wheelColliders[i].GetWorldPose(out pos, out quat);
        wheelMeshes[i].position = pos;
        wheelMeshes[i].rotation = quat;
    }

    void FixedUpdate()
    {
        Vector2 steerInput = controls.axis.Steer.ReadValue<Vector2>();
        float steer = maxSteerAngle * steerInput.x;

        float accelerateInput = controls.axis.Accelerate.ReadValue<float>();
        float torque = maxTorque * accelerateInput;

        //float steer = maxSteerAngle * Input.GetAxis("Horizontal");
        //float torque = maxTorque * Input.GetAxis("Vertical");

        for (int i = 0; i < 4; i++)
        {
            if (i < 2) // For front wheels
            {
                wheelColliders[i].steerAngle = steer;
            }
            wheelColliders[i].motorTorque = torque;
        }

        Vector3 velocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().linearVelocity);
        velocity = Vector3.Scale(velocity, new Vector3(driftFactor, 1, driftFactor));
        GetComponent<Rigidbody>().linearVelocity = transform.TransformDirection(velocity);
    }
}