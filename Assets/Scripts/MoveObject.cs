using UnityEngine;
using System.Collections;


public class MoveObject : MonoBehaviour
{
    public MoveState CurrentMoveState;
    public Vector3 Direction = Vector3.zero;
    public float MoveSpeed = 5f;

    public bool UseMaxDistance = false;
    public float MaxDistaneFromOrigin = 20f;
    public float NewDirDelay = 5f;

    public bool UseTimer = false;
    public float TimerDelay = 5f;
    public float NextTime = 0;

    private readonly MoveState _defaultMoveState = MoveState.Paused;
    private Vector3 _startPos;
    private bool _hasGottenNew = false;

    private Vector3 _minPositions;
    private Vector3 _maxPositions;
    private float _maxDistOffset = 20f;

    void Awake()
    {
        CurrentMoveState = _defaultMoveState;
        _startPos = transform.position;
    }

    void Start()
    {
        TerrainData terrainData = Terrain.activeTerrain.terrainData;
        _minPositions = new Vector3(0,0,0);
        _maxPositions = terrainData.size;
        print(_maxPositions);
    }

    void Update ()
	{
	    if (CurrentMoveState == MoveState.Paused)
	        return;
	    
        HandleUseMaxDistance();
        HandleUseTimer();
	    HandleMovement();
	}

    private void HandleUseMaxDistance()
    {
        if (!UseMaxDistance)
            return;

        if (Vector3.Distance(transform.position, _startPos) > MaxDistaneFromOrigin && !_hasGottenNew)
        {
            _hasGottenNew = true;
            Direction = GetRandomDir();
            Invoke("HasGottenNewToFalse", NewDirDelay);
        }
    }

    private void HasGottenNewToFalse()
    {
        _hasGottenNew = false;
    }

    private void HandleUseTimer()
    {
        if (!UseTimer)
            return;

        if (Time.time > NextTime)
        {
            NextTime += TimerDelay;
            Direction = GetRandomDir();
        }
    }


    private Vector3 GetRandomDir()
    {
        int x;
        int z;

        do
        {
            x = Random.Range(-1, 2);
            z = Random.Range(-1, 2);
        } while (x == 0 && z == 0);

        if (transform.position.x < _minPositions.x + _maxDistOffset)
            x = 1;
        if (transform.position.x > _maxPositions.x - _maxDistOffset)
            x = -1;
        if (transform.position.z < _minPositions.z + _maxDistOffset)
            z = 1;
        if (transform.position.z > _maxPositions.z - _maxDistOffset)
            x = -1;

        return new Vector3(x, 0, z);
    }

    private void HandleMovement()
    {
        transform.LookAt(transform.position + Direction);
        //        GetComponent<Rigidbody>().velocity = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1f))
        {
            if (hit.transform.tag == "Terrain")
                print(hit.transform.gameObject);
                //print(hit.transform.GetComponent<TerrainData>().GetSteepness(hit.point.x, hit.point.z));
        }

        float y = GetComponent<Rigidbody>().velocity.y;
        Vector3 move = Direction * MoveSpeed * Time.deltaTime;
        move.y = y;
        GetComponent<Rigidbody>().velocity = move;


        //transform.Translate(Direction * MoveSpeed * Time.deltaTime, Space.World);
    }
}
