using Jungle_CustomProperties.Extension.myEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using TSD = Tekla.Structures.Drawing;
using TSG = Tekla.Structures.Geometry3d;

namespace Jungle_CustomProperties.Extension
{
    public static class ToolsDrawing
    {
        /// <summary>
        /// Вставляет содержимое строки таблицы на виде view
        /// с координатами левого верхнего угла строки (startX, startY) 
        /// с высотой строки heightRow
        /// с шириной столбцов listWidth
        /// с содержимым столбцов listContent
        /// с выравниванием столбцов listAlligmentContent
        /// c настройками текста textAttributes
        /// если необходимо рисовать Frame доп столбцов (если текст шире значения из listWidth), тогда drawAddRow==true
        /// возвращает координату по Y нижней грани строки endY
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="heightRow"></param>
        /// <param name="heightRowTransfer"></param>
        /// <param name="listWidth"></param>
        /// <param name="view"></param>
        /// <param name="listContent"></param>
        /// <param name="listAlligmentContent"></param>
        /// <param name="textAttributes"></param>
        /// <param name="endY"></param>
        public static void SetRowText(double startX, double startY, 
            int heightRow, int heightRowTransfer, 
            List<int> listWidth, TSD.ViewBase view, List<string> listContent, 
            List<enumContetnHorizontalAlligment> listAlligmentContent,
            TSD.Text.TextAttributes textAttributes, bool drawAddRow,
            TSD.AttributesBase attributes,
            out double endY)
        {
            double heightRowAdd = 0;
            int countAddRow = 0;

            double endX;
            endY = startY - heightRow - heightRowAdd;

            double startXadd = startX;
            double startYadd = startY - heightRow;

            for (int i = 0; i < listWidth.Count; i++)
            {
                endX = startX + listWidth[i];
                #region Вставка текста
                if (listContent[i] == "" || listContent[i] == string.Empty)
                {
                    startX = endX;
                    continue;
                }

                if (listAlligmentContent[i] == enumContetnHorizontalAlligment.center)
                {
                    //Если точка вставки по центру ячейки
                    TSG.Point insertTextPoint = new TSG.Point(startX + (endX - startX) / 2, endY + (startY - endY) / 2, 0);
                    TSD.Text text = new TSD.Text(view, insertTextPoint, listContent[i], textAttributes);
                    text.Insert();
                    double _width = text.GetAxisAlignedBoundingBox().Width;
                    if (_width > listWidth[i])
                    {
                        double heightRowAddCurrent;
                        int countAddRowCurrent;
                        ToolsDrawing.CreateAlligmentText(text, view, listWidth[i], heightRowTransfer, enumContetnHorizontalAlligment.left,
                            out text, out heightRowAddCurrent, out countAddRowCurrent);
                        if (heightRowAddCurrent > heightRowAdd)
                        {
                            heightRowAdd = heightRowAddCurrent;
                            countAddRow = countAddRowCurrent;
                        }

                        text.Delete();
                    }
                }
                else if (listAlligmentContent[i] == enumContetnHorizontalAlligment.left)
                {
                    //Вставка нижний левый угол + смещение по x
                    double dx = 2;
                    TSG.Point insertTextPoint = new TSG.Point(startX, endY + (startY - endY) / 2, 0);
                    TSD.Text text = new TSD.Text(view, insertTextPoint, listContent[i], textAttributes);
                    text.Insert();
                    var sizeText = text.GetAxisAlignedBoundingBox();
                    var width = sizeText.Width / 2;
                    TSG.Vector textVectorMove = new TSG.Vector(width + dx, 0, 0);
                    text.MoveObjectRelative(textVectorMove);
                    text.Modify();
                    double _width = text.GetAxisAlignedBoundingBox().Width;
                    if (_width > listWidth[i])
                    {
                        double heightRowAddCurrent;
                        int countAddRowCurrent;
                        ToolsDrawing.CreateAlligmentText(text, view, listWidth[i], heightRowTransfer, enumContetnHorizontalAlligment.left,
                            out text, out heightRowAddCurrent, out countAddRowCurrent);
                        if (heightRowAddCurrent > heightRowAdd)
                        {
                            heightRowAdd = heightRowAddCurrent;
                            countAddRow = countAddRowCurrent;
                        }

                        text.Delete();
                    }
                }

                #endregion

                startX = endX;
            }

            if(countAddRow>0 && drawAddRow)
            {
                for(int i = 0; i<countAddRow; i++)
                {
                    
                    DrawRowFrame(startXadd, startYadd, heightRowTransfer, listWidth, view, attributes);
                    startYadd -= heightRowTransfer;
                }
            }

            endY = startY - heightRow - heightRowAdd;

        }


