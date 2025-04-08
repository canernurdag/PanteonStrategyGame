using UnityEngine;

public class CreatedGridData 
{
	public readonly Vector3 CenterPosition;
	public readonly float MinX, MinY, MaxX, MaxY;

	public CreatedGridData(Vector3 centerPosition, float minX, float minY, float maxX, float maxY)
	{
		CenterPosition = centerPosition;
		MinX = minX;
		MinY = minY;
		MaxX = maxX;
		MaxY = maxY;
	}
}
