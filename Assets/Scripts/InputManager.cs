using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static bool s_FireInputWasHeld;

    private void LateUpdate()
    {
        s_FireInputWasHeld = GetFireInputHeld();
    }

    public static float GetXMovement()
    {
        return Input.GetAxis(GameConstants.HORIZONTAL_AXIS);
    }

    public static float GetYMovement()
    {
        return Input.GetAxis(GameConstants.VERTICAL_AXIS);
    }

    public static bool GetFireInputDown()
    {
        return Input.GetButtonDown(GameConstants.SHOOT_BTN);
    }

    public static bool GetFireInputHeld()
    {
        return Input.GetButton(GameConstants.SHOOT_BTN);
    }

    public static bool GetFireInputUp()
    {
        return !GetFireInputHeld() && s_FireInputWasHeld;
    }

    public static bool GetReloadInputDown()
    {
        return Input.GetKeyDown(GameConstants.RELOAD_BTN);
    }

    public static bool GetPauseInputDown()
    {
        return Input.GetKeyDown(GameConstants.PAUSE_BTN);
    }
}