        /// <summary>
        /// Вставляет содержимое строки таблицы на виде view
        /// с координатами левого верхнего угла строки (startX, startY) 
        /// с высотой строки heightRow
        /// с шириной столбцов listWidth
        /// с содержимым столбцов listContent
        /// с выравниванием столбцов listAlligmentContent
        /// c настройками текста textAttributes
        /// если необходимо рисовать Frame доп столбцов (если текст шире значения из listWidth), тогда drawAddRow==true
        /// возвращает координату по Y нижней грани строки endY
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="heightRow"></param>
        /// <param name="heightRowTransfer"></param>
        /// <param name="listWidth"></param>
        /// <param name="view"></param>
        /// <param name="listContent"></param>
        /// <param name="listAlligmentContent"></param>
        /// <param name="textAttributes"></param>
        /// <param name="endY"></param>
        public static void SetRowText(double startX, double startY,
            int heightRow, int heightRowTransfer,
            List<int> listWidth, TSD.ViewBase view, List<string> listContent,
            List<enumContetnHorizontalAlligment> listAlligmentContent,
            TSD.Text.TextAttributes textAttributes, bool drawAddRow, 
            out double endY)
        {
            double heightRowAdd = 0;
            int countAddRow = 0;

            double endX;
            endY = startY - heightRow - heightRowAdd;

            double startXadd = startX;
            double startYadd = startY - heightRow;

            for (int i = 0; i < listWidth.Count; i++)
            {
                endX = startX + listWidth[i];
                #region Вставка текста
                if (listContent[i] == "" || listContent[i] == string.Empty)
                {
                    startX = endX;
                    continue;
                }

                if (listAlligmentContent[i] == enumContetnHorizontalAlligment.center)
                {
                    //Если точка вставки по центру ячейки
                    TSG.Point insertTextPoint = new TSG.Point(startX + (endX - startX) / 2, endY + (startY - endY) / 2, 0);
                    TSD.Text text = new TSD.Text(view, insertTextPoint, listContent[i], textAttributes);
                    text.Insert();
                    double _width = text.GetAxisAlignedBoundingBox().Width;
                    if (_width > listWidth[i])
                    {
                        double heightRowAddCurrent;
                        int countAddRowCurrent;
                        ToolsDrawing.CreateAlligmentText(text, view, listWidth[i], heightRowTransfer, enumContetnHorizontalAlligment.left,
                            out text, out heightRowAddCurrent, out countAddRowCurrent);
                        if (heightRowAddCurrent > heightRowAdd)
                        {
                            heightRowAdd = heightRowAddCurrent;
                            countAddRow = countAddRowCurrent;
                        }

                        text.Delete();
                    }
                }
                else if (listAlligmentContent[i] == enumContetnHorizontalAlligment.left)
                {
                    //Вставка нижний левый угол + смещение по x
                    double dx = 15;
                    TSG.Point insertTextPoint = new TSG.Point(startX, endY + (startY - endY) / 2, 0);
                    TSD.Text text = new TSD.Text(view, insertTextPoint, listContent[i], textAttributes);
                    text.Insert();
                    var sizeText = text.GetAxisAlignedBoundingBox();
                    var width = sizeText.Width / 2;
                    TSG.Vector textVectorMove = new TSG.Vector(width + dx, 0, 0);
                    text.MoveObjectRelative(textVectorMove);
                    text.Modify();
                    double _width = text.GetAxisAlignedBoundingBox().Width;
                    if (_width > listWidth[i])
                    {
                        double heightRowAddCurrent;
                        int countAddRowCurrent;
                        ToolsDrawing.CreateAlligmentText(text, view, listWidth[i], heightRowTransfer, enumContetnHorizontalAlligment.left,
                            out text, out heightRowAddCurrent, out countAddRowCurrent);
                        if (heightRowAddCurrent > heightRowAdd)
                        {
                            heightRowAdd = heightRowAddCurrent;
                            countAddRow = countAddRowCurrent;
                        }

                        text.Delete();
                    }
                }

                #endregion

                startX = endX;
            }

            if (countAddRow > 0 && drawAddRow)
            {
                for (int i = 0; i < countAddRow; i++)
                {

                    DrawRowFrame(startXadd, startYadd, heightRowTransfer, listWidth, view);
                    startYadd -= heightRowTransfer;
                }
            }

            endY = startY - heightRow - heightRowAdd;

        }


