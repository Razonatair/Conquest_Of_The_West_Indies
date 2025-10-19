using Godot;
using System;

public partial class Colonist : Unit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        base._Ready();
		m_UnitType = "Colonist";
        m_movementPoints = 1;
        m_movementPointsRemaining = 1;
        m_CurrentMapPosition = new Vector2I(2, 2);
        m_ValidDomain = 0;

		//Testing.
        Unit caravel = (Unit)GetParent<UnitManager>().GetChild(0);
        GD.Print(LoadInto(caravel));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
