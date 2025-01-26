using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Dash")]
public class DashConfigSO : PowerUpConfigSO
{
    [SerializeField]
    private float dashRange = 5f;

    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent(out IPowerUp _))
        {
            return;
        }
        var dash = target.AddComponent<Dash>();
        dash.SetUp(dashRange);
    }
}
