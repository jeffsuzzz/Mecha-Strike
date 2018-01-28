using UnityEngine;
using System.Collections;

public class Stage2ClearExplode : MonoBehaviour {

    public GameObject Fade;
    private int num;

	// Use this for initialization
	void Start () {
        num = 0;
        Invoke("SetActiveExplode", 1);
        Invoke("SetActiveExplode", 1.4f);
        Invoke("SetActiveExplode", 1.8f);
        Invoke("SetActiveExplode", 2f);
        Invoke("SetActiveExplode", 2.5f);
        Invoke("SetActiveExplode", 2.7f);
        Invoke("SetActiveExplode", 3f);
        Invoke("CallFade", 6.5f);
    }

    void SetActiveExplode()
    {
        GameObject explode_temp = transform.GetChild(num).gameObject;
        explode_temp.SetActive(true);
        num++;       
    }
    void CallFade()
    {
        Fade.SendMessage("PlayFadeOut");
    }

}
