using Installer.LightningReturnFF13.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using DialogResult = MudBlazor.DialogResult;

namespace Installer.LightningReturnFF13.Shared.Framework;

public class CommonModal
{
    private IDialogService _dialogService;
    public CommonModal(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<DialogResult> Show(string contextText, DialogOptions options = default!, string title = "", string buttonText = "Ok", bool cancelButton = false)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", (MarkupString)contextText},
            { "ButtonCancel", cancelButton },
            { "ButtonText", buttonText }
        };

        IDialogReference? dialog = await _dialogService.ShowAsync<CommonDialogs>(title: title, parameters: parameters, options: options);
        return await dialog.Result;
    }
}