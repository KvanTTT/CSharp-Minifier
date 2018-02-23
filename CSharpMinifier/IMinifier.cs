namespace CSharpMinifier
{
    public interface IMinifier
    {
        string MinifyFiles(string[] csFiles);

        string MinifyFromString(string csharpCode);
    }
}
