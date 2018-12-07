// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.EntityFrameworkCore.TestModels.RainbowsModel
{
    public class LocationWithRainbowSightingDetails
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public string RainbowTypeName { get; set; }

        public DateTime SeenTime { get; set; }
    }
}
