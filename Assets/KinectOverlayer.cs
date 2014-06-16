using UnityEngine;
using System.Collections;

public class KinectOverlayer : MonoBehaviour 
{
	public Vector3 TopLeft;
	public Vector3 TopRight;
	public Vector3 BottomRight;
	public Vector3 BottomLeft;
	
	public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
	
	void Update () 
	{
		KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized())
		{
			// this.renderer.material.mainTexture = manager.GetUsersClrTex();
			
			Vector3 vRight = BottomRight - BottomLeft;
			Vector3 vUp = TopLeft - BottomLeft;
			
			int iJointIndex = (int)TrackedJoint;
			
			if(manager.IsUserDetected())
			{
				uint userId = manager.GetPlayer1ID();
				
				if(manager.IsJointTracked(userId, iJointIndex))
				{
					GameObject.Find("Obstacle").renderer.enabled = true;

					Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);
					
					// 3d position to depth
					Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);
					
					// depth pos to color pos
					Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);
					
					float scaleX = (float)posColor.x / Camera.main.pixelWidth;
					float scaleY = 1.0f - (float)posColor.y / Camera.main.pixelHeight;
					
					Vector3 vOverlayPosition = BottomLeft + ((vRight * scaleX) + (vUp * scaleY));
					GameObject.Find ("Obstacle").transform.position = new Vector3(
						vOverlayPosition.y * -1f,
						vOverlayPosition.x,
						0
						);
				}
			}
			else
			{
				GameObject.Find("Obstacle").renderer.enabled = false;
			}
		}
	}
}
