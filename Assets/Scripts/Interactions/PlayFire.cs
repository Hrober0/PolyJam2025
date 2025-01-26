using UnityEngine;

public class PlayFire : SingletonMB<PlayFire>
{
    [SerializeField] private ParticleSystem[] fires;

    override protected void Awake()
    {
        base.Awake();
        foreach (var fire in fires)
        {
            fire.playOnAwake = false;
        }
    }

    public void Active()
    {
        foreach (var fire in fires)
        {
            fire.Play();
        }
    }
}
