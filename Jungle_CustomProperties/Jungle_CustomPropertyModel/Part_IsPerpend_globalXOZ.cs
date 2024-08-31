using System;
using System.ComponentModel.Composition;
using Tekla.Structures;
using Tekla.Structures.CustomPropertyPlugin;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Jungle_CustomProperties.Extension;
using System.Data;

namespace Jungle_CustomProperties.Jungle_CustomPropertyModel
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_IS_PERPENDICULAR_GLOBAL_XOZ")]
    public class Part_IsPerpend_globalXOZ : ICustomPropertyPlugin
    {
        GeometricPlane geometricPlane = new GeometricPlane(new Point(0, 0, 0), new Vector(0, 1, 0));
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.IsPerpendicular_Plane_IN_GLOBAL(part, geometricPlane);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsPerpendicular_Plane_IN_GLOBAL(part, geometricPlane);
            return intProperty;
        }

        public string GetStringProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsPerpendicular_Plane_IN_GLOBAL(part, geometricPlane);
            return intProperty.ToString();
        }
    }
}
