using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Vars
    private float basePower = 0.1f;

    [SerializeField] private int harpoonCount;
    
    [SerializeField] private bool canShoot = true;
        
    [SerializeField] private GameObject GO_harpoon;

    [SerializeField] private float maxForce;

    private float force;
    
    [SerializeField] private Transform shotPoint;

    [SerializeField] private GameObject point;

    private GameObject[] points;
    
    [SerializeField] private int numOfPoints;
    
    [SerializeField] private float spaceBetweeenPoints;

    private Vector3 mouseDirection;
    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        
        //Shoot Harpoon
        if (Input.GetMouseButtonDown(0) && canShoot && harpoonCount > 0) Shoot();
        
        for (int i = 0; i < numOfPoints; i++)
        {
            points[i].transform.position = PointPos(i * spaceBetweeenPoints);
        }
        
        //
        force = Mathf.Sqrt(Mathf.Sqrt((mouseDirection.x * mouseDirection.x) + ( mouseDirection.y * mouseDirection.y)));
        if (force >= maxForce) force = maxForce;
    }
    

    private void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mouseDirection.z = 5.23f;

        Vector3 objectPos = UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
        
        mouseDirection.x = mousePos.x - objectPos.x;
        mouseDirection.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Shoot()
    {
        GameObject GO_shotHarpoon = Instantiate(GO_harpoon, shotPoint.position, shotPoint.rotation);
        GO_shotHarpoon.GetComponent<Rigidbody>().velocity = transform.right * force;
        canShoot = false;
        harpoonCount -= 1;
    }

    public void prepareHarpoon()
    {
        
        canShoot = true;
    }
    private Vector3 PointPos(float t)
    {
        Vector3 pos = shotPoint.position + (mouseDirection.normalized * force * t) + 0.5f * Physics.gravity * (t * t);
        return pos;
    }
}
