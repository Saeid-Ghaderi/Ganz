using Ganz.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganz.Domain.Catalogs.Features
{
    public sealed class FeatureId : StronglyTypedId<FeatureId>
    {
        public FeatureId(Guid value) : base(value)
        {

        }
    }
}
