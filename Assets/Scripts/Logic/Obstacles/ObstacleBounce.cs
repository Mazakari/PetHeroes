using UnityEngine;

public class ObstacleBounce : MonoBehaviour, IBouncable
{
    [SerializeField] private float _directionChangeDeadZone = 0.1f;
    [SerializeField] private int _forceDecreaseMultiplier = 3;
    [SerializeField] private ItemSound _sound;

    private void OnCollisionEnter2D(Collision2D collision) => 
        BounceOff(collision);

    public void BounceOff(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out LaunchPlayer player))
        {
            CalculateBounceDirection(collision, player);
            PlayCollisionSound();
        }
    }
    private void CalculateBounceDirection(Collision2D collision, LaunchPlayer player)
    {
        Vector2 collisionPoint = collision.GetContact(0).point;

        float collisionPositionX = RoundXPosValue(collisionPoint.x);
        float platformPositionX = RoundXPosValue(transform.position.x);

        float difference = platformPositionX - collisionPositionX;

        if (BallOutsideDeathZone(difference))
        {
            player.Rigidbody.velocity = Vector2.zero;

            float direction = -1f;
            if (collisionPositionX > platformPositionX)
            {
                direction = 1f;
            }

            Vector2 newDirection = new(direction * Mathf.Abs(difference * (player.Force / _forceDecreaseMultiplier)), player.Force);
            player.AddPlayerLaunchForce(newDirection);
            

            Debug.Log($"Collision {gameObject.name}");
        }
    }

    private bool BallOutsideDeathZone(float difference) =>
        Mathf.Abs(difference) > _directionChangeDeadZone;
    private static float RoundXPosValue(float value) =>
        Mathf.Round(value * 100f) / 100f;

    private void PlayCollisionSound() =>
        _sound.Play();
}
