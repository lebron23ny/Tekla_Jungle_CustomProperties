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
    [ExportMetadata("CustomProperty", "CUSTOM.ME_IS_PARALLEL_GLOBAL_Z")]
    public class Part_IsParallel_globalZ : ICustomPropertyPlugin
    {
        Vector vector = new Vector(0, 0, 1);
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                doubleProperty = ToolsModel.IsParallel_Vector_IN_GLOBAL(part, vector);
            return doubleProperty;
        }

        public int GetIntegerProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsParallel_Vector_IN_GLOBAL(part, vector);
            return intProperty;
        }

        public string GetStringProperty(int objectId)
        {
            int intProperty = -1;
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                intProperty = ToolsModel.IsParallel_Vector_IN_GLOBAL(part, vector);
            return intProperty.ToString();
        }
    }
}
