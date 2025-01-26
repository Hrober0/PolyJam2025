using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/RumbaTool")]
public class RumbaToolConfigSO : PowerUpConfigSO
{
    [SerializeField] private PlayerToolsController.Tool tool;
    [SerializeField] private float duration = 10;

    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent(out RumbaTool lastTool))
        {
            lastTool.Remove();
        }
        target.AddComponent<RumbaTool>().Setup(tool, duration);
    }
}