        /// <summary>
        /// Рисует линию на виде view
        /// с начальной точкой startPoint
        /// с конечной точкой endPoint
        /// c TSD.Line.LineAttributes = lineAttributes
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="view"></param>
        /// <param name="lineAttributes"></param>
        public static void DrawLine(TSG.Point startPoint, TSG.Point endPoint, TSD.View view, TSD.Line.LineAttributes lineAttributes)
        {
            TSD.Line line = new TSD.Line(view, startPoint, endPoint, 0, lineAttributes);
            line.Insert();
        }

        /// <summary>
        /// Рисует линию на виде view
        /// с начальной точкой startPoint
        /// с конечной точкой endPoint
        /// c TSD.Line.LineAttributes = standard
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="view"></param>
        /// <param name="lineAttributes"></param>
        public static void DrawLine(TSG.Point startPoint, TSG.Point endPoint, TSD.View view)
        {
            TSD.Line.LineAttributes lineAttributes = new TSD.Line.LineAttributes();
            TSD.Line line = new TSD.Line(view, startPoint, endPoint, 0, lineAttributes);
            line.Insert();
        }



        /// <summary>
        /// Рисует строку таблицы на виде view
        /// с координатами левой верхней точки строки (startX, startY) 
        /// с высотой строки heightRow
        /// с шириной столбцов listWidth
        /// c TSD.Rectangle.RectangleAttributes=rectangleAttributes
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="heightRow"></param>
        /// <param name="listWidth"></param>
        /// <param name="view"></param>
        public static void DrawRowFrame(double startX, double startY, int heightRow, List<int> listWidth, TSD.ViewBase view, TSD.AttributesBase attributes)
        {
            double endX;
            double endY = startY - heightRow;
            if(attributes is TSD.Rectangle.RectangleAttributes)
            {
                for (int i = 0; i < listWidth.Count; i++)
                {
                    endX = startX + listWidth[i];

                    TSG.Point point1 = new TSG.Point(startX, startY, 0);
                    TSG.Point point2 = new TSG.Point(endX, endY, 0);
                    //TSD.Rectangle.RectangleAttributes rectangleAttributes = new TSD.Rectangle.RectangleAttributes("standard");
                    TSD.Rectangle rectangle = new TSD.Rectangle(view, point1, point2, (TSD.Rectangle.RectangleAttributes)attributes);
                    rectangle.Insert();
                    startX = endX;
                }
            }
            else if(attributes is TSD.Line.LineAttributes)
            {
                int sumListWidth = listWidth.Sum();
                TSG.Point point1 = new TSG.Point(startX, startY - heightRow, 0);
                TSG.Point point2 = new TSG.Point(startX + sumListWidth, startY - heightRow, 0);
                TSD.Line line = new TSD.Line(view, point1, point2, 0, (TSD.Line.LineAttributes)attributes);
                line.Insert();
                
            }
            
        }

