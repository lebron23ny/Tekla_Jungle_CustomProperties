using System;
using Tekla.Structures.CustomPropertyPlugin;
using System.ComponentModel.Composition;
using Tekla.Structures.Model;
using Tekla.Structures;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyModel
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_REDUCED_THICK_MM")]
    public class ProfileReducedThickness : ICustomPropertyPlugin
    {
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = 0.0;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.Get_Reduced_Thickness(part);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
           return Convert.ToInt32(this.GetDoubleProperty(objectId));
        }

        public string GetStringProperty(int objectId)
        {
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                return this.GetDoubleProperty(objectId).ToString("f2");
            return "UNKNOWN";
        }
    }
}
