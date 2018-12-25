using UnityEngine;

// Defines attributes of HexTiles
public class Hex
{
	public GameObject HexGameObject;

	// Q + R + S = 0
	// S = -(Q + R)
	public readonly int Q; // Column
	public readonly int R; // Row
	public readonly int S;

	private static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

	public Hex(int q, int r)
	{
		Q = q;
		R = r;
		S = -(q + r);
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

	public int GetDistanceFrom(Hex hex)
	{
		return Mathf.Max(Mathf.Abs(Q - hex.Q), Mathf.Abs(R - hex.R), Mathf.Abs(S - hex.S));
	}
}
