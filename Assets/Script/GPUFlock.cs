//
//  Created by jiadong chen
//  http://www.chenjd.me
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUFlock : MonoBehaviour {

    #region 字段

    public ComputeShader cshader;

    public GameObject boidPrefab;
    public int boidsCount;
    public float spawnRadius;
    public GameObject[] boidsGo;
    public GPUBoid[] boidsData;
    public float flockSpeed;
    public float nearbyDis;

    private Vector3 targetPos = Vector3.zero;
    private int kernelHandle;

    #endregion

    #region 方法

    void Start()
    {
        this.boidsGo = new GameObject[this.boidsCount];
        this.boidsData = new GPUBoid[this.boidsCount];
        this.kernelHandle = cshader.FindKernel("CSMain");


        for (int i = 0; i < this.boidsCount; i++)
        {
            this.boidsData[i] = this.CreateBoidData();
            this.boidsGo[i] = Instantiate(boidPrefab, this.boidsData[i].pos, Quaternion.Euler(this.boidsData[i].rot)) as GameObject;
            this.boidsData[i].rot = this.boidsGo[i].transform.forward;
        }
    }

    GPUBoid CreateBoidData()
    {
        GPUBoid boidData = new GPUBoid();
        Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Random.rotation, 0.3f);
        boidData.pos = pos;
        boidData.flockPos = transform.position;
        boidData.boidsCount = this.boidsCount;
        boidData.nearbyDis = this.nearbyDis;
        boidData.speed = this.flockSpeed + Random.Range(-0.5f, 0.5f);

        return boidData;
    }

    void Update()
    {

        this.targetPos += new Vector3(2f, 5f, 3f);
        this.transform.localPosition += new Vector3(
            (Mathf.Sin(Mathf.Deg2Rad * this.targetPos.x) * -0.2f),
            (Mathf.Sin(Mathf.Deg2Rad * this.targetPos.y) * 0.2f),
            (Mathf.Sin(Mathf.Deg2Rad * this.targetPos.z) * 0.2f)
        );


        ComputeBuffer buffer = new ComputeBuffer(boidsCount, 48);

        for (int i = 0; i < this.boidsData.Length; i++)
        {
            this.boidsData[i].flockPos = this.transform.position;
        }

        buffer.SetData(this.boidsData);

        cshader.SetBuffer(this.kernelHandle, "boidBuffer", buffer);
        cshader.SetFloat("deltaTime", Time.deltaTime);

        cshader.Dispatch(this.kernelHandle, this.boidsCount, 1, 1);

        buffer.GetData(this.boidsData);

        buffer.Release();

        for (int i = 0; i < this.boidsData.Length; i++)
        {

            this.boidsGo[i].transform.localPosition = this.boidsData[i].pos;

            if(!this.boidsData[i].rot.Equals(Vector3.zero))
            {
                this.boidsGo[i].transform.rotation = Quaternion.LookRotation(this.boidsData[i].rot);
            }

        }
    }


    #endregion

}
