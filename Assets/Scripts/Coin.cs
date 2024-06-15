using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;
    void OnTriggerEnter(Collider collider) 
    {
        //If not collider with player
        if (collider.gameObject.name != "Player") {
            if (collider.gameObject.TryGetComponent<Obstacle>(out Obstacle component)) {
                Destroy(gameObject);
            }
            return;
        }
        //Add score
        GameManager.inst.IncrementScore();
        //Destroy this coin
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0,0, turnSpeed * Time.deltaTime);
    }
}
