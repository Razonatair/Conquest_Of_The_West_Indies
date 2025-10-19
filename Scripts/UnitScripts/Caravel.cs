using Godot;
using System;

public partial class Caravel : Unit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		m_movementPoints = 4;
		m_movementPointsRemaining = 4;
        m_CurrentMapPosition = new Vector2I(2, 2);
		m_ValidDomain = 0;
		m_CargoSlots = 2;
		m_CargoSlotsRemaining = 2;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
