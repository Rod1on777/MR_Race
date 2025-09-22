using UnityEngine;
using System;
using System.Collections.Generic;

public class CarControl : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    public float moveInput;
    public float steerInput;
    public float brakeInput;

    private Rigidbody carRb;

    private InputSystem_Actions controls;

    //private CarLights carLights;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        //carLights = GetComponent<CarLights>();
    }

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
        GetInputs();
        AnimateWheels();
        WheelEffects();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }
    void GetInputs()
    {
        //moveInput = Input.GetAxis("Vertical");
        //steerInput = Input.GetAxis("Horizontal");
        Vector2 steerInputControl = controls.axis.Steer.ReadValue<Vector2>();
        steerInput = steerInputControl.x;
        moveInput = controls.axis.Accelerate.ReadValue<float>();
        brakeInput = controls.axis.Brake.ReadValue<float>();
        //Debug.Log("steerInput " + steerInput);
        //Debug.Log("moveInput " + moveInput);
        //Debug.Log("breakInput " + brakeInput);
    }

    void Move()
    {
        if (moveInput > 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
                //Debug.Log("forv -> " + wheel.wheelCollider.motorTorque);
            }
        }
        else if (brakeInput >= 0 && moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = -brakeInput * 600 * maxAcceleration * Time.deltaTime;
                //Debug.Log("<- back " + wheel.wheelCollider.motorTorque);
            }
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (brakeInput < 1 && moveInput == 0 || brakeInput == 1 && moveInput == 1)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
                //Debug.Log("stop");
            }

            //carLights.isBackLightOn = true;
            //carLights.OperateBackLights();
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

            //carLights.isBackLightOn = false;
            //carLights.OperateBackLights();
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            var dirtParticleMainSettings = wheel.smokeParticle.main;

            if (brakeInput == 1 && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.linearVelocity.magnitude >= 10.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else if (wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.linearVelocity.magnitude >= 60.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else if (wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.linearVelocity.magnitude >= 30.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
}