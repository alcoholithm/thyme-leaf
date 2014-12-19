using UnityEngine;
using System.Collections;

public class LayerScript : MonoBehaviour {
    private Layer layer
    {
        get { return layer; }
        set
        {
            Debug.Log("You changed layer from " + this.layer + " to " + layer);
            this.layer = value;
        }
    }
}
