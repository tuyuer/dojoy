using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    //input controls
    public List<InputControl> inputControls = new List<InputControl>();

    [Header("Double Tap Settings")]
    public float doubleTapSpeed = .3f;
    private float lastInputTime = 0f;
    private string lastInputAction = "";

    public delegate void DirectionInputHandler(Vector2 dir, Vector2 dirRaw, input_action_state inputState);
    public event DirectionInputHandler onDirectionEvent;

    public delegate void InputEventHandler(string action, input_action_state actionState);
    public event InputEventHandler onInputEvent;

    public delegate void MouseEventHandler(float mouseX, float mouseY);
    public event MouseEventHandler onMouseEvent;


    void Awake()
    {
        InitKeyboardInputs();
        OnInitExternalInputs();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardControls();
        MouseEvents();
    }

    void KeyboardControls()
    {
        //for test
        if (Input.GetMouseButton(0))
        {
            onInputEvent(InputActionNames.O, input_action_state.press);
        }

        bool doubleTapState = false;

        foreach (InputControl inputControl in inputControls)
        {
            if (onInputEvent == null) return;

            //on keyboard key down
            if (Input.GetKeyDown(inputControl.key))
            {
                doubleTapState = DetectDoubleTap(inputControl);
                onInputEvent(inputControl.action, input_action_state.press);
            }

            //on keyboard key up
            if (Input.GetKeyUp(inputControl.key))
            {
                onInputEvent(inputControl.action, input_action_state.release);
            }
        }

        //Store the input axes.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float hRaw = Input.GetAxisRaw("Horizontal");
        float vRaw = Input.GetAxisRaw("Vertical");


        input_action_state actionState = input_action_state.release;
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            actionState = input_action_state.hold;
        }
        else if (Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S))
        {
            actionState = input_action_state.press;
        }
        onDirectionEvent(new Vector2(h, v), new Vector2(hRaw, vRaw), actionState);
    }

    void MouseEvents()
    {
        if (onMouseEvent != null)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            onMouseEvent(mouseX, mouseY);
        }
    }

    //returns true if a key double tap is detected
    bool DetectDoubleTap(InputControl inputControl)
    {
        bool doubleTapDetected = ((Time.time - lastInputTime < doubleTapSpeed) && (lastInputAction == inputControl.action));
        lastInputAction = inputControl.action;
        lastInputTime = Time.time;
        return doubleTapDetected;
    }


    void InitKeyboardInputs()
    {
        //attack
        inputControls.Add(new InputControl(InputActionNames.O, KeyCode.J));
        inputControls.Add(new InputControl(InputActionNames.X, KeyCode.K));
    }

    virtual public void OnInitExternalInputs()
    {

    }
}
