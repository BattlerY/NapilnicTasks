
    class Weapon
    {
        private int Damage;
        private int Bullets;

        public void Fire(Player player)
        {
            if (Bullets < 1)
                return;

            player.TakeDamage(Damage);
            Bullets -= 1;
        }
    }

    class Player
    {
        private int Health;

        public void TakeDamage(int Damage) 
        {
            if(Healt > 0)
                Health -= Damage;
        }
    }

    class Bot
    {
        private Weapon Weapon;

        public void OnSeePlayer(Player player)
        {
            Weapon.Fire(player);
        }
    }