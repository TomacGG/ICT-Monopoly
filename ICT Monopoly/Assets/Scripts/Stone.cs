using System.Collections;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class Stone : MonoBehaviour
{
    public Route currentRoute;
    
    private int routePosition;
    private int steps;
    private int count;
    private int count1;
    private int move = 1;
    private int startCount;
    public int money;
    
    private bool isWhile = true;
    private bool isMoving;
    private bool Choice;
    
    public TMP_Text rolled;
    public TMP_Text shop;
    public TMP_Text bank;
    public Camera FirstPersonCamera;
    public Camera BoardCamera;
    public GameObject store;
    public GameObject player;

    void Start()
    {
        BoardCamera.enabled = false;
        
        player.transform.Rotate(0,-90,0);
        money = 5000;
    }

    public GameObject[] waypoints;
    private int current = 0;
    private float rotSpeed;
    public float speed;
    private float WPradius = 1;
    
    void Update()
    {
        if (Choice)
        {
//            if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
//            {
//                current++;
//                if (current >= waypoints.Length)
//                {
//                    current = 0;
//                }
//            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }

        bank.text = "Bank: " + money;

        spawnPoint = GameObject.Find("spawnPoint");
        if (Input.GetKeyDown(KeyCode.Space) && !Dice.rolling && !isMoving)
        {
            Dice.Clear();
            Dice.Roll("2" + "d6", "d6-white-dots", spawnPoint.transform.position, Force());
        }

        if (Dice.rolling)
        {
            isWhile = true;
            Choice = false;
            shop.text = "";
            move = 0;

            FirstPersonCamera.enabled = false;
            bank.enabled = false;
            rolled.enabled = false;
            BoardCamera.enabled = true;
        }
        else if (!Dice.rolling && !isMoving && move == 0)
        {
            steps = Dice.Value(dieType: "");
            rolled.text = "Je gooide: " + Dice.Value(dieType: "");
            
            StartCoroutine(Move(1.5f));
        }
        
        if (startCount >= 40 && !isMoving)
        {
            money += 200;
            startCount = 0 + routePosition;
        }
        
        Debug.Log(current);
    }

    IEnumerator Move(float delay)
    {
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }

        FirstPersonCamera.enabled = true;
        BoardCamera.enabled = false;
        
        bank.enabled = true;
        rolled.enabled = true;
        
        if (transform.position == waypoints[current].transform.position)
        {
            player.transform.Rotate(0, -90, 0);
        }

        if (isMoving)
        {
            yield break;
        }

        isMoving = true;
        
        startCount += steps; 

        while (steps > 0)
        {
            routePosition++;
            routePosition %= currentRoute.childNodeList.Count;

            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps--;

            if (count == 9)
            {
                player.transform.Rotate(0, 90, 0);
                count1 += 2;
                count = 0;
            }
            else
            {
                count++;
            }

            if (count1 == 3 && routePosition != 2)
            {
                current++;
                count1 = 0;
            }
            else if (current == 11)
            {
                current = 0;
            }
            else
            {
                count1++;
            }
        }
        
        move = 1;
        isMoving = false;
        
        switch (routePosition)
        {
            case 2:
                store.SetActive(true);
                break;
            case 5:
                store.SetActive(true);
                break;
            case 8:
                store.SetActive(true);
                break;
            case 12:
                store.SetActive(true);
                break;
            case 15:
                store.SetActive(true);
                break;
            case 18:
                store.SetActive(true);
                break;
            case 22:
                store.SetActive(true);
                break;
            case 25:
                store.SetActive(true);
                break;
            case 28:
                store.SetActive(true);
                break;
            case 32:
                store.SetActive(true);
                break;
            case 35:
                store.SetActive(true);
                break;
            case 38:
                store.SetActive(true);
                break;
        }
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 8f * Time.deltaTime));
    }
    
    private GameObject spawnPoint = null;
    private Vector3 Force()
    {
        Vector3 rollTarget = Vector3.zero + new Vector3(2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
        return Vector3.Lerp(spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
    }
    
    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width-520)/2, Screen.height-40, 520, 25), "");
        GUI.Label(new Rect(((Screen.width - 520) / 2)+10, Screen.height - 38, 520, 22), "Druk op 'spatie' om de dobbelstenen te gooien.");
    }
    
    public void ChoiceYes()
    {
        player.transform.Rotate(0, 90, 0);

        if (money >= 200)
        {
            Choice = true;
            money -= 200;
            shop.text = "Bedankt voor uw aankoop";
        }
        else
        {
            shop.text = "Sorry, je hebt te weinig geld!";
        }
        store.SetActive(false);
    }
    
    public void ChoiceNo()
    {
        store.SetActive(false);
    }
}
