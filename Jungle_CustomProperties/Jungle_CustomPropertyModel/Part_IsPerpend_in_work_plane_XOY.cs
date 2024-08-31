using System.ComponentModel.Composition;
using Tekla.Structures;
using Tekla.Structures.CustomPropertyPlugin;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyModel
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_IS_PERPENDICULAR_WORK_PLANE_XOY")]
    public class Part_IsPerpend_in_work_plane_XOY : ICustomPropertyPlugin
    {
        GeometricPlane geometricPlane = new GeometricPlane(new Point(0, 0, 0), new Vector(0, 0, 1));
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.IsPerpendicular_IN_WORK_PLANE(part, geometricPlane);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsPerpendicular_IN_WORK_PLANE(part, geometricPlane);
            return intProperty;
        }

        public string GetStringProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsPerpendicular_IN_WORK_PLANE(part, geometricPlane);
            return intProperty.ToString();
        }
    }
}
