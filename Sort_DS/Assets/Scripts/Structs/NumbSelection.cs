
public class NumbSelection
{
    public short Count { get; private set; }
    public short Min { get; private set; }
    public short Max { get; private set; }
    public NumbSelection(short count, short min, short max)
    {
        Count = count;
        Min = min;
        Max = max;
    }
}
