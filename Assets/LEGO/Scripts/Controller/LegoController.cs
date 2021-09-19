using System.Collections;
using System.Collections.Generic;
using LEGODeviceUnitySDK;
using UnityEngine;
using System.Linq;


public enum GAMEMODES
{
    MINIFIG,
    SUBMARINE,
    TRASHGRAB
}

public class LegoController : MonoBehaviour, ILEGOGeneralServiceDelegate
{
    public string frontDeviceId = "47DA662B84900000";
    public string backDeviceId = "8D8E652B4900000";

    LEGOTechnicMotor rightLever;
    LEGOTechnicMotor leftLever;
    LEGOTechnicMotor steeringWheel;

    LEGOTechnicForceSensor rightButton;
    LEGOTechnicForceSensor leftButton;

    LEGOSingleColorLight frontLights;

    //public GameObject submarine;

    public int minButtonForce;
    public int maxButtonForce;

    public int leverDeadZone;
    public int steeringWheelDeadZone;

    //ILEGODevice frontDevice;
    //ILEGODevice backDevice;

    public DeviceHandler frontDeviceHandler;
    public DeviceHandler backDeviceHandler;

    public GAMEMODES gameMode = GAMEMODES.MINIFIG;

    public int rightButtonValue;
    public int leftButtonValue;
    public int steeringWheelValue;
    public int rightLeverValue;
    public int leftLeverValue;

    public bool rightButtonPressed = false;
    public bool leftButtonPressed = false;
    public bool steeringWheelLocked = false;

    private int rightLeverUpright = 85;
    private int leftLeverUpright = -85;


    private bool frontInited = false;
    private bool backInited = false;

    public bool controllerInitialized = false;

    public float endScore = 0;

    private static LegoController lcInstance;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (lcInstance == null)
        {
            lcInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }

        frontDeviceHandler.OnDeviceInitialized += OnFrontDeviceInitialized;
        backDeviceHandler.OnDeviceInitialized += OnBackDeviceInitialized;

        frontDeviceHandler.OnDeviceAppeared += OnSeeDevice;
        backDeviceHandler.OnDeviceAppeared += OnSeeDevice;

