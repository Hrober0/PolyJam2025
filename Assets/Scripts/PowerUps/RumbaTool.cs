using UnityEngine;

public class RumbaTool : MonoBehaviour
{
    private PlayerToolsController toolsController;
    private PlayerToolsController.Tool tool;

    public async void Setup(PlayerToolsController.Tool tool, float duration)
    {
        this.tool = tool;
        toolsController = GetComponent<PlayerToolsController>();
        toolsController.SetTool(tool, true);
        await Awaitable.WaitForSecondsAsync(duration);
        Remove();
    }

    public void Remove()
    {
        Destroy(this);
        toolsController.SetTool(tool, false);
    }
}
