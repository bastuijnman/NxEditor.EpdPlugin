using CsOead;
using BfevLibrary;
using NxEditor.EpdPlugin.Models.Common;
using NxEditor.PluginBase.Models;

namespace NxEditor.EpdPlugin.ViewModels;

public class BfevflEditorViewModel : TextEditorBase
{
    public BfevflEditorViewModel(IFileHandle handle) : base(handle)
    {
        Handle = handle;
        ExportExtensions = new string[] {
            "BFEVFL:*.bfevfl|",
        };

        View.GrammarId = "source.json";
        View.ReloadSyntaxHighlighting();
    }

    public override string[] ExportExtensions { get; }

    public override Task Read()
    {
        BfevFile bfev = BfevFile.FromBinary(Handle.Data);
        View.TextEditor.Text = bfev.ToJson(true);

        return Task.CompletedTask;
    }

    public override Task<IFileHandle> Write()
    {
        BfevFile bfev = BfevFile.FromJson(View.TextEditor.Text);
        Handle.Data = bfev.ToBinary();

        return Task.FromResult(Handle);
    }
}
