using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialInputs
{
    private InputControls controls;
    private InputControls.BaseMapActions baseMap;
    private InputAction mousePositionAction;
    public Vector2 MousePosition
    {
        get
        {
            return mousePositionAction.ReadValue<Vector2>();
        }
    }

    public RadialInputs()
    {
        controls = new InputControls();
        baseMap = controls.BaseMap;
        mousePositionAction = baseMap.MousePosition;

        Enable();
    }

    public void Enable()
    {
        baseMap.Enable();
    }

    public void Disable()
    {
        baseMap.Disable();
    }

}