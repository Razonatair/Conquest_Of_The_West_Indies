using Godot;
using System;
using System.Numerics;

public partial class Unit : Sprite2D
{
	// Statistics that all units have.
	protected int m_movementPoints = 0;
	protected int m_movementPointsRemaining = 0;

	protected Vector2I m_mapPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void setMapPosition(Vector2I mapPosition)
	{
		m_mapPosition = mapPosition;
	}
}
