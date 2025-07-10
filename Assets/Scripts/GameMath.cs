
public static class GameMath
{
    /// <summary>
    /// Převádí otáčky za minutu (RPM) na stupně za sekundu (°/s).
    /// </summary>
    public static float SetRPM(float rpm)
    {
        return (rpm / 60f) * 360f;
    }

    /// <summary>
    /// Převádí střely za minutu (FPM) na střely za sekundu.
    /// </summary>
    public static float SetFPM(float fpm)
    {
        return fpm / 60f;
    }
}

