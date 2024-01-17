namespace WebStore.Other
{
    public class PathModifier
    {
        public static string MakeRelativePath(string absolutePath, string referencePath, char newSeparator)
        {
            var fileUri = new Uri(absolutePath);
            var referenceUri = new Uri(referencePath);
            return Uri.UnescapeDataString(referenceUri.MakeRelativeUri(fileUri).ToString()).Replace('\\', newSeparator);
        }
    }
}
