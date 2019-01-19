using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Model
{
	/// <summary>
	/// Collection of methods to interact with the mouse.
	/// </summary>
	internal class Mouse
	{
		/// <summary>
		/// Current position of the mouse on the XZ plane.
		/// </summary>
		public Vector3 Position => GetPositionXZ();
		/// <summary>
		/// Current position of the mouse on the XY plane (screen) in pixel coordinates.
		/// </summary>
		public Vector3 ScreenPosition => Input.mousePosition;
		/// <summary>
		/// Last manually saved position of the mouse.
		/// </summary>
		public Vector3 LastPosition { get; private set; }
		/// <summary>
		/// Specifies whether the mouse is dragging something.
		/// </summary>
		public bool IsDragging = false;
		/// <summary>
		/// Specifies whether the left mouse button was pressed this frame.
		/// </summary>
		public bool Down => Input.GetMouseButtonDown(0);
		/// <summary>
		/// Specifies whether the left mouse button was released this frame.
		/// </summary>
		public bool Up => Input.GetMouseButtonUp(0);
		/// <summary>
		/// Specifies how far the mouse was scrolled this frame.
		/// </summary>
		public float Scrolled => -Input.mouseScrollDelta.y / 10;

		/// <summary>
		/// Save the current mouse position at <see cref="LastPosition"/>.
		/// </summary>
		public void SavePosition() => LastPosition = Position;

		private Vector3 GetPositionXZ()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayLength = ray.origin.y / ray.direction.y;
			return ray.origin - (ray.direction * rayLength);
		}
	}
}
