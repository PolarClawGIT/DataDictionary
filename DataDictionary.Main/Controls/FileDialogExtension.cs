using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;

namespace DataDictionary.Main.Controls
{
    static class FileDialogExtension
    {
        /// <summary>
        /// Opens or prompts for a file and returns the work items required to Open that file.
        /// </summary>
        /// <param name="dialog">The file dialog used to prompt the user when a filename is required.</param>
        /// <param name="directory">Directory context providing an <see cref="IDirectoryValue.InitialDirectory"/> and validation helper.</param>
        /// <param name="file">The file value that may contain a (possibly relative) file name and exposes operations to produce work items.</param>
        /// <returns>
        /// A read-only list of <see cref="WorkItem"/> instances representing the tasks required to open the selected or existing file.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when neither a valid existing file nor a dialog-provided filename is available.
        /// The UI is expected to prevent this situation (e.g. open button disabled).
        /// </exception>
        /// <remarks>
        /// Behavior:
        /// <list type="bullet">
        /// <item>If both <paramref name="directory"/> and <paramref name="file"/> are valid, the method creates a <see cref="FileInfo"/>
        /// pointing to the combined path and returns the work items for the file without displaying the dialog.
        /// </item>
        /// <item>If the combined path does not exist, the <paramref name="dialog"/> is shown; when the user accepts,
        /// <paramref name="file"/>.FileName is updated (relative to the directory's initial directory) and the work items
        /// for the resulting path are returned.
        /// </item>
        /// <item>If the directory is valid but the file is not, the <paramref name="dialog"/> is shown to obtain a filename,
        /// which is then set on <paramref name="file"/> and used to produce the work items.</item>
        /// <item>Otherwise an <see cref="ArgumentException"/> is thrown; callers should ensure the UI prevents invoking
        /// this method in that invalid state.
        /// </item>
        /// </list> 
        /// </remarks>
        public static IReadOnlyList<WorkItem> OpenDialog(this FileDialog dialog, IDirectoryValue directory, IFileValue file)
        {   // CoPilot generated comment block.
            List<WorkItem> work = new List<WorkItem>();
            dialog.Reset();

            if (directory.IsValid(out _) && file.IsValid(out _))
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(directory.InitialDirectory, file.FileName));

                if (fileInfo.Exists)
                { work.AddRange(file.Open(fileInfo)); }
                else if (dialog.ShowDialog(directory, file) is DialogResult.OK)
                {
                    file.FileName = Path.GetRelativePath(directory.InitialDirectory, dialog.FileName);

                    fileInfo = new FileInfo(Path.Combine(directory.InitialDirectory, file.FileName));
                    work.AddRange(file.Open(fileInfo));
                }

            }
            else if (directory.IsValid(out _) && !file.IsValid(out _)
                && dialog.ShowDialog(directory, file) is DialogResult.OK)
            {
                file.FileName = Path.GetRelativePath(directory.InitialDirectory, dialog.FileName);

                FileInfo fileInfo = new FileInfo(Path.Combine(directory.InitialDirectory, file.FileName));
                work.AddRange(file.Open(fileInfo));
            }
            else
            {
                // Should not get here. Button should be disabled.
                Exception ex = new ArgumentException("Directory could not be resolved.");
                ex.Data.Add(nameof(directory.RootFolder), directory.RootFolder);
                ex.Data.Add(nameof(directory.InitialDirectory), directory.InitialDirectory);
                ex.Data.Add(nameof(file.FileName), file.FileName);
                throw ex;
            }

