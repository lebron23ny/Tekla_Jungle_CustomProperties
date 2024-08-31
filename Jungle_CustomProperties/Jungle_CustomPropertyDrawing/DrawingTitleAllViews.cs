using System.ComponentModel.Composition;
using Tekla.Structures.CustomPropertyPlugin;
using TSD = Tekla.Structures.Drawing;
using Jungle_CustomProperties.Extension;

namespace Jungle_CustomProperties.Jungle_CustomPropertyDrawing
{

    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_TITLE_ALL_VIEWS")]
    public class DrawingTitleAllViews : ICustomPropertyPlugin
    {

        private TSD.DrawingHandler DrawingHandler = new TSD.DrawingHandler();
        public double GetDoubleProperty(int objectId) => -1.0;


        public int GetIntegerProperty(int objectId) => -1;
        

        public string GetStringProperty(int objectId)
        {
            TSD.Drawing drawing = DrawingHandler.GetActiveDrawing();
            if(drawing == null)
                return string.Empty;
            MyDrawingClass myDrawingClass = new MyDrawingClass(drawing);
            return myDrawingClass.TittleAllViews;
        }
    }
}
