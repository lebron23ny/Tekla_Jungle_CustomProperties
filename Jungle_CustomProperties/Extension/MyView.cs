using System;
using System.Collections.Generic;
using TSD = Tekla.Structures.Drawing;
using Jungle_CustomProperties.Extension.myEnum;

namespace Jungle_CustomProperties.Extension
{
    public class MyView: IComparable<MyView>
    {
        public string Name { get; }

        private TSD.View.ViewTypes viewType;

        private enumMyTypeView myViewType;
        public enumMyTypeView MyViewType { get => myViewType; }


        public List<TSD.SectionMark> ListSectionsMark { get; }

        public List<string> ListSectionMarkStr { get; }

        public List<TSD.DetailMark> ListDetailMark { get; }

        public List<string> ListDetailMarkStr { get; }

        public string Tag1Content { get; }
        public string Tag2Content { get; }
        public string Tag3Content { get; }
        public string Tag4Content { get; }
        public string Tag5Content { get; }


        private string markForPlot;
        public string MarkForPlot { get => markForPlot; }

        
        
        private TSD.View viewTekla;

        /// <summary>
        /// Коснтруктор
        /// </summary>
        /// <param name="viewTekla"></param>
        public MyView(TSD.View viewTekla)
        {
            Name = viewTekla.Name;
            viewType = viewTekla.ViewType;
            this.viewTekla = viewTekla;
            ListSectionsMark = getSectionMark();
            ListDetailMark = getDetailMark();
            ListSectionMarkStr = getSectionMarkStr();
            ListDetailMarkStr = getDetailMarkStr();
            Tag1Content = getTag1();
            Tag2Content = getTag2();
            Tag3Content = getTag3();
            Tag4Content = getTag4();
            Tag5Content = getTag5();
            markForPlot = getMarkForPlot();
            myViewType = getMyTypeView();
        }

        /// <summary>
        /// Возвращает список SectionMark,
        /// размещенных на данном виде
        /// </summary>
        /// <returns></returns>
        private List<TSD.SectionMark> getSectionMark()
        {
            List<TSD.SectionMark> listSectionsMark = new List<TSD.SectionMark>();
            Type[] typeFilter = new Type[] { typeof(TSD.SectionMark) };
            var objects_enum = viewTekla.GetObjects(typeFilter);
            while (objects_enum.MoveNext())
            {
                listSectionsMark.Add(objects_enum.Current as  TSD.SectionMark);
            }
            return listSectionsMark;
        }


        /// <summary>
        /// Возвращает список имен Разрезов, 
        /// размещенных на данном виде
        /// </summary>
        /// <returns></returns>
        private List<string> getSectionMarkStr()
        {
            List<string> listSectionMarkStr = new List<string>();
            Type[] typeFilter = new Type[] { typeof(TSD.SectionMark) };
            var objects_enum = viewTekla.GetObjects(typeFilter);
            while (objects_enum.MoveNext())
            {
                string name = (objects_enum.Current as TSD.SectionMark).Attributes.MarkName;
                string fullName;
                if (viewTekla.ViewType is TSD.View.ViewTypes.DetailView)
                    fullName = name;
                else
                    fullName = name;
                listSectionMarkStr.Add(fullName);
            }
            return listSectionMarkStr;
        }

        /// <summary>
        /// Возвращает список DetailMark (узлов),
        /// размещенных на данном виде
        /// </summary>
        /// <returns></returns>
        private List<TSD.DetailMark> getDetailMark()
        {
            List<TSD.DetailMark> ListDetails = new List<TSD.DetailMark>();
            Type[] typeFilter = new Type[] { typeof(TSD.DetailMark) };
            var objects_enum = viewTekla.GetObjects(typeFilter);
            while (objects_enum.MoveNext())
            {
                ListDetails.Add(objects_enum.Current as TSD.DetailMark);
            }
            return ListDetails;
        }


        /// <summary>
        /// Возвращает список имен Узлов,
        /// размещенных на данном виде
        /// </summary>
        /// <returns></returns>
        private List<string> getDetailMarkStr()
        {
            List<string> ListDetailsStr = new List<string>();
            Type[] typeFilter = new Type[] { typeof(TSD.DetailMark) };
            var objects_enum = viewTekla.GetObjects(typeFilter);
            while (objects_enum.MoveNext())
            {
                string name = (objects_enum.Current as TSD.DetailMark).Attributes.MarkName;
                string fullName = name;
                ListDetailsStr.Add(fullName);
            }
            return ListDetailsStr;
        }

        /// <summary>
        /// Возвращает содержимое Tag1 подписи вида
        /// </summary>
        /// <returns></returns>
        private string getTag1()
        {
            string nameTag1 = string.Empty;

            TSD.View.ViewAttributes atrributesTags = viewTekla.Attributes as TSD.View.ViewAttributes;
            TSD.View.ViewMarkTagsAttributes tags = atrributesTags.TagsAttributes;
            TSD.View.ViewMarkTagAttributes tags1 = tags.TagA1;
            TSD.ContainerElement contentTag1 = tags1.TagContent;
            var tag1Enumerator = contentTag1.GetEnumerator();
            while (tag1Enumerator.MoveNext())
            {
                if (tag1Enumerator.Current is TSD.TextElement text)
                    nameTag1 += text.Value;
                if (tag1Enumerator.Current is TSD.PropertyElement propertyElement)
                    nameTag1 += propertyElement.Value;
                if (tag1Enumerator.Current is TSD.SpaceElement spaceElement)
                    nameTag1 += " ";
            }

            return nameTag1;
        }


