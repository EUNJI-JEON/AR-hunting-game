using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour {

	private Transform targetFocus;
	// find the target object cube
	void Start () {
		targetFocus = GameObject.FindGameObjectWithTag("target").transform;
	}
	
	// Update is called once per frame
	void Update () {
		//the distance from the obect to the bird
		//this=bird(because this script is attached to the bird)
		Vector3 target = targetFocus.position - this.transform.position;

		//to test
		//magnitude는 두 오브젝트 사이의 벡터의 길이 구해줌.
		Debug.Log(target.magnitude);

		//두 물체간 거리 서로 너무 가까울 때(1보다 작을 때) 버그 해결
		if(target.magnitude<1)
		{
			targetCollider.instance.moveTarget();
		}

		transform.LookAt(targetFocus.transform);

		//새 죽고 다음 새 나오면 그 다음 새는 기존 새보다 속도가 더 느려지거나 빨라지도록 설정
		float speed = Random.Range(15f,30f);
		//bird가 x,y론 안움직이고 z방향으로만 움직임. 어차피 새는 타겟인 큐브를 따라 움직이도록 look at에 설정됨. 
		transform.Translate(0,0,speed*Time.deltaTime);

	}
}
