using Godot;
using System;

public partial class ConquerEngine : Node2D
{
	//Reference to the game's camera.
	private Camera2D m_Camera;

    private Map m_Map;
    private Vector2I m_MapPosition;

    private UnitManager m_UnitManager;

    private bool m_LeftMouseButtonPressed = false;
    private bool m_RightMouseButtonPressed = false;
    private Vector2 m_LocalMousePosition;

    private int m_AssignKeyPressed = 0;
    private Key[] m_KeyPressed = { Key.None, Key.None, Key.None };
    private bool m_Key_Enter = false;   // End turn
    private bool m_Key_Kp7 = false;     // Move NW
    private bool m_Key_Kp4 = false;     // Move W
    private bool m_Key_Kp1 = false;     // Move SW
    private bool m_Key_Kp9 = false;     // Move NE
    private bool m_Key_Kp6 = false;     // Move E
    private bool m_Key_Kp3 = false;     // Move SE

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		m_Camera = (Camera2D)GetNode("Camera");
        m_Map = (Map)GetNode("RawMap");
        m_UnitManager = (UnitManager)GetNode("RawMap/UnitManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        processMouseEvents();
        processKeyboardEvents();
	}

    public override void _Input(InputEvent _event)
    {
        // Records mouse button input that the Conquer Engine cares about.
        InputEventMouseButton mouseButtonEvent = _event as InputEventMouseButton;
        if (mouseButtonEvent != null && mouseButtonEvent.IsReleased())
        {
            switch (mouseButtonEvent.ButtonIndex)
            {
                case MouseButton.Left: // Left button pressed.
                    {
                        m_LocalMousePosition = m_Camera.GetLocalMousePosition() + m_Camera.Position;
                        m_LeftMouseButtonPressed = true;
                    }
                    break;
            }
        }

        // Handle keyboard input that the Conquer Engine cares about.
        InputEventKey keyEvent = _event as InputEventKey;
        if (keyEvent != null && keyEvent.IsReleased())
        {
            AssignKeyPressed(keyEvent.Keycode);
        }
    }

    private void AssignKeyPressed(Key key)
    {
        if(m_AssignKeyPressed < m_KeyPressed.Length)
        {
            m_KeyPressed[m_AssignKeyPressed] = key;
            m_AssignKeyPressed++;
        }
        else
        {
            GD.Print("Excessive keys pressed.");
        }
    }

    private void EraseKeyPressed(int index)
    {
        m_KeyPressed[index] = Key.None;
        for (int i = index; i < m_KeyPressed.Length - 1; i++)
        {
            m_KeyPressed[i] = m_KeyPressed[i + 1];
        }
        m_AssignKeyPressed--;
    }

    private void processMouseEvents()
    {
        if (m_LeftMouseButtonPressed == true)
        {
            m_MapPosition = m_Map.LocalToMap((m_LocalMousePosition));
            GD.Print(m_MapPosition);
            UnitManager.OrderSelectedUnit(new Order("MOVE", m_MapPosition));

            m_LeftMouseButtonPressed = false;
        }
        else if (m_RightMouseButtonPressed == true)
        {


            m_RightMouseButtonPressed = false;
        }
    }

    private void processKeyboardEvents()
    {
        switch(m_KeyPressed[0])
        {
            case Key.Enter:
                {
                    GD.Print("End turn");
                    EndTurn();
                    EraseKeyPressed(0);
                }
                break;
            case Key.Kp7: // Order NW move
                {
                    GD.Print("Order NW move");
                    UnitManager.OrderSelectedUnit(new Order("MOVE", 7));
                    EraseKeyPressed(0);
                }
                break;

            case Key.Kp4: // Order W move
                {
                    GD.Print("Order W move");
                    UnitManager.OrderSelectedUnit(new Order("MOVE", 4));
                    EraseKeyPressed(0);
                }
                break;

            case Key.Kp1: // SW move
                {
                    GD.Print("Order SW move");
                    UnitManager.OrderSelectedUnit(new Order("MOVE", 1));
                    EraseKeyPressed(0);
                }
                break;

            case Key.Kp9: // Order NE move
                {
                    GD.Print("Order NE move");
                    UnitManager.OrderSelectedUnit(new Order("MOVE", 9));
                    EraseKeyPressed(0);
                }
                break;

            case Key.Kp6: // Order E move
                {
                    GD.Print("Order E move");
                    UnitManager.OrderSelectedUnit(new Order("MOVE", 6));
                    EraseKeyPressed(0);
                }
                break;

            case Key.Kp3: // Order SE move
                {
                    GD.Print("Order SE move");
                    UnitManager.OrderSelectedUnit(new Order("MOVE", 3));
                    EraseKeyPressed(0);
                }
                break;
            default:
                // Do nothing.
                break;
        }
    }

    private void EndTurn()
    {
        UnitManager.ProcessTurn();
    }
}
