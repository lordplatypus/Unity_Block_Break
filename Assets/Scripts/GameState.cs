using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public enum State
    {
        Normal, //basic state / no special activated
        Horizontal, //activate ability to destroy a row
        Vertical, //activate ability to destroy a colume
        Area, //activate abilty to destroy a 3 by 3 box
    }

    public enum InputState
    {
        Enable,
        Disable,
    }

    public static State state = State.Normal;
    public static InputState inputState = InputState.Enable;
}
