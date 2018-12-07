// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.EntityFrameworkCore.TestModels.RainbowsModel
{
    public class RainbowSighting
    {
        public int Id { get; set; }

        public int RainbowTypeId { get; set; }

        public int PersonId { get; set; }

        public int LocationId { get; set; }

        public DateTime SeenTime { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual Location Location { get; set; }

        public virtual Person Person { get; set; }

        public virtual RainbowType RainbowType { get; set; }
    }
}
