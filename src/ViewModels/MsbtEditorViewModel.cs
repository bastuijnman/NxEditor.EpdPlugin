﻿using CsMsbt;
using NxEditor.EpdPlugin.Models.Common;
using NxEditor.PluginBase.Models;

namespace NxEditor.EpdPlugin.ViewModels;

public class MsbtEditorViewModel : TextEditorBase
{
    public MsbtEditorViewModel(IFileHandle handle) : base(handle)
    {
        Handle = handle;
        ExportExtensions = new string[] {
            "MSBT:*.msbt|",
            "Compressed:*.zs|"
        };

        View.GrammarId = "source.yaml";
        View.ReloadSyntaxHighlighting();
    }

    public override string[] ExportExtensions { get; }

    public override Task Read()
    {
        Msbt msbt = Msbt.FromBinary(Handle.Data);
        View.TextEditor.Text = msbt.ToText().ToString();

        return Task.CompletedTask;
    }

    public override Task<IFileHandle> Write()
    {
        Msbt msbt = Msbt.FromText(View.TextEditor.Text);
        Handle.Data = msbt.ToBinary().ToArray();
        return Task.FromResult(Handle);
    }
}
