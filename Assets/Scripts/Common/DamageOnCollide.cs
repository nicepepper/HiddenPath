using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private void HitObject(GameObject theObject)
    {
        var theirDamage = theObject.GetComponent<DamageTaking>();
        if (theirDamage)
        {
            theirDamage.TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        HitObject(collider.gameObject);
    }
}
