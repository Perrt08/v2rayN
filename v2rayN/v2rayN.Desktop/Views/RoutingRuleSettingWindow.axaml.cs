using v2rayN.Desktop.Base;
using v2rayN.Desktop.Common;

namespace v2rayN.Desktop.Views;

public partial class RoutingRuleSettingWindow : WindowBase<RoutingRuleSettingViewModel>
{
    public RoutingRuleSettingWindow()
    {
        InitializeComponent();
    }

    public RoutingRuleSettingWindow(RoutingItem routingItem)
    {
        InitializeComponent();

        this.Loaded += Window_Loaded;
        btnCancel.Click += (s, e) => this.Close();
        this.KeyDown += RoutingRuleSettingWindow_KeyDown;
        lstRules.SelectionChanged += lstRules_SelectionChanged;
        lstRules.DoubleTapped += LstRules_DoubleTapped;
        menuRuleSelectAll.Click += menuRuleSelectAll_Click;
        //btnBrowseCustomIcon.Click += btnBrowseCustomIcon_Click;
        btnBrowseCustomRulesetPath4Singbox.Click += btnBrowseCustomRulesetPath4Singbox_ClickAsync;

        ViewModel = new RoutingRuleSettingViewModel(routingItem, UpdateViewHandler);

        cmbdomainStrategy.ItemsSource = Global.DomainStrategies.AppendEmpty();
        cmbdomainStrategy4Singbox.ItemsSource = Global.DomainStrategies4Singbox;
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.Close(true);
                break;

            case EViewAction.ShowYesNo:
                if (await UI.ShowYesNo(this, ResUI.RemoveServer) != ButtonResult.Yes)
                {
                    return false;
                }
                break;

            case EViewAction.AddBatchRoutingRulesYesNo:
                if (await UI.ShowYesNo(this, ResUI.AddBatchRoutingRulesYesNo) != ButtonResult.Yes)
                {
                    return false;
                }
                break;

            case EViewAction.RoutingRuleDetailsWindow:
                if (obj is null)
                    return false;
                return await new RoutingRuleDetailsWindow((RulesItem)obj).ShowDialog<bool>(this);

            case EViewAction.ImportRulesFromFile:
                var fileName = await UI.OpenFileDialog(this, null);
                if (fileName.IsNullOrEmpty())
                {
                    return false;
                }
                ViewModel?.ImportRulesFromFileAsync(fileName);
                break;

            case EViewAction.SetClipboardData:
                if (obj is null)
                {
                    return false;
                }

                await AvaUtils.SetClipboardData(this, (string)obj);
                break;

            case EViewAction.ImportRulesFromClipboard:
                var clipboardData = await AvaUtils.GetClipboardData(this);
                if (clipboardData.IsNotEmpty())
                {
                    ViewModel?.ImportRulesFromClipboardAsync(clipboardData);
                }

                break;
        }

        return await Task.FromResult(true);
    }

    private void Window_Loaded(object? sender, RoutedEventArgs e)
    {
        txtRemarks.Focus();
    }

    private void RoutingRuleSettingWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyModifiers is KeyModifiers.Control or KeyModifiers.Meta)
        {
            if (e.Key == Key.A)
            {
                lstRules.SelectAll();
            }
            else if (e.Key == Key.C)
            {
                ViewModel?.RuleExportSelectedAsync();
            }
        }
        else
        {
            if (e.Key == Key.T)
            {
                ViewModel?.MoveRule(EMove.Top);
            }
            else if (e.Key == Key.U)
            {
                ViewModel?.MoveRule(EMove.Up);
            }
            else if (e.Key == Key.D)
            {
                ViewModel?.MoveRule(EMove.Down);
            }
            else if (e.Key == Key.B)
            {
                ViewModel?.MoveRule(EMove.Bottom);
            }
            else if (e.Key == Key.Delete)
            {
                ViewModel?.RuleRemoveAsync();
            }
        }
    }

    private void lstRules_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            ViewModel.SelectedSources = lstRules.SelectedItems.Cast<RulesItemModel>().ToList();
        }
    }

    private void LstRules_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        ViewModel?.RuleEditAsync(false);
    }

    private void menuRuleSelectAll_Click(object? sender, RoutedEventArgs e)
    {
        lstRules.SelectAll();
    }

    //private async void btnBrowseCustomIcon_Click(object? sender, RoutedEventArgs e)
    //{
    //    var fileName = await UI.OpenFileDialog(this, FilePickerFileTypes.ImagePng);
    //    if (fileName.IsNullOrEmpty())
    //    {
    //        return;
    //    }

    //    txtCustomIcon.Text = fileName;
    //}

    private async void btnBrowseCustomRulesetPath4Singbox_ClickAsync(object? sender, RoutedEventArgs e)
    {
        var fileName = await UI.OpenFileDialog(this, null);
        if (fileName.IsNullOrEmpty())
        {
            return;
        }

        txtCustomRulesetPath4Singbox.Text = fileName;
    }

    private void linkCustomRulesetPath4Singbox(object? sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://github.com/2dust/v2rayCustomRoutingList/blob/master/singbox_custom_ruleset_example.json");
    }
}
