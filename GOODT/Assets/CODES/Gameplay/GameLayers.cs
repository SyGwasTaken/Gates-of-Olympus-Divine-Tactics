using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask SolidObjectLayer;
    [SerializeField] LayerMask InteractablesLayer;

    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }
    public LayerMask SolidLayer
    {
        get => SolidObjectLayer;
    }

        public LayerMask interactablesLayer
    {
        get => InteractablesLayer;
    }
}
