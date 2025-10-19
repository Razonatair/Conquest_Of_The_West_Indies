using Godot;
using System;

public partial class Caravel : Unit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		m_movementPoints = 4;
		m_movementPointsRemaining = 4;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
