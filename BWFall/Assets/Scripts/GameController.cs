using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool major;
    int player;
    int white;
    int black;

    public GameObject PlayerPrefab;
    public GameObject Bottom;
    MeshRenderer playerColorRenderer;

    bool menu;

    // Start is called before the first frame update
    void Start()
    {
        major = true;
        player = LayerMask.NameToLayer("player");
        white = LayerMask.NameToLayer("white");
        black = LayerMask.NameToLayer("black");
        Physics2D.IgnoreLayerCollision(player, white);
        Physics2D.IgnoreLayerCollision(player, black, false);

        playerColorRenderer = PlayerPrefab.GetComponent<MeshRenderer>();
        playerColorRenderer.sharedMaterial.color = Color.black;

        Vector3 pos = new Vector3(0, 0, -0.5f);
        GameObject Player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
        Player.name=PlayerPrefab.name;

        Camera.main.GetComponent<CamController>().setMainPlayer();

        GameObject.Find("SE").GetComponent<SEController>().setController(GameObject.Find("GameController"));
    }

    // Update is called once per frame
    void Update()
    {
        menu = GameObject.Find("GameController").GetComponent<UIController>().menu;

        // onGround & only push space => change Color
        if (!menu && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y==0)
            {
                if (GameObject.Find("Player").transform.position.y > Bottom.transform.position.y + 3.5f && major)
                {
                    playerColorRenderer.sharedMaterial.color = Color.white;
                    Physics2D.IgnoreLayerCollision(player, white, false);
                    Physics2D.IgnoreLayerCollision(player, black);
                }
                else
                {
                    playerColorRenderer.sharedMaterial.color = Color.black;
                    Physics2D.IgnoreLayerCollision(player, black, false);
                    Physics2D.IgnoreLayerCollision(player, white);
                }
                GameObject.Find("BGM").GetComponent<BGMController>().change();
                major = !major;
            }
        }
    }

    public bool getMode()
    {
        return major;
    }
}
