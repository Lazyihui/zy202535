using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour

{

    public float onTime; // 激光开启时间
    public float offTime; // 激光关闭时间

    private float timer; // 计时器

    private Renderer laserRenderer; // 渲染器

    private Light laserLight; // 灯光
    // Start is called before the first frame update
    void Awake()
    {
        laserRenderer = GetComponent<Renderer>();
        laserLight = GetComponent<Light>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(laserRenderer.enabled&&timer>=onTime)
            SwitchBeam();
        if(!laserRenderer.enabled&&timer>=offTime)
            SwitchBeam();
    }

    void SwitchBeam()
    {
        timer = 0f;
        
        // 激光开启
        laserRenderer.enabled = !laserRenderer.enabled;
        laserLight.enabled = !laserLight.enabled;

    }
}
