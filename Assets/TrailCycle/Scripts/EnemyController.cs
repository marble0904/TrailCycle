using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject player;
    public Rigidbody rb;
    public float speed;
    public float rot;
    public float MaxAn;
    public float MaxVel;
    public GameObject body;
    public float slopeVel;

    public GameObject desSound;
    public GameObject genSound;

    /*
    RaycastHit hit;

    [SerializeField]
    bool isEnable = false;

    public bool obs; 
    */

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject gse = Instantiate(genSound) as GameObject;
        gse.transform.position = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //自身の向きを取得
        float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);//y軸の回転を取得
        Vector3 dir = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));//xとzの角度に変換

        //プレイヤーの向きを取得
        float playerDir = player.transform.eulerAngles.y * (Mathf.PI / 180.0f);
        Vector3 pdir = new Vector3(Mathf.Sin(playerDir), 0, Mathf.Cos(angleDir));

        Vector3 diff = (player.transform.position - transform.position);//プレイヤーの相対座標
        Vector3 difDir = diff.normalized;//角度をもとめる
        if (rb.velocity.magnitude <= MaxVel)
        {//速度制限
            rb.AddForce(dir * speed);//アクセル
        }

        float dotf = Vector3.Dot(transform.forward, difDir);
        float dotr = Vector3.Dot(transform.right, difDir);

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(dotr);
        }
        */

        if (rb.angularVelocity.magnitude <= MaxAn)
        {//角速度制限
            if (dotf < 0)
            {
                rb.AddTorque(new Vector3(0, rot, 0) * Mathf.PI);//右に曲がる
            }
            else if (dotr > 0)
            {
                rb.AddTorque(new Vector3(0, rot, 0) * Mathf.PI);//右に曲がる
            }
            else if (dotr < 0)
            {
                rb.AddTorque(new Vector3(0, -rot, 0) * Mathf.PI);//左に曲がる
            }
        }

        //角速度によって車体を傾ける
        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        float slope = body.transform.localEulerAngles.z;
        if (slope > 180)
        {
            slope -= 360;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(localAngVel);
            Debug.Log(slope);
        }
        if (localAngVel.y <= 0.1 && localAngVel.y >= -0.1)//角速度が0なら傾きを戻す
        {
            float ret = -1.0f;
            if (slope < 1.0f && slope > -1.0f)
            {
                ret = 0;
            }
            else if (slope < 0)
            {
                ret = -ret;
            }
            body.transform.Rotate(0, 0, ret);
        }
        else if (slope > -50 && slope < 45 && localAngVel.y < 0.0)
        {
            if (slope < 0)
            {
                body.transform.Rotate(0, 0, -localAngVel.y * slopeVel * 2);
            }
            else
            {
                body.transform.Rotate(0, 0, -localAngVel.y * slopeVel);
            }
        }
        else if (slope > -45 && slope < 50 && localAngVel.y > 0.0)
        {
            if (slope > 0)
            {
                body.transform.Rotate(0, 0, -localAngVel.y * slopeVel * 2);
            }
            else
            {
                body.transform.Rotate(0, 0, -localAngVel.y * slopeVel);
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "wall")
        {
            GameObject se = Instantiate(desSound) as GameObject;
            se.transform.position = transform.position;
            GameObject.Destroy(gameObject);
        }
    }

    /*
    void OnDrawGizmos()
    {
        if (isEnable == false)
            return;

        var scale = transform.lossyScale.x * 0.5f;

        var isHit = Physics.BoxCast(transform.position, Vector3.one * scale, transform.forward, out hit, transform.rotation);
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, Vector3.one * scale * 2);
            if(hit.distance <= 10)
            {
                obs = true;
            }
            else
            {
                obs = false;
            }
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * 100);
            obs = false;
        }
    }
    */
}
