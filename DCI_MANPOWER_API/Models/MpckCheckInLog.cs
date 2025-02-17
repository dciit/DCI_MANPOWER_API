﻿using System;
using System.Collections.Generic;

namespace DCI_MANPOWER_API.Models;

public partial class MpckCheckInLog
{
    public string Nbr { get; set; } = null!;

    public string? ObjCode { get; set; }

    public string? EmpCode { get; set; }

    public string? Ckdate { get; set; }

    public string? Ckshift { get; set; }

    public string? Cktype { get; set; }

    public DateTime? CkdateTime { get; set; }
}
