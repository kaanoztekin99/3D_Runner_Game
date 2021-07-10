using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //holding number of coins we triggered = score
    public static int numberOfCoins;

    //check if game is over or not 
    public static bool isGameOver = false;

    //looking for new level 
    public static bool isGoNext = false;

    //we are dividing our scene upto three ways.
    //we want our simulation make more smooth 
    [SerializeField] private float _firstLine;
    [SerializeField] private float _secondline;
    [SerializeField] private float _thirdLine;

    [SerializeField] private float _moveThreshold;
    [SerializeField] private float _speed;
    [SerializeField] private float _moveSpeed;
    

    private Rigidbody rb;

    private float _lastMoveTime;

    private Lane _lane = Lane.Second;
    enum Lane
    {
        First,
        Second,
        Third
    }

    Vector3 moveTo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        isGameOver = false;
        numberOfCoins = 0;
    }


    //we are controlling the player with touch system and mouse control system.
    private void Update()
    {
        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            float movePow = touch.deltaPosition.normalized.x;

            if (Mathf.Abs(movePow) > _moveThreshold && Time.time - _lastMoveTime > 0.5f)
            {
                _lastMoveTime = Time.time;
                if (movePow < 0)
                {
                    switch (_lane)
                    {
                        case Lane.First:
                            break;
                        case Lane.Second:
                            moveTo = new Vector3(_firstLine, transform.position.y, transform.position.z);
                            _lane = Lane.First;
                            break;
                        case Lane.Third:
                            moveTo = new Vector3(_secondline, transform.position.y, transform.position.z);
                            _lane = Lane.Second;
                            break;
                    }
                }
                if (movePow > 0)
                {
                    switch (_lane)
                    {
                        case Lane.First:

                            moveTo = new Vector3(_secondline, transform.position.y, transform.position.z);
                            _lane = Lane.Second;
                            break;
                        case Lane.Second:
                            moveTo = new Vector3(_thirdLine, transform.position.y, transform.position.z);
                            _lane = Lane.Third;
                            break;
                        case Lane.Third:
                            break;
                    }
                }
            }
        }

        Move(moveTo);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * (Time.deltaTime * _moveSpeed);
    }

    private void Move(Vector3 moveTo)
    {
        moveTo = new Vector3(moveTo.x, 0, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            isGameOver = true;
        }

        if(collision.gameObject.tag == "Finish Line")
        {
            
            if (numberOfCoins > GameController.minCoinforNextScene)
            {
                isGoNext = true;
            }
            else
            {
                isGameOver = true;
            }
        }
    }

}
