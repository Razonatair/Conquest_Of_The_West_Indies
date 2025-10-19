using Godot;
using Godot.NativeInterop;
using System;

public partial class ConquerEngine : Node2D
{
	//Reference to the game's camera.
	private Camera2D m_Camera;

    private TileMapLayer m_Map;
    private Vector2I m_MapPosition;

    private UnitManager m_UnitManager;

    private bool m_LeftMouseButtonPressed = false;
    private bool m_RightMouseButtonPressed = false;
    private Vector2 m_mousePosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		m_Camera = (Camera2D)GetNode("Camera");
        m_Map = (TileMapLayer)GetNode("RawMap");
        m_UnitManager = (UnitManager)GetNode("UnitManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        processMouseEvents();
	}

    public override void _Input(InputEvent _event)
    {
        // Records mouse button input that the Conquer Engine cares about.
        InputEventMouseButton mouseButtonEvent = _event as InputEventMouseButton;
        if (mouseButtonEvent != null)
        {
            switch (mouseButtonEvent.ButtonIndex)
            {
                case MouseButton.Left: // Left button pressed.
                    {
                        m_mousePosition = mouseButtonEvent.Position;
                        m_LeftMouseButtonPressed = true;
                    }
                    break;
            }
        }
    }

    private void processMouseEvents()
    {
        if (m_LeftMouseButtonPressed == true)
        {
            m_MapPosition = m_Map.LocalToMap(m_mousePosition);



            m_LeftMouseButtonPressed = false;
        }
        else if (m_RightMouseButtonPressed == true)
        {


            m_RightMouseButtonPressed = false;
        }
    }
}
