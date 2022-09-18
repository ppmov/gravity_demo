using UnityEngine;

public struct FigureContact
{
    private Rigidbody body1;
    private Rigidbody body2;
    private float time;

    public float TimeSpent => Time.time - time;

    public FigureContact(Rigidbody body1, Rigidbody body2)
    {
        this.body1 = body1;
        this.body2 = body2;
        time = Time.time;
    }

    public static implicit operator FigureContact(ContactPoint contactPoint) => 
        new FigureContact(contactPoint.thisCollider.attachedRigidbody, contactPoint.otherCollider.attachedRigidbody);

    public override bool Equals(object o)
    {
        if (!(o is FigureContact other))
            return false;

        if (body1 == other.body1 && body2 == other.body2)
            return true;

        // контакт с другим порядком тел - тот же контакт
        if (body1 == other.body2 && body2 == other.body1)
            return true;

        return false;
    }

    public override int GetHashCode() => body1.GetHashCode() ^ body2.GetHashCode();
}
