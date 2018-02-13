namespace CSharpMinifier
{
    public interface IMinifier
    {
        string Minify();

        string MinifyFiles(string[] csFiles);

        string MinifyFromString(string csharpCode);
    }
}
