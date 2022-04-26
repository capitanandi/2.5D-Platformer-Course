using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;
    [SerializeField]
    private float _speed = 1.0f;
    private float _distance;
    private bool _switching = false;

    // Start is called before the first frame update
    void Start()
    {
        _distance = Time.deltaTime * _speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _distance);
        }
        else if(_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _distance);
        }

        if(transform.position == _targetB.position)
        {
            _switching = true;
        }
        else if(transform.position == _targetA.position)
        {
            _switching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}