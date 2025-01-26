using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Jump")]
public class JumpConfigSO : PowerUpConfigSO
{
    [SerializeField]
    private float force = 5f;

    public override void Apply(GameObject target)
    {
        target.AddComponent<Jump>().SetUp(force);
    }
}
