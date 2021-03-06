﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendBoom : Singleton<ExtendBoom>
{
    Rigidbody _rigid;
    public Vector3 position;
    public bool boomIsExtending;
    public float extendSpeed;
    public Vector3 originalPosition;
    public Vector3 originalRotation;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        extendSpeed = 20f;
        originalPosition = this.transform.localPosition;
        originalRotation = this.transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        position = transform.localPosition;
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }

    public void BoomExtend()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigid.AddForce(transform.forward * extendSpeed);
        /*
        Vector3 tempVect = new Vector3(0, 0, 1);
        tempVect = tempVect.normalized * extendSpeed * Time.deltaTime;
        _rigid.MovePosition(transform.position + tempVect);
        */

    }

    public void BoomShorten()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigid.AddForce(-transform.forward * extendSpeed);
        /*Vector3 tempVect = new Vector3(0, 0, 1);
        tempVect = tempVect.normalized * extendSpeed * Time.deltaTime;
        _rigid.MovePosition(transform.position - tempVect);*/
    }

    public void ExtendStationary()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }
}
