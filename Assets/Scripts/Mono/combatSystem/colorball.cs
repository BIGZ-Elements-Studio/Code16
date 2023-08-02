using CombatSystem;
using UnityEngine;

public class colorball : MonoBehaviour
{
    public CombatColor color;
  public  GameObject g;
   public Sprite red;
    public Sprite yellow;
    public Sprite blue;
    [SerializeField]
    SpriteRenderer renderer;
    private void Start()
    {
        if (color==CombatColor.red)
        {
            renderer.sprite=red;
        }
        else if (color == CombatColor.yellow)
        {
            renderer.sprite=yellow;
        } else if (color == CombatColor.blue)
        {
            renderer.sprite= blue;
        }
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
