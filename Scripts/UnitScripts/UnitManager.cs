using Godot;
using System;

public partial class UnitManager : Node2D
{
	private static Unit m_SelectedUnit = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		m_SelectedUnit = (Unit)GetNode("Caravel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public static void OrderSelectedUnit(Order order)
	{
		m_SelectedUnit.Order(order);
	}

	public static void ProcessTurn()
	{
		m_SelectedUnit.ProcessTurn();
	}
}
