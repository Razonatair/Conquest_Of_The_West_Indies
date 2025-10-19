using Godot;
using System;

public class Order
{
	public string m_Type;
	public Vector2I m_Map_Coordinates;

	public Order(string type, Vector2I coordinates)
	{
		m_Type = type;
		m_Map_Coordinates = coordinates;
	}
}
