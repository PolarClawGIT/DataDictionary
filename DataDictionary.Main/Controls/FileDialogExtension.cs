using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Controls
{
    static class FileDialogExtension
    {
        /// <summary>
        /// Sets the FileDialog to handle the specific file.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="file"></param>
        public static void SetDialog(this FileDialog dialog, IFileValue file)
        {
            dialog.Reset();
            dialog.InitialDirectory = file.DirectoryPath;
            dialog.Filter = String.Join('|', file.FileFormats.Select(s => s.DialogFilter()));
            dialog.FileName = file.FileName;
        }

    }
}
