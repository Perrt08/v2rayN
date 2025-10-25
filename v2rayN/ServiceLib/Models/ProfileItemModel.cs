namespace ServiceLib.Models;

[Serializable]
public partial class ProfileItemModel : ProfileItem
{
    public bool IsActive { get; set; }
    public string SubRemarks { get; set; }

    [Reactive]
    public partial int Delay { get; set; }

    public decimal Speed { get; set; }
    public int Sort { get; set; }

    [Reactive]
    public partial string DelayVal { get; set; }

    [Reactive]
    public partial string SpeedVal { get; set; }

    [Reactive]
    public partial string TodayUp { get; set; }

    [Reactive]
    public partial string TodayDown { get; set; }

    [Reactive]
    public partial string TotalUp { get; set; }

    [Reactive]
    public partial string TotalDown { get; set; }
}