            return work;
        }

        /// <summary>
        /// Opens or prompts for a file and returns the work items required to Save that file.
        /// </summary>
        /// <param name="dialog">The file dialog used to prompt for a filename when needed.</param>
        /// <param name="directory">Directory context providing an initial directory and validation.</param>
        /// <param name="file">The file value containing the (possibly relative) file name and operations to produce work items.</param>
        /// <returns>
        /// A read-only list of <see cref="WorkItem"/> instances representing the tasks required to perform the save.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the directory or file state is invalid and the dialog cannot supply a filename.
        /// The UI is expected to prevent this situation (e.g. save button disabled).
        /// </exception>
        /// <remarks>
        /// Behavior:
        /// <list type="bullet">
        /// <item>If both <paramref name="directory"/> and <paramref name="file"/> are valid, the method creates a <see cref="FileInfo"/>
        /// pointing to the combined path and returns the work items for the file without displaying the dialog.
        /// </item>
        /// <item>If the directory is valid but the file name is not, the <paramref name="dialog"/> is shown to prompt the user.
        /// When the user accepts, the <paramref name="file"/>'s FileName is updated (relative to the directory's initial directory)
        /// and the work items for the resulting path are returned.</item>
        /// <item>If neither condition applies the method throws <see cref="ArgumentException"/>; callers should ensure the
        /// UI prevents invoking this method in that state.</item>
        /// </list> 
        /// </remarks>
        public static IReadOnlyList<WorkItem> SaveDialog(this FileDialog dialog, IDirectoryValue directory, IFileValue file)
        {   // CoPilot generated base comment block.
            List<WorkItem> work = new List<WorkItem>();
            dialog.Reset();

            if (directory.IsValid(out _) && file.IsValid(out _))
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(directory.InitialDirectory, file.FileName));
                work.AddRange(file.Save(fileInfo));
            }
            else if (directory.IsValid(out _) && !file.IsValid(out _)
                && dialog.ShowDialog(directory, file) is DialogResult.OK)
            {
                file.FileName = Path.GetRelativePath(directory.InitialDirectory, dialog.FileName);

                FileInfo fileInfo = new FileInfo(Path.Combine(directory.InitialDirectory, file.FileName));
                work.AddRange(file.Save(fileInfo));
            }
            else
            {
                // Should not get here. Button should be disabled.
                Exception ex = new ArgumentException("Directory could not be resolved.");
                ex.Data.Add(nameof(directory.RootFolder), directory.RootFolder);
                ex.Data.Add(nameof(directory.InitialDirectory), directory.InitialDirectory);
                ex.Data.Add(nameof(file.FileName), file.FileName);
                throw ex;
            }

            return work;
        }

        /// <summary>
        /// Prompts for a file name using a File Dialog.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        /// <remarks>
        /// Displays the dialog and then sets the FileName to the selected value.
        /// The code does not OPEN the file just returns and updates the FileName.
        /// </remarks>
        public static void SelectDialog(this OpenFileDialog dialog, IDirectoryValue directory, IFileValue file)
        {   // The Button will still read Open, but all it does is updates the FileName.
            // Changing the text on the button would require extensive coding into the Windows API.
            dialog.Reset();
            dialog.Title = "Select file (does not OPEN)";
            dialog.CheckFileExists = false;

            if (dialog.ShowDialog(directory, file) is DialogResult.OK)
            {
                if (String.IsNullOrWhiteSpace(directory.InitialDirectory))
                { file.FileName = dialog.FileName; }
                else
                { file.FileName = Path.GetRelativePath(directory.InitialDirectory, dialog.FileName); }
            }
        }

        /// <summary>
        /// ShowDialog that accepts IDirectoryValue and IFileValue.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        static DialogResult ShowDialog(this FileDialog dialog, IDirectoryValue directory, IFileValue file)
        {
            dialog.InitialDirectory = Path.Combine(directory.InitialDirectory, Path.GetDirectoryName(file.FileName) ?? String.Empty);
            dialog.Filter = String.Join('|',
                file.FileFormats.
                Select(s => s.GetEnumeration()).
                SelectMany(s => s.Extensions, (p, c) =>
                {
                    if (String.IsNullOrWhiteSpace(c))
                    { return String.Format("{0}|*.*", p.DisplayName); }
                    else
                    { return String.Format("{0} ({1})|*.{1}", p.DisplayName, c); }
                }));
            dialog.FileName = Path.GetFileName(file.FileName);

            return dialog.ShowDialog();
        }
    }
}
