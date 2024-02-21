using System.Text;

public interface IHaveDescription
{
    string CreateDescription();

    void AddDescription(ref StringBuilder sb)
    {
        sb.AppendLine(CreateDescription());
    }
}

