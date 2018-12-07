// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.TestModels.RainbowsModel
{
    public class Person
    {
        public Person()
        {
            RainbowSightings = new List<RainbowSighting>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<RainbowSighting> RainbowSightings { get; set; }
    }
}
