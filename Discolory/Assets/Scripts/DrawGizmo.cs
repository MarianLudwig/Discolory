using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{

	public Color gizmoColor;

	void OnDrawGizmos()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}
}
