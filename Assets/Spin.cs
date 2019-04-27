using UnityEngine;

public class Spin : MonoBehaviour{

    public float speed;
	Transform mTrans;
	void Start (){
		mTrans = transform;
	}

	void Update (){
		ApplyDelta(Time.deltaTime);
	}

	public void ApplyDelta (float delta) {
		delta *= Mathf.Rad2Deg * Mathf.PI * 2f;
		Quaternion offset = Quaternion.Euler(new Vector3(speed, 0 , 0f) * delta);
		mTrans.rotation = mTrans.rotation * offset;
	}
}