        /// <summary>
        /// Рисует строку таблицы на виде view
        /// с координатами левой верхней точки строки (startX, startY) 
        /// с высотой строки heightRow
        /// с шириной столбцов listWidth
        /// c TSD.Rectangle.RectangleAttributes = standard
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="heightRow"></param>
        /// <param name="listWidth"></param>
        /// <param name="view"></param>
        public static void DrawRowFrame(double startX, double startY, int heightRow, List<int> listWidth, TSD.ViewBase view)
        {
            double endX;
            double endY = startY - heightRow;
            for (int i = 0; i < listWidth.Count; i++)
            {
                endX = startX + listWidth[i];
                TSG.Point point1 = new TSG.Point(startX, startY, 0);
                TSG.Point point2 = new TSG.Point(endX, endY, 0);
                TSD.Rectangle.RectangleAttributes rectangleAttributes =
                    new TSD.Rectangle.RectangleAttributes("standard");
                TSD.Rectangle rectangle = new TSD.Rectangle(view, point1, point2, rectangleAttributes);
                rectangle.Insert();
                startX = endX;
            }
        }


        /// <summary>
        /// Если ширина textDrwObj больше limitWidth, 
        /// то строится новый текст с переносом на другую строку,
        /// так чтобы ширина строк текста была не больше limitWidth
        /// </summary>
        /// <param name="textDrwObj"></param>
        /// <param name="view"></param>
        /// <param name="limitWidth"></param>
        /// <param name="heightTransfer"></param>
        /// <param name="alligment"></param>
        /// <param name="textDrwStart"></param>
        /// <param name="heightRowAdd"></param>
        /// <param name="countAddRow"></param>
        public static void CreateAlligmentText(TSD.Text textDrwObj, TSD.ViewBase view,
            double limitWidth, double heightTransfer, enumContetnHorizontalAlligment alligment, 
            out TSD.Text textDrwStart, out double heightRowAdd, out int countAddRow)
        {
            heightRowAdd = 0;
            countAddRow = 0;
            textDrwStart = textDrwObj;
            //Получаем содержимое входного текста
            double widthInputText = textDrwObj.GetAxisAlignedBoundingBox().Width;
            if (widthInputText < limitWidth) return;

            TSD.Text.TextAttributes textAttributes = textDrwObj.Attributes;
            string content = textDrwObj.TextString;
            string[] words_array = content.Split(new char[] { ' ' });
            List<string> words = words_array.ToList();

            double widthText = textDrwObj.GetAxisAlignedBoundingBox().Width;

            double currentYcenter = textDrwObj.InsertionPoint.Y;
            double currentXcenter = textDrwObj.InsertionPoint.X;

            double currentXleft = currentXcenter - widthText / 2;

            widthInputText = textDrwObj.GetAxisAlignedBoundingBox().Width;
            int j_count = 0;
            while ((widthInputText > limitWidth) || words.Count > 0)
            {
                j_count++;
                //widthInputText = textDrwObj.GetAxisAlignedBoundingBox().Width;
                string newTextContentCurrent = string.Empty;
                string newTextContentNext = string.Empty;
                double rotation = Math.Min(limitWidth / widthInputText, 1);
                var count = words.Count * rotation;
                int countInt = Convert.ToInt32(count);
                for (int i = 0; i < countInt; i++)
                {
                    newTextContentCurrent += words[i] + " ";
                }
                double widthCurrent = GetWidthText(newTextContentCurrent, textAttributes, view);

                while(widthCurrent > limitWidth)
                {
                    countInt -= 1;
                    newTextContentCurrent = string.Empty;
                    for (int i = 0; i < countInt; i++)
                    {
                        newTextContentCurrent += words[i] + " ";
                    }
                    widthCurrent = GetWidthText(newTextContentCurrent, textAttributes, view);
                }


                #region Старый код если что вернуть
                //if (widthCurrent > limitWidth)
                //{
                //    countInt -= 1;
                //    newTextContentCurrent = string.Empty;
                //    for (int i = 0; i < countInt; i++)
                //    {
                //        newTextContentCurrent += words[i] + " ";
                //    }
                //}
                //widthCurrent = GetWidthText(newTextContentCurrent, textAttributes, view);
                #endregion


                for (int i = countInt; i < words.Count; i++)
                {
                    newTextContentNext += words[i] + " ";
                }
                if (alligment == enumContetnHorizontalAlligment.center)
                {
                    textDrwObj = new TSD.Text(view,
                    new TSG.Point(currentXcenter, currentYcenter, 0),
                    newTextContentCurrent, textAttributes);
                    textDrwObj.Insert();
                }
                else if (alligment == enumContetnHorizontalAlligment.left)
                {
                    textDrwObj = new TSD.Text(view,
                    new TSG.Point(currentXcenter, currentYcenter, 0),
                    newTextContentCurrent, textAttributes);
                    textDrwObj.Insert();
                    var _x_left = textDrwObj.GetAxisAlignedBoundingBox().LowerLeft.X;
                    TSG.Vector vectorMove = new TSG.Vector(currentXleft - _x_left, 0, 0);
                    textDrwObj.MoveObjectRelative(vectorMove);
                    textDrwObj.Modify();
                }
                currentYcenter -= heightTransfer;
                words.RemoveRange(0, countInt);
                if (words.Count <= 0)
                {
                    heightRowAdd = (j_count-1) * heightTransfer;
                    countAddRow = j_count - 1;
                    return;
                }

                widthInputText = GetWidthText(newTextContentNext, textAttributes, view);
            }
            heightRowAdd = (j_count - 1) * heightTransfer;
            countAddRow = j_count - 1;
        }


