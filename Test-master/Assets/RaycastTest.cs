using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaycastTest : MonoBehaviour {

	Vector3 touchPosWorld;
	public Text myText;
    public Text mytouchText;
	//Change me to change the touch phase used.
	TouchPhase touchPhase = TouchPhase.Began;
    public GameObject[] cubes;
    public GameObject particle;
    //	bool startMoving = false;
    private int j;
    private int count;
    public int[] countperCube; 
    void Start()
    {
        j =0;
        cubes=  GameObject.FindGameObjectsWithTag("cube");
        countperCube = new int[cubes.Length];
        countperCube[0] = 0;
        for (int i = 1; i < cubes.Length; i++)
        {
            cubes[i].SetActive(false);
            countperCube[i] = 0;
        }
        count = 0;
    }

    void Update() {
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
                count += Input.touchCount;
                // Construct a ray from the current touch coordinates
                Camera camLeft = GameObject.Find("LeftEye").GetComponent<Camera>();
                Ray rayLeft = camLeft.ScreenPointToRay(Input.GetTouch(i).position);
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				// Create a particle if hit
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit) || Physics.Raycast(rayLeft, out hit)) {
					//Instantiate(particle, transform.position, transform.rotation);     
	//				startMoving = true;
					myText.text = "Hit";
				}
                if (hit.collider != null){
					GameObject touchedObject = hit.transform.gameObject;
					myText.text += ": " + touchedObject.transform.name;
                    if (touchedObject.transform.name == cubes[j].name){
                        //startMoving = true;
                        touchedObject.SetActive(false);
                        if ((j >= cubes.Length - 1) && (!cubes[cubes.Length - 1].activeSelf))
                        {
//                            countperCube[j] = count - 1;
                            j = 0;
                            count = 0;
                            myText.text = "all counts " + countperCube[0]+ " "+ countperCube[1] + " " + countperCube[2] + " " + countperCube[3];
                            cubes[j].SetActive(true);
                        }
                        else
                        {
                            countperCube[j] = count - 1;
                            cubes[j + 1].SetActive(true);
                            j++;
//                            mytouchText.text = " Success after " + (count-1);
                            count = 0;
                        }
                        //Repeat this  
                    }
                }
            }
		}

        /*		if (startMoving) {
                    particle.transform.Translate (0, Mathf.Sin (Time.time) * 0.02f, 0, Space.World);
                }*/
    }
}
