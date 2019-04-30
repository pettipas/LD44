using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float speed;
    Transform mTrans;
    public Animator ani;
    public bool started;

    void Start() {
        mTrans = transform;
        speed = Random.Range(1, 3);
    }

    void Update() {
        ApplyDelta(Time.deltaTime);
        if (Input.anyKeyDown && !started) {
            started = true;
            StartCoroutine(SlowAndStart());
        }
    }

    public void ApplyDelta(float delta) {
        delta *= Mathf.Rad2Deg * Mathf.PI * 2f;
        Quaternion offset = Quaternion.Euler(new Vector3(0 , 0, speed) * delta);
        mTrans.rotation = mTrans.rotation * offset;
    }

    public IEnumerator SlowAndStart() {

        while (speed > 0) {
            speed -= Time.smoothDeltaTime * 2.0f;
            yield return null;
        }

        ani.SafePlay("look");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("level1");
        yield break;
    }
}