        /// <summary>
        /// Возвращает ширину объекта TSD.Text c текстом text,
        /// c настройками текста textAttributes
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textAttributes"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        private static double GetWidthText(string text, TSD.Text.TextAttributes textAttributes, TSD.ViewBase view)
        {

            TSD.Text textObj = new TSD.Text(view, new TSG.Point(), text, textAttributes);
            textObj.Insert();
            double width = textObj.GetAxisAlignedBoundingBox().Width;
            textObj.Delete();
            return width;
        }


        /// <summary>
        /// Возвращает список чисел разделенных "," и "-".
        /// Например по строке "1, 3, 5-12" -> list<int>(){1, 3, 5, 6, 7, 8, 9, 10, 11, 12}
        /// в случае ошибки возвращает пустой список
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<int> ParseNumber(string text)
        {
            string[] words = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> listNumber = new List<int>();
            string w;
            try
            {
                foreach (string word in words)
                {
                    if (word.Contains("-"))
                    {
                        string[] words2 = word.Split(new char[] { '-' });
                        if (words2.Length > 2)
                            return new List<int>();
                        int start = int.Parse(words2[0].Trim());
                        int end = int.Parse(words2[1].Trim());
                        for (int i = start; i <= end; i++)
                            listNumber.Add(i);
                    }
                    else
                    {
                        w = word.Trim();
                        listNumber.Add(int.Parse(w));
                    }
                }
                return listNumber;
            }
            catch 
            {
                return new List<int>();
            }

        }


        /// <summary>
        /// Возвращает список строк разделенных ",". 
        /// Например по строке "КМ1, КМ1.СМ1" -> list<string>() {"КМ1", "КМ1.СМ1"}
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<string> ParseText(string text)
        {
            List<string> listText = new List<string>();
            string[] words = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string w;
            foreach (string word in words)
            {

                w = word.Trim();
                listText.Add(w);

            }
            return listText;
        }



        /// <summary>
        /// Возвращает список строк разбитых так чтобы длина каждой была
        /// не больше заданного количества символов limitCountSymbol
        /// </summary>
        /// <param name="text"></param>
        /// <param name="limitCountSymbol"></param>
        /// <returns></returns>
        public static List<string> GetParseRowLimitWidth(string text, int limitCountSymbol)
        {
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> outPut = new List<string>();
            string currentRow;
            string tempRow;
            int i = 0;
            while (text.Length > 0)
            {
                currentRow = string.Empty;
                tempRow = string.Empty;
                while (currentRow.Length < limitCountSymbol)
                {
                    if (i == words.Length)
                        break;
                    tempRow = currentRow + words[i];

                    if (tempRow.Length > limitCountSymbol)
                        break;
                    currentRow += words[i] + " ";
                    i++;
                }
                int lengthCurrentRow = currentRow.Length;
                int lengthText = text.Length;
                outPut.Add(currentRow.Substring(0, currentRow.Length - 1));
                if (lengthText - lengthCurrentRow <= 0)
                    break;
                text = text.Substring(lengthCurrentRow, lengthText - lengthCurrentRow);
            }
            return outPut;
        }
    }
}
