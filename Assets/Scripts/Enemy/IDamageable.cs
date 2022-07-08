namespace Enemy
{
    public interface IDamageable
    {
        /// <summary>
        /// calculates remaining health, updates healthbar and causes injury effects
        /// </summary>
        /// <param name="damage"> damage done </param>
        public void Hit(int damage);
    }
}