        /// <summary>
        /// Возвращает содержимое Tag2 подписи вида
        /// </summary>
        /// <returns></returns>
        private string getTag2()
        {
            string nameTag2 = string.Empty;

            TSD.View.ViewAttributes atrributesTags = viewTekla.Attributes as TSD.View.ViewAttributes;
            TSD.View.ViewMarkTagsAttributes tags = atrributesTags.TagsAttributes;
            TSD.View.ViewMarkTagAttributes tags2 = tags.TagA2;
            TSD.ContainerElement contentTag1 = tags2.TagContent;
            var tag1Enumerator = contentTag1.GetEnumerator();
            while (tag1Enumerator.MoveNext())
            {
                if (tag1Enumerator.Current is TSD.TextElement text)
                    nameTag2 += text.Value;
                if (tag1Enumerator.Current is TSD.PropertyElement propertyElement)
                    nameTag2 += propertyElement.Value;
                if (tag1Enumerator.Current is TSD.SpaceElement spaceElement)
                    nameTag2 += " ";
            }

            return nameTag2;
        }


        /// <summary>
        /// Возвращает содержимое Tag2 подписи вида
        /// </summary>
        /// <returns></returns>
        private string getTag3()
        {
            string nameTag3 = string.Empty;

            TSD.View.ViewAttributes atrributesTags = viewTekla.Attributes as TSD.View.ViewAttributes;
            TSD.View.ViewMarkTagsAttributes tags = atrributesTags.TagsAttributes;
            TSD.View.ViewMarkTagAttributes tags3 = tags.TagA3;
            TSD.ContainerElement contentTag = tags3.TagContent;
            var tagEnumerator = contentTag.GetEnumerator();
            while (tagEnumerator.MoveNext())
            {
                if (tagEnumerator.Current is TSD.TextElement text)
                    nameTag3 += text.Value;
                if (tagEnumerator.Current is TSD.PropertyElement propertyElement)
                    nameTag3 += propertyElement.Value;
                if (tagEnumerator.Current is TSD.SpaceElement spaceElement)
                    nameTag3 += " ";
            }

            return nameTag3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string getTag4()
        {
            string nameTag4 = string.Empty;

            TSD.View.ViewAttributes atrributesTags = viewTekla.Attributes as TSD.View.ViewAttributes;
            TSD.View.ViewMarkTagsAttributes tags = atrributesTags.TagsAttributes;
            TSD.View.ViewMarkTagAttributes tags4 = tags.TagA4;
            TSD.ContainerElement contentTag = tags4.TagContent;
            var tagEnumerator = contentTag.GetEnumerator();
            while (tagEnumerator.MoveNext())
            {
                if (tagEnumerator.Current is TSD.TextElement text)
                    nameTag4 += text.Value;
                if (tagEnumerator.Current is TSD.PropertyElement propertyElement)
                    nameTag4 += propertyElement.Value;
                if (tagEnumerator.Current is TSD.SpaceElement spaceElement)
                    nameTag4 += " ";
            }

            return nameTag4;
        }

        /// <summary>
        /// Возвращает содержимое Tag3 подписи вида
        /// </summary>
        /// <returns></returns>
        private string getTag5()
        {
            string nameTag5 = string.Empty;

            TSD.View.ViewAttributes atrributesTags = viewTekla.Attributes as TSD.View.ViewAttributes;
            TSD.View.ViewMarkTagsAttributes tags = atrributesTags.TagsAttributes;
            TSD.View.ViewMarkTagAttributes tags5 = tags.TagA5;
            TSD.ContainerElement contentTag = tags5.TagContent;
            var tagEnumerator = contentTag.GetEnumerator();
            while (tagEnumerator.MoveNext())
            {
                if (tagEnumerator.Current is TSD.TextElement text)
                    nameTag5 += text.Value;
                if (tagEnumerator.Current is TSD.PropertyElement propertyElement)
                    nameTag5 += propertyElement.Value;
                if (tagEnumerator.Current is TSD.SpaceElement spaceElement)
                    nameTag5 += " ";
            }

            return nameTag5;
        }


        /// <summary>
        /// Возвращает имя вида для вывода 
        /// </summary>
        /// <returns></returns>
        private string getMarkForPlot()
        {
            string markForPlot = string.Empty;
            if (viewType is TSD.View.ViewTypes.SectionView)
                return Name;
            if (viewType is TSD.View.ViewTypes.DetailView)
                return Name;
            if (viewType is TSD.View.ViewTypes.ModelView)
                return Tag1Content;
            return markForPlot;
        }


        /// <summary>
        /// Возварщает enumMyTypeView
        /// </summary>
        /// <returns></returns>
        private enumMyTypeView getMyTypeView()
        {

            if (viewType is TSD.View.ViewTypes.SectionView)
                return enumMyTypeView.sectionByView;
            if (viewType is TSD.View.ViewTypes.DetailView)
                return enumMyTypeView.detail;
            if (viewType is TSD.View.ViewTypes.ModelView)
                return enumMyTypeView.sectionByView;
            return enumMyTypeView.unknownView;
        }




        /// <summary>
        /// Устанавливает myViewType = enumMyTypeView.sectionByDetail,
        /// ViewType is TSD.View.ViewTypes.SectionView
        /// Создан только для того чтобы отличить разрез по виду модели и разрез по узлу
        /// </summary>
        public void setViewSectionByDetail()
        {
            if (viewType is TSD.View.ViewTypes.SectionView)
                myViewType = enumMyTypeView.sectionByDetail;
        }

        /// <summary>
        /// Переопределенный метод ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return viewType.ToString() + " " + MarkForPlot;
        }

        /// <summary>
        /// Переопределенный метод CompareTo
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int CompareTo(MyView other)
        {
            return markForPlot.CompareTo(other.markForPlot);
        }
    }

}