        frontDeviceHandler.StartScanning();
        backDeviceHandler.StartScanning();
    }

    // Update is called once per frame
    void Update()
    {

        if(frontInited && backInited && !controllerInitialized)
        {
            controllerInitialized = true;
        }

        if (controllerInitialized)
        {
            if (gameMode == GAMEMODES.SUBMARINE)
            {
                //deadzone stuff
                if (steeringWheelValue < steeringWheelDeadZone && steeringWheelValue > -steeringWheelDeadZone)
                {
                    steeringWheelValue = 0;
                }
                if (rightLeverValue < rightLeverUpright + leverDeadZone && rightLeverValue > rightLeverUpright - steeringWheelDeadZone)
                {
                    rightLeverValue = rightLeverUpright;
                }
                if (leftLeverValue < leftLeverUpright + leverDeadZone && leftLeverValue > leftLeverUpright - steeringWheelDeadZone)
                {
                    leftLeverValue = leftLeverUpright;
                }

                //Steering wheel unlocking
                if (steeringWheelLocked && steeringWheel != null)
                {
                    steeringWheelLocked = false;
                    Debug.Log("Locking Steering wheel");
                    var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
                    {
                        Position = 0,
                        Speed = 20
                    };
                    cmd.SetEndState(MotorWithTachoEndState.Drifting);
                    cmd.ExecuteImmediately = true;
                    steeringWheel.SendCommand(cmd);
                }

                //right lever unlocking
                if (!rightButtonPressed && rightButtonValue >= minButtonForce)
                {
                    rightButtonPressed = true;
                    Debug.Log("Yolo right");
                    var cmd = new LEGOTachoMotorCommon.DriftCommand();
                    cmd.ExecuteImmediately = true;
                    rightLever.SendCommand(cmd);
                }
                //right lever locking
                else if (rightButtonPressed && rightButtonValue < minButtonForce)
                {
                    rightButtonPressed = false;
                    Debug.Log("Yolo right end");
                    var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
                    {
                        Position = rightLeverValue,
                        Speed = 3
                    };
                    cmd.SetEndState(MotorWithTachoEndState.Holding);
                    cmd.ExecuteImmediately = true;
                    rightLever.SendCommand(cmd);
                }
                //left lever unlocking
                if (!leftButtonPressed && leftButtonValue >= minButtonForce)
                {
                    leftButtonPressed = true;
                    Debug.Log("Yolo right");
                    var cmd = new LEGOTachoMotorCommon.DriftCommand();
                    cmd.ExecuteImmediately = true;
                    leftLever.SendCommand(cmd);
                }
                //left lever locking
                else if (leftButtonPressed && leftButtonValue < minButtonForce)
                {
                    leftButtonPressed = false;
                    Debug.Log("Yolo right end");
                    var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
                    {
                        Position = leftLeverValue,
                        Speed = 3
                    };
                    cmd.SetEndState(MotorWithTachoEndState.Holding);
                    cmd.ExecuteImmediately = true;
                    leftLever.SendCommand(cmd);
                }

            }
            else if (gameMode == GAMEMODES.MINIFIG)
            {
                //steering wheel locking
                if (!steeringWheelLocked && steeringWheel != null)
                {
                    steeringWheelLocked = true;
                    Debug.Log("Locking Steering wheel");
                    var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
                    {
                        Position = 0,
                        Speed = 20
                    };
                    cmd.SetEndState(MotorWithTachoEndState.Holding);
                    cmd.ExecuteImmediately = true;
                    steeringWheel.SendCommand(cmd);

                    LeverReset();
                }
            }
        }
    }

    public int GetLeftLeverValue()
    {
        return leftLeverValue - leftLeverUpright;
    }

    public int GetRightLeverValue()
    {
        return rightLeverValue - rightLeverUpright;
    }

    public int GetSteeringWheelValue()
    {
        return steeringWheelValue;
    }

    public void RightLeverReset()
    {
        var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
        {
            Position = rightLeverUpright,
            Speed = 4
        };
        cmd.SetEndState(MotorWithTachoEndState.Holding);
        rightLever.SendCommand(cmd);
    }

    public void LeftLeverReset()
    {
        var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
        {
            Position = leftLeverUpright,
            Speed = 4
        };
        cmd.SetEndState(MotorWithTachoEndState.Holding);
        leftLever.SendCommand(cmd);
    }

    public void LeverReset()
    {
        LeftLeverReset();
        RightLeverReset();
    }

    public void SteeringWheelReset()
    {
        var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
        {
            Position = 0,
            Speed = 4
        };
        cmd.SetEndState(MotorWithTachoEndState.Drifting);
        steeringWheel.SendCommand(cmd);
    }

    public void ButtonPress(int amount, bool right)
    {
        if (right == true)
        {
            //Debug.Log("Right Button: " + amount);
            rightButtonValue = amount;

            
            
        }
        else if (right == false)
        {
            //Debug.Log("Left Button: " + amount);
            leftButtonValue = amount;
        }

    }

    public void LeverRotated(int amount, bool right)
    {
        if (right == true)
        {
            //Debug.Log("Right Lever: " + amount);
            if(amount > rightLeverValue + leverDeadZone || amount < rightLeverValue - leverDeadZone)
            {
                rightLeverValue = amount;
            }
        }
        else if (right == false)
        {
            //Debug.Log("Left Lever: " + amount);
            if (amount > leftLeverValue + leverDeadZone || amount < leftLeverValue - leverDeadZone)
            {
                leftLeverValue = amount;
            }
        }
    }

    public void SteeringWheelRotated(int amount)
    {
        //Debug.Log("Steering Wheel: " + amount);
        if (amount > steeringWheelValue + leverDeadZone || amount < steeringWheelValue - leverDeadZone)
        {
            steeringWheelValue = amount;
        }
    }

    public void BlinkLights()
    {
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink()
    {
        LEGOSingleColorLight.SetPercentCommand offCmd = new LEGOSingleColorLight.SetPercentCommand()
        {
            Percentage = 0
        };
        LEGOSingleColorLight.SetPercentCommand onCmd = new LEGOSingleColorLight.SetPercentCommand()
        {
            Percentage = 100
        };

        for (var i = 0; i < 3; ++i)
        {
            frontLights.SendCommand(offCmd);

            yield return new WaitForSeconds(0.1f);

            frontLights.SendCommand(onCmd);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnFrontDeviceInitialized(ILEGODevice device)
    {
        //Debug.LogFormat("OnDeviceInitialized {0}", device);
        SetUpFrontWithDevice(device);
    }

    public void OnBackDeviceInitialized(ILEGODevice device)
    {
        //Debug.LogFormat("OnDeviceInitialized {0}", device);
        SetUpBackWithDevice(device);
    }

    public void OnSeeDevice(ILEGODevice device)
    {
        if(device.DeviceID == frontDeviceId)
        {
            frontDeviceHandler.ConnectToDevice(device);
        }
        else if(device.DeviceID == backDeviceId)
        {
            backDeviceHandler.ConnectToDevice(device);
        }
    }

    public void SetUpFrontWithDevice(ILEGODevice device)
    {
        
        //this.device = device;
        Debug.LogFormat("Setting up light"); // Must be connected to port B.
        var lightServices = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeLight, 1);
        if (lightServices == null || lightServices.Count() == 0)
        {
            Debug.LogFormat("No light services found!");
        }
        else
        {
            frontLights = (LEGOSingleColorLight)(lightServices.First());
            Debug.LogFormat("Has light service {0}", frontLights);
            frontLights.UpdateCurrentInputFormatWithNewMode((int)LEGOSingleColorLight.LightMode.Percentage);

            var cmd = new LEGOSingleColorLight.SetPercentCommand();
            cmd.Percentage = 100;
            frontLights.SendCommand(cmd);
        }


        Debug.LogFormat("Setting steering motor"); // Must be connected to port D.
        var steeringMotors = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicMotorL, 3);
        if (steeringMotors == null || steeringMotors.Count() == 0)
        {
            Debug.LogFormat("No rollMotors found!");
        }
        else
        {
            steeringWheel = (LEGOTechnicMotor)steeringMotors.First();
            Debug.LogFormat("Has motor service {0}", steeringWheel);
            steeringWheel.UpdateInputFormat(new LEGOInputFormat(steeringWheel.ConnectInfo.PortID, steeringWheel.ioType, steeringWheel.PositionModeNo, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            steeringWheel.RegisterDelegate(this);
            frontInited = true;

            /*
             * var cmd = new LEGOTachoMotorCommon.SetSpeedPositionCommand()
            {
                Position = 0,
                Speed = 5
            };
            cmd.SetEndState(MotorWithTachoEndState.Drifting);
            steeringWheel.SendCommand(cmd);
            */
        }        
    }

    public void SetUpBackWithDevice(ILEGODevice device)
    {

        
        Debug.LogFormat("Setting up left lever"); // Must be connected to port B.
        var leftLevers = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicMotorXL, 1);
        if (leftLevers == null || leftLevers.Count() == 0)
        {
            Debug.LogFormat("No rollMotors found!");
        }
        else
        {
            leftLever = (LEGOTechnicMotor)leftLevers.First();
            Debug.LogFormat("Has motor service {0}", leftLever);
            leftLever.UpdateInputFormat(new LEGOInputFormat(leftLever.ConnectInfo.PortID, leftLever.ioType, leftLever.PositionModeNo, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            leftLever.RegisterDelegate(this);

            LeftLeverReset();
        }

        Debug.LogFormat("Setting up right lever"); // Must be connected to port A.
        var rightLevers = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicMotorXL, 0);
        if (rightLevers == null || rightLevers.Count() == 0)
        {
            Debug.LogFormat("No rollMotors found!");
        }
        else
        {
            rightLever = (LEGOTechnicMotor)rightLevers.First();
            Debug.LogFormat("Has motor service {0}", rightLever);
            rightLever.UpdateInputFormat(new LEGOInputFormat(rightLever.ConnectInfo.PortID, rightLever.ioType, rightLever.PositionModeNo, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            rightLever.RegisterDelegate(this);

            RightLeverReset();
        }

        

        var forceLeftSensorServices = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicForceSensor, 2);
        if (forceLeftSensorServices == null || forceLeftSensorServices.Count() == 0)
        {
            Debug.LogFormat("No force sensor services found   !");
        }
        else
        {
            leftButton = (LEGOTechnicForceSensor)(forceLeftSensorServices.First());
            Debug.LogFormat("Has forceSensor service {0}", leftButton);
            // Mode 0 - Variable force
            // Mode 1 - Binary pressed/not pressed
            // Mode 2 - not sure what this is...
            int mode = 0;
            leftButton.UpdateInputFormat(new LEGOInputFormat(leftButton.ConnectInfo.PortID, leftButton.ioType, mode, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            leftButton.RegisterDelegate(this);
        }

        var forceRightSensorServices = ServiceHelper.GetServicesOfTypeOnPort(device, IOType.LEIOTypeTechnicForceSensor, 3);
        if (forceRightSensorServices == null || forceRightSensorServices.Count() == 0)
        {
            Debug.LogFormat("No force sensor services found   !");
        }
        else
        {
            rightButton = (LEGOTechnicForceSensor)(forceRightSensorServices.First());
            Debug.LogFormat("Has forceSensor service {0}", rightButton);
            // Mode 0 - Variable force
            // Mode 1 - Binary pressed/not pressed
            // Mode 2 - not sure what this is...
            int mode = 0;
            rightButton.UpdateInputFormat(new LEGOInputFormat(rightButton.ConnectInfo.PortID, rightButton.ioType, mode, 1, LEGOInputFormat.InputFormatUnit.LEInputFormatUnitRaw, true));
            rightButton.RegisterDelegate(this);
            backInited = true;
        }

    }


    #region ILEGOGeneralService Delegates

    public void DidUpdateValueData(ILEGOService service, LEGOValue oldValue, LEGOValue newValue)
    {
        if (service == steeringWheel)
        {
            //currentPitchValue = newValue.RawValues[0
            SteeringWheelRotated((int)newValue.RawValues[0]);

        }
        else if (service == rightLever)
        {
            //currentRollValue = newValue.RawValues[0];
            LeverRotated((int)newValue.RawValues[0], true);
        }
        else if (service == leftLever)
        {
            //currentBoostValue = newValue.RawValues[0];
            LeverRotated((int)newValue.RawValues[0], false);
        }
        else if (service == rightButton)
        {
            //currentBoostValue = newValue.RawValues[0];
            ButtonPress((int)newValue.RawValues[0], true);
        }
        else if (service == leftButton)
        {
            //currentBoostValue = newValue.RawValues[0];
            ButtonPress((int)newValue.RawValues[0], false);
        }
    }

    public void DidChangeState(ILEGOService service, ServiceState oldState, ServiceState newState)
    {
        //    Debug.LogFormat("DidChangeState {0} to {1}", service, newState);
    }

    public void DidUpdateInputFormat(ILEGOService service, LEGOInputFormat oldFormat, LEGOInputFormat newFormat)
    {
        //    Debug.LogFormat("DidUpdateInputFormat {0} to {1}", service, newFormat);
    }

    public void DidUpdateInputFormatCombined(ILEGOService service, LEGOInputFormatCombined oldFormat, LEGOInputFormatCombined newFormat)
    {
        //    Debug.LogFormat("DidUpdateInputFormatCombined {0} to {1}", service, newFormat);
    }

    #endregion

}
