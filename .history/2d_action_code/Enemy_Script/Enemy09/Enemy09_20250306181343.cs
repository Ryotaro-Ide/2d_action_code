using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy09 : MonoBehaviour,IMove
{
    
    public float _Vx;
    public float _Vy;
    public float _Interval;
    public GameObject _poisonPrefab;
    private float _time;
    private int _waterShotDirection;
    private bool _isFirstMove=true;
    private GameObject _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Vector2 _tr;
    
    public void Move(float _moveSpeed){
        
    }
    private void Attack() {
        
        GameObject _poisonL=Instantiate(_poisonPrefab,transform.position, Quaternion.identity,transform);
        GameObject _poisonR=Instantiate(_poisonPrefab,transform.position, Quaternion.identity,transform);
        _poisonL.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1*_Vx,_Vy),ForceMode2D.Impulse);
        _poisonR.GetComponent<Rigidbody2D>().AddForce(new Vector2(1*_Vx,_Vy),ForceMode2D.Impulse);
        
    }
    // Start is called before the first frame update
    void Awake()
    {
        _player=GameObject.FindGameObjectWithTag("Player");
        _rb=GetComponent<Rigidbody2D>();
        _sr=GetComponent<SpriteRenderer>();
        _tr=transform.position;
        _time=0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        _time+=Time.deltaTime;
        if(_time>=_waterShotInterval){
            _time=0f;
            Attack();
        }
    }
}
