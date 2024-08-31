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
    [ExportMetadata("CustomProperty", "CUSTOM.ME_IS_PARALLEL_WORK_PLANE_Y")]
    public class Part_IsParallel_in_work_plane_Y : ICustomPropertyPlugin
    {
        Vector vector = new Vector(0, 1, 0);
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.IsParallel_Vector_IN_WORK_PLANE(part, vector);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsParallel_Vector_IN_WORK_PLANE(part, vector);
            return intProperty;
        }

        public string GetStringProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsParallel_Vector_IN_WORK_PLANE(part, vector);
            return intProperty.ToString();
        }
    }
}
