using System;
using System.ComponentModel.Composition;
using Tekla.Structures;
using Tekla.Structures.CustomPropertyPlugin;
using Tekla.Structures.Model;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyModel
{

    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_PROFILE_KM_SYMBOL")]
    public class ProfileKM_Symbol : ICustomPropertyPlugin
    {
        public double GetDoubleProperty(int objectId)
        {
            throw new NotImplementedException();
        }

        public int GetIntegerProperty(int objectId)
        {
            throw new NotImplementedException();
        }

        public string GetStringProperty(int objectId)
        {
            string stringProperty = "UNKNOWN";
            if (new Tekla.Structures.Model.Model().SelectModelObject(new Identifier(objectId)) is Part part)
                stringProperty = ToolsModel.GetProfileKM_Symbol(part);
            return stringProperty;
        }
    }
}
