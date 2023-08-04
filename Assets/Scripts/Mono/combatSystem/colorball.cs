using CombatSystem;
using UnityEngine;

public class colorball : MonoBehaviour
{
    public CombatColor color;
  public  GameObject g;
    [SerializeField]
    SpriteRenderer renderer;
    private void Start()
    {
        renderer.sprite = combatColorController.GetSprite(color);
    }
    // Start is called before the first frame update
    public void Enter(Collider other)
    {
        IndividualProperty individualProperty=other.GetComponent<IndividualProperty>();
        if (individualProperty!=null&& individualProperty.color==color)
        {
            individualProperty.gainColor(1);
            Destroy(g);
        }
    }
}
