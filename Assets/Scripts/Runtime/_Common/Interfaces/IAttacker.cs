public interface IAttacker : IMonobehaviour
{
	void Attack(IDamagable damageable);
	void SetDamageAmount(float damageAmount);
}
