using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerGo : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public float rot;
    public float MaxAn;
    public float MaxVel;
    public GameObject body;
    public float slopeVel;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        /*
        if (OVRPlugin.productName == "Oculus Go")
        {//Goの時だけ
         //アクティブなcontrollerの取得
            var controller = OVRInput.GetActiveController() & (OVRInput.Controller.LTrackedRemote | OVRInput.Controller.RTrackedRemote);
            if (controller != OVRInput.Controller.None)
            {//コントローラが存在すれば

                //取得したコントローラーから回転角度の取得
                var rot = OVRInput.GetLocalControllerRotation(controller);

                //自身の向きを取得
                float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);//y軸の回転を取得
                Vector3 dir = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));//xとzの角度に変換
                if (rb.velocity.magnitude <= MaxVel)
                {//速度制限
                    if (10 < rot.x && rot.x < 90)//上キー押している間
                    {
                        rb.AddForce(transform.forward * speed);
                    }
                    else if (-90 < rot.x && rot.x < -10)//下キー押している間
                    {
                        rb.AddForce(transform.forward * -speed);//ブレーキ、バック
                    }
                }

                if (rb.angularVelocity.magnitude <= MaxAn)
                {//角速度制限
                    if (-90 < rot.z && rot.z < -10)//左キー押している間
                    {
                        rb.AddTorque(new Vector3(0, -rot, 0) * Mathf.PI);//左に曲がる
                    }
                    else if (10 < rot.z && rot.z < 90)//右キー押している間
                    {
                        rb.AddTorque(new Vector3(0, rot, 0) * Mathf.PI);//右に曲がる
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
                    Debug.Log(localAngVel.y);
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
                else if (slope > -50 && slope < 45 && localAngVel.y < 0.0)//左に曲がる
                {
                    if (slope < 0)//左に傾いているとき
                    {
                        body.transform.Rotate(0, 0, -localAngVel.y * slopeVel * 2);
                    }
                    else//右に傾いているとき
                    {
                        body.transform.Rotate(0, 0, -localAngVel.y * slopeVel);
                    }
                }
                else if (slope > -45 && slope < 50 && localAngVel.y > 0.0)//右に曲がる
                {
                    if (slope > 0)//右に傾いているとき
                    {
                        body.transform.Rotate(0, 0, -localAngVel.y * slopeVel * 2);
                    }
                    else//左に傾いているとき
                    {
                        body.transform.Rotate(0, 0, -localAngVel.y * slopeVel);
                    }
                }
            }
        }
        */
    }


    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "wall")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
