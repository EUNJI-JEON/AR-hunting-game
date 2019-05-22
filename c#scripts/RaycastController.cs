using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour {

	public float maxDistanceRay=100f;
	public static RaycastController instance;
	
	public Transform gunFlashTarget;
	//1.6초 지날 때까진 다음 샷 쏘지 못함.
	public float fireRate = 1.6f;
	private bool nextShot = true;
	//저격한 오브젝트 이름
	private string objName="";

	AudioSource audio;
	//다양한 오디오클립 사용해야하므로 []로 만들어줌
	public AudioClip[] clips;

	void Awake(){
		if(instance==null)
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		//새 찍어내기
		StartCoroutine(spawnNewBird());
		audio = GetComponent<AudioSource>();
	}

	public void playSound(int sound)
	{
		audio.clip = clips[sound];
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire()
	{
		if(nextShot) {
			StartCoroutine(takeShot());
			nextShot = false;
		}
	}

	private IEnumerator takeShot(){

		//everytime we allow to take a shot, we will play the sound clip of fire gun shot and then do everything we've already done.
		gunScript.instance.fireSound();

		//카메라의 중앙에 raycast firing

		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
		RaycastHit hit;

		gameController.instance.shotsPerRound--;

		int layer_mask = LayerMask.GetMask("birdLayer");
		if(Physics.Raycast(ray,out hit, maxDistanceRay,layer_mask)){
			
			objName = hit.collider.gameObject.name;
			
			Vector3 birdPosition = hit.collider.gameObject.transform.position;

			if(objName == "Bird_Asset(Clone)"){
				//play the explosion effect when we hit the bird
				GameObject Boom = Instantiate(Resources.Load("boom", typeof(GameObject))) as GameObject;
				Boom.transform.position = birdPosition;

				playSound(1);

				Destroy(hit.collider.gameObject);
				StartCoroutine(spawnNewBird());
				StartCoroutine(clearBoom());
				gameController.instance.shotsPerRound = 3;
				gameController.instance.playerScore++;
				gameController.instance.roundScore++;
			}
		}

		GameObject gunFlash = Instantiate(Resources.Load("gunFlashSmoke", typeof(GameObject))) as GameObject;
		gunFlash.transform.position = gunFlashTarget.position;

		yield return new WaitForSeconds(fireRate);
		nextShot = true;

		GameObject[] gunSmokeGroup = GameObject.FindGameObjectsWithTag("GunSmoke");
		//read for each objects in Hiararchy and find all the smoke objects and destroy them.
		foreach(GameObject theSmoke in gunSmokeGroup){
			Destroy(theSmoke.gameObject);
		}
	}

	private IEnumerator clearBoom(){
		//wait 1.5seconds till the boom is over
		yield return new WaitForSeconds(1.5f);

		GameObject[] smokeGroup = GameObject.FindGameObjectsWithTag("Boom");
		//read for each objects in Hiararchy and find all the boom objects and destroy them.
		foreach(GameObject smoke in smokeGroup){
			Destroy(smoke.gameObject);
		}
	}

	private IEnumerator spawnNewBird()
	{
		//게임 시작후 3초후에 새 만들어지고 새 저격후에 새로운 새 역시 3초 후에 만들어짐.
		yield return new WaitForSeconds(3f);

		//spawn new bird. instantiate이용해서 Resources폴더에 있는 load할 애셋 이름 넣어주면 됨. 이름 틀리지 않게 주의
		//typeof는 불러오려는 애셋의 타입 의미
		GameObject newBird = Instantiate(Resources.Load("Bird_Asset", typeof(GameObject))) as GameObject;

		//Make bird child of ImageTarget-새가 spawn되면 ImageTarget의 차일드로 들어가도록 설정
		newBird.transform.parent = GameObject.Find("ImageTarget").transform;

		//Scale the bird
		newBird.transform.localScale = new Vector3(2.2f,2.2f,2.2f);

		//Random start position. targetCollier 스크립트에 작성했던 것과 똑같음. 새가 spawn될 때마다 랜덤한 위치에서 나오도록.
		Vector3 temp;
		temp.x=Random.Range(-48f,48f);
		temp.z=Random.Range(-48f,48f);
		temp.y=Random.Range(10f,50f);
		newBird.transform.position = new Vector3(temp.x,temp.y,temp.z);

	}

}
