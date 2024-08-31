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
    [ExportMetadata("CustomProperty", "CUSTOM.ME_IS_PARALLEL_WORK_PLANE_YOZ")]
    public class Part_IsParallel_in_work_plane_YOZ : ICustomPropertyPlugin
    {
        GeometricPlane geometricPlane = new GeometricPlane(new Point(0, 0, 0), new Vector(1, 0, 0));
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.IsParallel_Plane_IN_WORK_PLANE(part, geometricPlane);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsParallel_Plane_IN_WORK_PLANE(part, geometricPlane);
            return intProperty;
        }

        public string GetStringProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsParallel_Plane_IN_WORK_PLANE(part, geometricPlane);
            return intProperty.ToString();
        }
    }
}
