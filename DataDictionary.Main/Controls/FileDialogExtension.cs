using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Controls
{
    static class FileDialogExtension
    {
        /// <summary>
        /// ShowDialog that accepts IDirectoryValue and IFileValue. FileName is updated on DialogResult.OK
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DialogResult ShowDialog(this FileDialog dialog, IDirectoryValue directory, IFileValue file)
        {
            // Save current settings
            String filePath = Path.GetDirectoryName(file.FileName) ?? String.Empty;
            String fileName = Path.GetFileName(file.FileName);
            String title = dialog.Title;
            Boolean checkFile = dialog.CheckFileExists;

            // Reset then restore settings.
            dialog.Reset();
            dialog.InitialDirectory = Path.Combine(directory.InitialDirectory, filePath);
            dialog.Filter = String.Join('|', file.FileFormats.Select(s => s.DialogFilter()));
            dialog.FileName = fileName;
            dialog.Title = title;
            dialog.CheckFileExists = checkFile;

            DialogResult result = dialog.ShowDialog();

            if (result is DialogResult.OK)
            {
                String relativePath = String.Empty;
                String fileDirectory = Path.GetDirectoryName(dialog.FileName)??String.Empty;

                if (!directory.IsValid(out _) || String.IsNullOrWhiteSpace(directory.InitialDirectory))
                { relativePath = fileDirectory ?? String.Empty; }
                else if (String.Equals(directory.InitialDirectory, fileDirectory))
                { relativePath = String.Empty; }
                else
                { relativePath = Path.GetRelativePath(directory.InitialDirectory, fileDirectory); }

                file.FileName = Path.Combine(relativePath, Path.GetFileName(dialog.FileName));
            }

            return result;
        }
    }
}
