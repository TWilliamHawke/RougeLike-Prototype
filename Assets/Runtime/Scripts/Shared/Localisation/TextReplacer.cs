[System.Serializable]
public struct TextReplacer
{
    public string pattern { get; init; }
    public string replacer { get; init; }
    
    public TextReplacer(string pattern, string replacer)
    {
        this.pattern = pattern;
        this.replacer = replacer;
    }

    public TextReplacer(string pattern, float replacer): this(pattern, replacer.ToString())
    {
    }

}


