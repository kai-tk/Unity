using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject Slider;
    Slider slider;
    public GameObject makeDungeon;
    MakeDungeon script;
    public GameObject AudioSource;
    AudioManager2 audioScript;
    public GameObject Player;
    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        slider = Slider.GetComponent<Slider>();
        script = makeDungeon.GetComponent<MakeDungeon>();
        audioScript = AudioSource.GetComponent<AudioManager2>();
        playerScript = Player.GetComponent<Player>();
    }

    public void MakeDungeon()
    {
        int size = (int)(2 * (slider.value) + 5);
        script.initiate();
        playerScript.initiate();
        script.makeDungeon(size);
        audioScript.musicStart();
    }
}
