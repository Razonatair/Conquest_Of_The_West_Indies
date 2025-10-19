using Godot;
using Godot.NativeInterop;
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

    private bool m_Key_Enter = false;

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
            switch (keyEvent.Keycode)
            {
                case Key.Enter:
                    {
                        m_Key_Enter = true;
                    }
                    break;
            }
        }
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
        if(m_Key_Enter == true)
        {
            EndTurn();
            m_Key_Enter = false;
        }
    }

    private void EndTurn()
    {
        UnitManager.ProcessTurn();
    }
}
