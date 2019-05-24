using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gyroscope : MonoBehaviour {

    enum State
    {
        Forward, Back, Stop, END
    }

    public Canvas cav;
    public Text text;

    private bool TestEvent = false;
    private State PlayerState = State.Forward;
    private Rigidbody rbd;
    private float Speed = 2f;

    private Vector3 acceler;
    private Vector3 Test;
    private Vector3 Move;

    private void Awake()
    {
        // 게임 시작시 기울기를 저장해둔다.
        // 기울기 초기값
        Test = Input.acceleration;
        Test = Test * 100f;
    }

    // Use this for initialization
    void Start () {
        rbd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        acceler = Input.acceleration * 100f;

        // 게임 시작시 정해둔 기울기와 현재 기울기를 비교해서
        // 일정 수치의 범위를 벗어나면 기울기를 감지
        if (acceler != new Vector3(0f, 0f, 0f))
        {
            if (TestEvent)
            {
                Move = Test - acceler;
                if (Mathf.Abs(Move.y) > 1.1f)
                {

                }
                else
                {
                    PlayerState = State.Stop;
                }
                Test = acceler;
            }
            else
            {
                if (acceler.y > -65)
                {
                    PlayerState = State.Forward;
                }
                else if (acceler.y <= -65)
                {
                    PlayerState = State.Back;
                }
            }
            switch(PlayerState)
            {
                case State.Forward:
                    rbd.MovePosition(transform.position + Vector3.forward * Speed * Time.deltaTime);
                    break;

                case State.Back:
                    rbd.MovePosition(transform.position + Vector3.back * Speed * Time.deltaTime);
                    break;
                case State.Stop:
                    break;
            }
        }
        Debug.Log(acceler);
    }

    // Update is called once per frame
    void Update () {
        acceler = Input.acceleration * 100f;
        text.text = " X : " +(acceler.x).ToString() + "  Y : "+(acceler.y).ToString() + "  Z : "+(acceler.z).ToString();

        if ( Input.GetMouseButton(0))
        {
            TestEvent = true;
        }
        else
        {
            TestEvent = false;
        }
    }
}
