using Installer.LightningReturnFF13.Shared.Interfaces;

namespace Installer.LightningReturnFF13.Shared.Framework;

internal record FolderPicker : IFolderBrowserService
{
    public string DisplayFolderPicker()
    {
        using var dialog = new FolderBrowserDialog();
        DialogResult result = dialog.ShowDialog();
        return result == DialogResult.OK ? dialog.SelectedPath : string.Empty;
    }
}