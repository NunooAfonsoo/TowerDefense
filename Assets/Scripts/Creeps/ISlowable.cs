using System.Collections;

public interface ISlowable
{
    IEnumerator SlowDown(float time);
}
