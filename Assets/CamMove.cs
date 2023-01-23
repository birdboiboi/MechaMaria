using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class CamMove : ApolarMove
{
    // Start is called before the first frame update

    private Vector3 MidPointPlayers = Vector3.zero;
    public Vector3 cameraOffset = new Vector3(3, 0, 0);
    public float cameraSpeed = 5f;
    public Transform debugCube;

    private Vector3 oldPos;
    public float maxDist;
    public Vector3 diff = Vector3.zero;

    public PlayerMove Player1Move;
    public PlayerMove Player2Move;

    private Transform Player1Pos;
    private Transform Player2Pos;
    public int playerPriorityDir = 0;

    public PlayerMove leader;
    public int tempLeaderDir = 0;

    public Camera cam;



    private Queue<PlayerMove> PlayerMoveQueue = new Queue<PlayerMove>();
    void Start()
    {
        oldPos = transform.position;

        Player1Pos = Player1Move.gameObject.transform;
        Player2Pos = Player2Move.gameObject.transform;

        Player1Move.other = Player2Move;
        Player2Move.other = Player1Move;

        MidPointPlayers = Vector3.Slerp(Player1Pos.position, Player2Pos.position, .5f);//linear 

        maxDist = (Player1Move.cylStart.x + Player2Move.cylStart.x) - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        PlayerMove temp;
        PlayerMoveQueue.TryPeek(out temp);
     
        MidPointPlayers =  ((Player1Pos.position) + (Player2Pos.position))/2;//(((Cart2Cyl(Player1Pos.position) + Cart2Cyl(Player2Pos.position)) / 2));//Player1Pos.position, Player2Pos.position, .5f);//linear 
        SetDirPlayer(Player1Move);
        SetDirPlayer(Player2Move);

        diff = new Vector3((Player1Pos.position - Player2Pos.position).magnitude, 0, 0);
       
        Vector3 newPosPolar = (Cart2Cyl(MidPointPlayers) + Cart2Cyl( cameraOffset + axisRot.position + diff)) ;
        debugCube.position = (MidPointPlayers);
       
        transform.LookAt((MidPointPlayers));
        Vector3 newPos = Cyl2Cart(newPosPolar);
        transform.position = Vector3.Slerp(newPos, oldPos, 1 - (newPos - oldPos).normalized.magnitude * Time.deltaTime * cameraSpeed);
        oldPos = transform.position;
    }

    public void QueueMovePriority(PlayerMove playerToCheck, Vector3 mp)
    {
        if (playerToCheck.isMoving && !playerToCheck.addedToStack)
        {
            //Debug.Log("queued");
            playerToCheck.addedToStack = true;
            PlayerMoveQueue.Enqueue(playerToCheck);

        }
        else if (PlayerMoveQueue.Count > 0)
        {
            // Debug.Log(playerToCheck + " dir " + playerToCheck.dir);
            if (PlayerMoveQueue.Peek() == playerToCheck)
            {
                if (!playerToCheck.isMoving)
                {
                    playerToCheck.addedToStack = false;
                    PlayerMoveQueue.Dequeue();
                }
                else
                {
                    leader = playerToCheck;


                }

                ClampDist();


            }


        }
    }


    public void ClampMovement()
    {
        if ((PlayerMoveQueue.Count > 0))
        {
            if (diff.magnitude >= maxDist - 1)
            {
                Debug.Log("stopp!!!");

                PlayerMoveQueue.Peek().other.canMove = false;
                PlayerMoveQueue.Peek().movement = PlayerMoveQueue.Peek().other.movement;

            }
            else
            {
                PlayerMoveQueue.Peek().other.canMove = true;
            }
        }
    }
    public void ClampDist()
    {
        if (diff.magnitude >= maxDist)
        {
            Debug.Log("limit magnitude" + diff.magnitude + " >=" + maxDist); 

            Debug.Log("limit hit temp" + tempLeaderDir + "==leader" + leader.dir + "==other" + leader.other.dir);
            
            if (tempLeaderDir == leader.dir && (tempLeaderDir == leader.other.dir || leader.other.dir ==0))
            {
                Debug.Log("limit same dir");
                leader.other.canMove = false;
                
            }
            else
            {
                Debug.Log("limit break out");
                leader.other.canMove = true;
            }
        }
        else
        {
            tempLeaderDir = leader.dir;
        }
        
        
    }

    public void SetDirPlayer(PlayerMove playerToCheck)
    {
       
        Vector3 pos = cam.WorldToScreenPoint(playerToCheck.transform.position);
        float v = Mathf.Sign((int)((Screen.width / 2) - pos.x));
         playerToCheck.dir = (int)v;
        Debug.Log("world "+(int)v);
    }



}
