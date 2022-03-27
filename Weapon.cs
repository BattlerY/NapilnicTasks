class Weapon
{
    private int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
        _damage = damage > 0 ? damage : 0;
        _bullets = bullets > 0 ? bullets : 0;
    }

    public void Fire(Player player)
    {
        if (_bullets <= 0)
            throw new IndexOutOfRangeException();

        player.TakeDamage(_damage);
        _bullets--;
    }
}

class Player
{
    private int _health;

    public Player(int health) =>
        _health = health;

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            throw new IndexOutOfRangeException();

        _health -= damage;
    }

}

class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon) =>
        _weapon = weapon;

    public void OnSeePlayer(Player player) =>
        _weapon.Fire(player);
}