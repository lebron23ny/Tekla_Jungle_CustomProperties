using System;
using Tekla.Structures.CustomPropertyPlugin;
using System.ComponentModel.Composition;
using Tekla.Structures.Model;
using Tekla.Structures;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyModel
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_PERIMETR_MM")]
    public class ProfilePerimetr : ICustomPropertyPlugin
    {
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = 0.0;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.GetPerimetr(part);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
           return Convert.ToInt32(this.GetDoubleProperty(objectId));
        }

        public string GetStringProperty(int objectId)
        {
            return this.GetDoubleProperty(objectId).ToString("f2");
        }
    }
}
