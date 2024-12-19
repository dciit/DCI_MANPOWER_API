using System;
using System.Collections.Generic;

namespace DCI_MANPOWER_API.Models;

public partial class MpckObjectMaster
{
    public string ObjMasterId { get; set; } = null!;

    public string? MstName { get; set; }

    public string LayoutCode { get; set; } = null!;

    public string? ObjSvg { get; set; }

    public string? MstStatus { get; set; }

    public int? MstOrder { get; set; }
}
