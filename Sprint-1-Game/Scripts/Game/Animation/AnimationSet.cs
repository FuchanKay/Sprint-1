namespace Scripts.Game;

public class AnimationSet
{
    public string Name { get; set; }

    public int NumFrames { get; set; }

    public int Row { get; set; }

    public AnimationSet(string name, int numFrames, int rowNumber)
    {
        Name = name;
        NumFrames = numFrames;
        Row = rowNumber;
    }
}