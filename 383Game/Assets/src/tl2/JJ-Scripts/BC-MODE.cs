using UnityEngine;
using UnityEngine.UI;


//this is for a menu toggle
//whatever script that defines the player getting hurt will need a bc mode bool to check for
public class BCModeToggle : MonoBehaviour
{

    [SerializeField] private Toggle toggle; 
    //reference to the UI toggle set via inspector

    void Start()
    {
        if (toggle == null)//make sure toggle is assigned/exists
        {
            toggle = GetComponent<Toggle>();
            if (toggle == null)
            {
                Debug.LogError("Toggle component not found on " + gameObject.name);
                return;
            }
        }

        //load the saved state from PlayerPrefs
        bool savedState = PlayerPrefs.GetInt("BCMode", 0) == 1;
        
        //set the toggle init state without triggering the value change event
        toggle.isOn = savedState;
        
        //add the listener to detect changes to toggle's value then handle it
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        Debug.Log("Initial BC Mode state: " + savedState);
    }

    
    public void OnToggleValueChanged(bool isOn) //called whenever toggle's value changes (on click) t/f
    {
        PlayerPrefs.SetInt("BCMode", isOn ? 1 : 0); //PlayerPrefs is persistent storage
        PlayerPrefs.Save(); //save change immediately
        Debug.Log("BC Mode changed to: " + (isOn? "True": "False"));
    }
}
