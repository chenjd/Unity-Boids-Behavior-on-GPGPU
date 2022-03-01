//
//  Created by jiadong chen    
//  https://www.jiadongchen.com
//
//
using UnityEngine;

using Random = UnityEngine.Random;

public class GPUFlock : MonoBehaviour {

    #region Fields

    [SerializeField] private ComputeShader cshader;
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private GameObject[] boidsGo;
    [SerializeField] private int boidsCount;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float flockSpeed;
    [SerializeField] private float nearbyDis;
    private GPUBoid[] _boidsData;
    private Vector3 _targetPos = Vector3.zero;
    private int _kernelHandle;

    #endregion

    #region Methods

    private void Start()
    {
        boidsGo = new GameObject[boidsCount];
        _boidsData = new GPUBoid[boidsCount];
        _kernelHandle = cshader.FindKernel("CSMain");

        for (var i = 0; i < boidsCount; i++)
        {
            _boidsData[i] = CreateBoidData();
            boidsGo[i] = Instantiate(boidPrefab, _boidsData[i].pos, Quaternion.Euler(_boidsData[i].rot)) as GameObject;
            _boidsData[i].rot = boidsGo[i].transform.forward;
        }
    }

    private GPUBoid CreateBoidData()
    {
        var boidData = new GPUBoid();
        var pos = transform.position + Random.insideUnitSphere * spawnRadius;
        boidData.pos = pos;
        boidData.flockPos = transform.position;
        boidData.boidsCount = boidsCount;
        boidData.nearbyDis = nearbyDis;
        boidData.speed = flockSpeed + Random.Range(-0.5f, 0.5f);

        return boidData;
    }

    private void Update()
    {
        _targetPos += new Vector3(2f, 5f, 3f);
        transform.localPosition += new Vector3(
            (Mathf.Sin(Mathf.Deg2Rad * _targetPos.x) * -0.2f),
            (Mathf.Sin(Mathf.Deg2Rad * _targetPos.y) * 0.2f),
            (Mathf.Sin(Mathf.Deg2Rad * _targetPos.z) * 0.2f)
        );


        var buffer = new ComputeBuffer(boidsCount, 48);

        for (int i = 0; i < _boidsData.Length; i++)
        {
            _boidsData[i].flockPos = this.transform.position;
        }

        buffer.SetData(_boidsData);

        cshader.SetBuffer(_kernelHandle, "boidBuffer", buffer);
        cshader.SetFloat("deltaTime", Time.deltaTime);

        cshader.Dispatch(_kernelHandle, this.boidsCount, 1, 1);

        buffer.GetData(_boidsData);

        buffer.Release();

        for (int i = 0; i < _boidsData.Length; i++)
        {

            boidsGo[i].transform.localPosition = _boidsData[i].pos;

            if (!_boidsData[i].rot.Equals(Vector3.zero))
            {
                boidsGo[i].transform.rotation = Quaternion.LookRotation(_boidsData[i].rot);
            }
        }
    }

    #endregion
}
