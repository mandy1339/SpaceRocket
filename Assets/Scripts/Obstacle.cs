using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject particlesObstacleExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        GameObject obstExplostion = Instantiate(particlesObstacleExplosion, this.transform.position, this.transform.rotation).transform.GetChild(0).gameObject;
        float explosionScaleMultiplier = this.GetComponent<SpriteRenderer>().bounds.size.y * 0.45f;
        obstExplostion.transform.localScale = obstExplostion.transform.localScale * explosionScaleMultiplier;
        obstExplostion.GetComponent<ParticleSystem>()?.Play();
        Destroy(obstExplostion.transform.parent.gameObject, 3f);

        // explode the linked mirror obstacle
        GameObject mirrorObstacle = GetComponent<AttachableSpcObjProperties>().mirrorObstacle;
        if(mirrorObstacle != null)
        {
            GameObject obstExplostionMirror = Instantiate(particlesObstacleExplosion, mirrorObstacle.transform.position, mirrorObstacle.transform.rotation).transform.GetChild(0).gameObject;
            obstExplostionMirror.transform.localScale = obstExplostionMirror.transform.localScale * explosionScaleMultiplier;
            obstExplostionMirror.GetComponent<ParticleSystem>()?.Play();
            Destroy(obstExplostionMirror.transform.parent.gameObject, 3f);
        }
    }
}
