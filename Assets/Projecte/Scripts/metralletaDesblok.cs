using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metralletaDesblok : MonoBehaviour
{

    public float rotateSpeed;

    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (manager.pause == false)
        {
            gameObject.transform.Rotate(new Vector3(0, rotateSpeed, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
