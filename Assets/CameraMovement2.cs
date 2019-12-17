using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System;

public class CameraMovement2 : MonoBehaviour
{
    public GameObject text;
    public Text CurrentStateText;
    public Text RightLeftText;
    public InputField Field;
    string st;
    char[] starr;
    public float Speed = 6f;
    public GameObject LeftObj;
    public GameObject RightObj;
    Vector3 LastPosLeft;
    Vector3 LastPosRight;
    public AudioSource Audio;
    Regex regex;
    Match match;
    public GameObject Head;
    string CurrentState;

    public GameObject PopUpMenu;
    public Text Message;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = "q0";
        LastPosLeft = LeftObj.transform.position;
        LastPosRight = RightObj.transform.position;
        Field.gameObject.SetActive(true);
        text.gameObject.SetActive(false);
        Head.SetActive(false);
        PopUpMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        CurrentStateText.text = CurrentState;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Audio.Play();
            TuringMachineStart();
            //Right();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Left()
    {
        this.transform.position += new Vector3(-6, 0f, 0f);
        RightLeftText.text = "Left";
        if (this.transform.position.x == LastPosLeft.x)
        {
            LeftIntantiate();
        }
    }
    void Right()
    {
        this.transform.position += new Vector3(6, 0f, 0f);
        RightLeftText.text = "Right";
        if (this.transform.position.x == LastPosRight.x)
        {
            RightInstantiate();
        }
    }
    void LeftIntantiate()
    {
        GameObject objLeft = Instantiate(LeftObj, LastPosLeft + new Vector3(-Speed, LeftObj.transform.position.y, LeftObj.transform.position.z), Quaternion.identity) as GameObject;
        LastPosLeft = objLeft.transform.position;
        objLeft.GetComponentInChildren<TextMesh>().text = "#";

    }
    void RightInstantiate()
    {
        GameObject objRight = Instantiate(RightObj, LastPosRight + new Vector3(Speed, LeftObj.transform.position.y, LeftObj.transform.position.z), Quaternion.identity) as GameObject;
        LastPosRight = objRight.transform.position;
        objRight.GetComponentInChildren<TextMesh>().text = "#";
    }
    void RWrite(string Write)
    {

        Ray ray = new Ray(this.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            TextMesh txt = hit.collider.gameObject.GetComponentInChildren<TextMesh>();

            txt.text = Write;
        }

    }
    string RRead()
    {
        Ray ray = new Ray(this.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            TextMesh txt = hit.collider.gameObject.GetComponentInChildren<TextMesh>();
            Animator anim = hit.collider.gameObject.GetComponent<Animator>();
            anim.Play("rotationCube");

            return txt.text;
        }
        else
        {
            return "";
        }
    }
    public void OK()
    {
        st = Field.text;
        starr = st.ToCharArray();
        int l = 0;
        regex = new Regex("^[a-c]+$");
        for (int i = 0; i <= st.Length - 1; i++)
        {

            if (!regex.IsMatch(starr[i].ToString()))
            {
                Debug.Log("Not Matched");
            }
            else
            {
                l++;
            }
        }

        if (st.Length != 0 && l == st.Length)
        {
            Head.SetActive(true);
            text.gameObject.SetActive(true);
            LeftIntantiate();
            for (int j = 0; j <= st.Length - 1; j++)
            {
                GameObject objRight = Instantiate(RightObj, LastPosRight + new Vector3(Speed, LeftObj.transform.position.y, LeftObj.transform.position.z), Quaternion.identity) as GameObject;
                LastPosRight = objRight.transform.position;
                objRight.GetComponentInChildren<TextMesh>().text = starr[j].ToString();
            }
            Field.gameObject.SetActive(false);
        }
        else
        {
            PopUpMenu.SetActive(true);
            Message.text = "Please Try With the Valid String e.g., Combination of a,b and c";
        }
    }
    public void oKerror()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void TuringMachineStart()
    {
        if (CurrentState == "q0")
        {
            if (RRead() == "0")
            {
                RWrite("a");
                Right();
                CurrentState = "q1";
            }
            else if (RRead() == "1")
            {
                CurrentState = "q6";
                RWrite("b");
                Right();

            }
            else if (RRead() == "$")
            {
                RWrite("$");
                CurrentState = "q11";
                Right();
            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q1")
        {
            if (RRead() == "0")
            {
                //RWrite("0");
                Right();
                CurrentState = "q1";
            }
            else if (RRead() == "1")
            {
                CurrentState = "q1";
                Right();
                // RWrite("1");
            }
            else if (RRead() == "$")
            {
                RWrite("$");
                CurrentState = "q2";
                Right();
            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q2")
        {
            if (RRead() == "1")
            {
                RWrite("d");
                Right();
                CurrentState = "q3";
            }
            else if (RRead() == "c")
            {
                CurrentState = "q2";
                Right();
                // RWrite("c");
            }
            else if (RRead() == "d")
            {
                CurrentState = "q2";
                Right();
                // RWrite("d");
            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q3")
        {
            if (RRead() == "1")
            {
                RWrite("1");
                Right();
                CurrentState = "q3";
            }
            else if (RRead() == "0")
            {
                RWrite("0");
                Right();
                CurrentState = "q3";
            }
            else if (RRead() == "$")
            {
                RWrite("$");
                CurrentState = "q4";
                Right();

            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q4")
        {
            if (RRead() == "0")
            {
                RWrite("e");
                CurrentState = "q5";
                Left();

            }
            else if (RRead() == "f")
            {
                RWrite("f");
                Right();
                CurrentState = "q4";
            }
            else if (RRead() == "e")
            {
                RWrite("e");
                Right();
                CurrentState = "q4";
            }

            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q5")
        {
            if (RRead() == "f" || RRead() == "c" || RRead() == "d" || RRead() == "e" || RRead() == "$" || RRead() == "1" || RRead() == "0")
            {
                // RWrite("d");
                Left();
                CurrentState = "q5";
            }
            else if (RRead() == "a")
            {
                // RWrite("d");
                Right();
                CurrentState = "q0";
            }

            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q6")
        {
            if (RRead() == "1")
            {
                // RWrite("b");
                Right();
                CurrentState = "q6";
            }
            else if (RRead() == "0")
            {
                // RWrite("d");
                Right();
                CurrentState = "q6";
            }
            else if (RRead() == "$")
            {
                CurrentState = "q7";
                Right();
                //RWrite("b");
            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q7")
        {
            if (RRead() == "d")
            {
                // RWrite("d");
                Right();
                CurrentState = "q7";
            }
            else if (RRead() == "c")
            {
                // RWrite("d");
                Right();
                CurrentState = "q7";
            }
            else if (RRead() == "0")
            {
                RWrite("c");
                CurrentState = "q8";
                Right();

            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q8")
        {
            if (RRead() == "1")
            {
                // RWrite("d");
                Right();
                CurrentState = "q8";
            }
            else if (RRead() == "0")
            {
                // RWrite("d");
                Right();
                CurrentState = "q8";
            }
            else if (RRead() == "$")
            {
                CurrentState = "q9";
                Right();
                //RWrite("b");
            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q9")
        {
            if (RRead() == "f")
            {
                // RWrite("d");
                Right();
                CurrentState = "q9";
            }
            else if (RRead() == "e")
            {
                // RWrite("d");
                Right();
                CurrentState = "q9";
            }
            else if (RRead() == "1")
            {
                RWrite("f");
                CurrentState = "q10";
                Left();

            }
            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q10")
        {
            if (RRead() == "f" || RRead() == "c" || RRead() == "d" || RRead() == "e" || RRead() == "$" || RRead() == "1" || RRead() == "0")
            {
                // RWrite("d");
                Left();
                CurrentState = "q10";
            }
            else if (RRead() == "b")
            {
                // RWrite("d");
                Right();
                CurrentState = "q0";
            }

            else
            {
                Decision();
            }
        }
        else if (CurrentState == "q11")
        {
            if (RRead() == "f" || RRead() == "c" || RRead() == "d" || RRead() == "e" || RRead() == "b" || RRead() == "a" || RRead() == "$")
            {
                // RWrite("d");
                Right();
                CurrentState = "q11";
            }
            else if (RRead() == "#")
            {
                // RWrite("d");
                Right();
                CurrentState = "q12";
                Decision();
            }

            else
            {
                Decision();
            }
        }
    }

    private void Decision()
    {
        if (CurrentState == "q12")
        {
            Debug.Log("String Accepted");
            GameObject[] Find = GameObject.FindGameObjectsWithTag("Object");
            for (int i = 0; i <= Find.Length - 1; i++)
            {
                Find[i].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            GameObject[] Find = GameObject.FindGameObjectsWithTag("Object");
            for (int i = 0; i <= Find.Length - 1; i++)
            {
                Find[i].GetComponent<Renderer>().material.color = Color.red;
            }
            Debug.Log("String Rejected");
        }
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
