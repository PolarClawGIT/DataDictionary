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
            dialog.Reset();
            dialog.InitialDirectory = directory.InitialDirectory;
            dialog.Filter = String.Join('|', file.FileFormats.Select(s => s.DialogFilter()));
            dialog.FileName = file.FileName;

            DialogResult result = dialog.ShowDialog();

            if (result is DialogResult.OK)
            { file.FileName = dialog.FileName; }

            return result;
        }
    }
}
