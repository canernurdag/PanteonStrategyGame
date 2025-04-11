public interface IDamagable : IMonobehaviour
{
	void Damage(float damageAmount);
	void SetInitHealth(float health);
	void SetCurrentHealth(float health);
}
