﻿using UnityEngine;

public abstract class Hex
{
	public Unit Unit { get; private set; }
	public IHexView View { get; private set; }
	
	// Q + R + S = 0
	// S = -(Q + R)
	public readonly int Q; // Column
	public readonly int R; // Row
	public readonly int S;

	private static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

	protected Hex(int q, int r)
	{
		Q = q;
		R = r;
		S = -(q + r);
	}

	public void Spawn()
	{
		MapManager.Instance.SpawnHex(this);
	}

	public Vector3 GetPosition()
	{
		float radius = 1f;
		float hight = radius * 2;
		float width = WIDTH_MULTIPLIER * hight;

		float vert = hight * 0.75f;
		float horiz = width;

		return new Vector3(horiz * (Q + (R % 2 / 2f)), 0, vert * R);
	}

	public int GetDistance(Hex hex)
	{
		return Mathf.Max(Mathf.Abs(Q - hex.Q), Mathf.Abs(R - hex.R), Mathf.Abs(S - hex.S));
	}

	public void SetUnit(Unit unit)
	{
		// TODO check arguments, object state, ...
		Unit = unit;
	}
}