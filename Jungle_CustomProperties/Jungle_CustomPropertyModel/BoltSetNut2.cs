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
    [ExportMetadata("CustomProperty", "CUSTOM.ME_BOLT_SET_NUT2")]
    public class BoltSetNut2 : ICustomPropertyPlugin
    {
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1.0;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is BoltGroup boltGroup)
                doubleProperty = Convert.ToDouble(ToolsModel.SetNut2(boltGroup));
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            int doubleProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is BoltGroup boltGroup)
                doubleProperty = Convert.ToInt32(ToolsModel.SetNut2(boltGroup));
            return doubleProperty;
        }

        public string GetStringProperty(int objectId)
        {
            string doubleProperty = "UNKNOWN";
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is BoltGroup boltGroup)
                doubleProperty = Convert.ToString(ToolsModel.SetNut2(boltGroup));
            return doubleProperty;
        }
    }
}
