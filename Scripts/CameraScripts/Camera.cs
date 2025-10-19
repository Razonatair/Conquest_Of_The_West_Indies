using Godot;
using System;
public partial class Camera : Camera2D
{
    // Modifier keys' speed multiplier
    const float SHIFT_MULTIPLIER = 2.5f;

    [Export(PropertyHint.Range, "0.0f,1.0f")]
    public float sensitivity = 1f;

    // Mouse state
    private Vector2 m_mouse_position = new Vector2(0.0f, 0.0f);
    private bool m_middleMousePressed = false;
    private bool m_middleWheelUp = false;
    private bool m_middleWheelDown = false;

    // Movement state
    private Vector2 m_direction = new Vector2(0.0f, 0.0f);
    private Vector2 m_velocity = new Vector2(0.0f, 0.0f);
    private float m_acceleration = 300f;
    private float m_deceleration = -300f;
    private float m_vel_multiplier = 10f;

    // Keyboard state
    private bool m_Up_Key = false;
    private bool m_Down_Key = false;
    private bool m_Left_Key = false;
    private bool m_Right_Key = false;

    public override void _Input(InputEvent _event)
    {
        // Records mouse motion.
        InputEventMouseMotion mouseMotionEvent = _event as InputEventMouseMotion;
        if (mouseMotionEvent != null)
        {
            m_mouse_position = mouseMotionEvent.Relative;
        }

        // Records mouse button input that the camera cares about.
        InputEventMouseButton mouseButtonEvent = _event as InputEventMouseButton;
        if (mouseButtonEvent != null)
        {
            switch (mouseButtonEvent.ButtonIndex)
            {
                case MouseButton.Middle: // Press-And-Scroll engaged
                    {
                        // TODO: Implement Press-And-Scroll
                    }
                    break;

                case MouseButton.WheelUp: // Zoom In
                    {
                        m_middleWheelUp = true;
                    }
                    break;

                case MouseButton.WheelDown: // Zoom Out
                    {
                        m_middleWheelDown = true;
                    }
                    break;
            }
        }

        // Receives key input
        InputEventKey keyEvent = _event as InputEventKey;
        if (keyEvent != null)
        {
            switch (keyEvent.Keycode)
            {
                case Key.Up:
                    {
                        m_Up_Key = keyEvent.Pressed;
                    }
                    break;

                case Key.Down:
                    {
                        m_Down_Key = keyEvent.Pressed;
                    }
                    break;

                case Key.Left:
                    {
                        m_Left_Key = keyEvent.Pressed;
                    }
                    break;
                
                case Key.Right:
                    {
                        m_Right_Key = keyEvent.Pressed;
                    }
                    break;
            }
        }
    }

    // Updates camera movement and zoom every frame.
    public override void _Process(double delta)
    {
        updateMovement((float)delta);
        updateZoom((float)delta);
    }

    // Updates camera movement
    private void updateMovement(float delta)
    {
        // Computes desired direction from key states
        m_direction = Vector2.Zero;
        float zoom = base.Zoom.X;
        if (m_Up_Key) m_direction.Y -= 500.0f / zoom;
        if (m_Down_Key) m_direction.Y += 500.0f / zoom;
        if (m_Left_Key) m_direction.X -= 500.0f / zoom;
        if (m_Right_Key) m_direction.X += 500.0f / zoom;

        Translate(m_direction * delta);
    }

    // Updates the camera's zoom level.
    private void updateZoom(float delta)
    {
        Vector2 currentZoom = base.Zoom;
        if(m_middleWheelUp) // Zoom in
        {
            currentZoom.X = Mathf.Clamp(currentZoom.X * 1.25f, 0.2f, 1f);
            currentZoom.Y = Mathf.Clamp(currentZoom.Y * 1.25f, 0.2f, 1f);
        }
        else if(m_middleWheelDown) // Zoom out
        {
            currentZoom.X = Mathf.Clamp(currentZoom.X * 0.75f, 0.2f, 1f);
            currentZoom.Y = Mathf.Clamp(currentZoom.Y * 0.75f, 0.2f, 1f);
        }
        base.Zoom = currentZoom;

        // Reset zoom records to prevent infinite zooming.
        m_middleWheelUp = false;
        m_middleWheelDown = false;
    }
}
