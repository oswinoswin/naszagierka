using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGravity : MonoBehaviour {
    bool toggleGravity;
    Vector3 down;
    Vector3 up;
    

	// Use this for initialization
	void Start () {
        toggleGravity = false;
        down = new Vector3(0.0f, -9.8f, 0f);
        up = new Vector3(0f, 9.8f, 0f);
        //gravity = gameObject.AddComponent<ConstantForce>();
        //gravity.force = new Vector3(0.0f, -9.81f, 0.0f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {

            toggleGravity = !toggleGravity;
            print(Physics.gravity);
            if (toggleGravity)
            {
                Physics.gravity = up;
            }
            else
            {
                Physics.gravity = down;
            }

        }          

    }
    private void FixedUpdate()
    {
        
    }
}
