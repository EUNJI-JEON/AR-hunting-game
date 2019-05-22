using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class targetCollider : DefaultTrackableEventHandler {

	//두 물체간 거리 너무 가까울 때 일어나는 버그 해결
	public static targetCollider instance;

	void Awake(){
		if(instance == null)
		{
			instance = this;
		}
	}

	// 충돌 감지하면 moveTarget실행
	void OnTriggerEnter(Collider other)
	{
		moveTarget();
	}
		
	
	public void moveTarget()
	{
		Vector3 temp;
		//맵을 벗어나서 움직이지 않도록. 
		temp.x=Random.Range(-48f,48f);
		temp.z=Random.Range(-48f,48f);

		//너무 높이 날지 않도록
		temp.y=Random.Range(10f,50f);
		transform.position = new Vector3(temp.x,temp.y,temp.z);
		//타겟 찾았을 때만 soundeffect play.
		if(DefaultTrackableEventHandler.trueFalse ==true){
			RaycastController.instance.playSound(0);
		}
	}
}
