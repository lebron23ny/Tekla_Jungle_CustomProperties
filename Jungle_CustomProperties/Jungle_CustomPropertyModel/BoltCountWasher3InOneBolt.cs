using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.CustomPropertyPlugin;
using Tekla.Structures.Model;
using Tekla.Structures;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyModel
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_BOLT_COUNT_WASHER3_IN_BOLT")]
    public class BoltCountWasher3InOneBolt : ICustomPropertyPlugin
    {
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = 0.0;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is BoltGroup boltGroup)
                doubleProperty = ToolsModel.GetWasher3NumberInOneBolt(boltGroup);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            return Convert.ToInt32(this.GetDoubleProperty(objectId));
        }

        public string GetStringProperty(int objectId)
        {
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is BoltGroup boltGroup)
                return this.GetDoubleProperty(objectId).ToString("f2");
            return "UNKNOWN";
        }
    }
}
