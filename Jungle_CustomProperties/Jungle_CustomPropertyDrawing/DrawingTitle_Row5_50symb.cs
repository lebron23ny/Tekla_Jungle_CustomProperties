using System;
using System.ComponentModel.Composition;
using Tekla.Structures.CustomPropertyPlugin;
using TSD = Tekla.Structures.Drawing;
using Jungle_CustomProperties.Extension;
using System.Collections.Generic;

namespace Jungle_CustomProperties.Jungle_CustomPropertyDrawing
{
    [Export(typeof(ICustomPropertyPlugin))]
    [ExportMetadata("CustomProperty", "CUSTOM.ME_TITLE_ROW5_50SYMB")]
    public class DrawingTitle_Row5_50symb : ICustomPropertyPlugin
    {
        private TSD.DrawingHandler DrawingHandler = new TSD.DrawingHandler();
        public double GetDoubleProperty(int objectId) => -1.0;
        public int GetIntegerProperty(int objectId) => -1;

        public string GetStringProperty(int objectId)
        {
            TSD.Drawing drawing = DrawingHandler.GetActiveDrawing();
            if (drawing == null)
                return string.Empty;
            MyDrawingClass myDrawingClass = new MyDrawingClass(drawing);
            List<string> list = ToolsDrawing.GetParseRowLimitWidth(myDrawingClass.TittleAllViews, 50);
            if (list.Count > 4)
                return list[4];
            return string.Empty;
        }
    }
}
