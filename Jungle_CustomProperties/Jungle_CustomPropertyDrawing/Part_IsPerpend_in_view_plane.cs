using System.ComponentModel.Composition;
using Tekla.Structures;
using Tekla.Structures.CustomPropertyPlugin;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyDrawing
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_IS_PERPENDICULAR_VIEW_PLANE")]
    public class Part_IsPerpend_in_view_plane : ICustomPropertyPlugin
    {
        public double GetDoubleProperty(int objectId)
        {
            double doubleProperty = -1;
            throw new System.NotImplementedException();
        }

        public int GetIntegerProperty(int objectId)
        {
            int intProperty = -1;
            throw new System.NotImplementedException();
        }

        public string GetStringProperty(int objectId)
        {
            int intProperty = -1;
            throw new System.NotImplementedException();
        }
    }
}
