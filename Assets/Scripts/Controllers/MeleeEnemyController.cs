namespace Controllers
{
    public class MeleeEnemyController : EnemyController
    {
        // Start is called before the first frame update
        new void Awake()
        {
            base.Awake();
            IsMelee = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
