using Godot;
using System;

public class Order
{
	public string m_Type;
	public Vector2I m_Map_Coordinates;
	public Unit m_TargetUnit;

	public Order(string type, Vector2I coordinates)
	{
		m_Type = type;
		m_Map_Coordinates = coordinates;
	}

	public Order(string type, Unit targetUnit)
	{
		m_Type = type;
		m_TargetUnit = targetUnit;
	}
}
