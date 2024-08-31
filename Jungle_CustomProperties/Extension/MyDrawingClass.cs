using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Drawing;
using TSD = Tekla.Structures.Drawing;
using Jungle_CustomProperties.Extension.myEnum;


namespace Jungle_CustomProperties.Extension
{
    public class MyDrawingClass:IComparable<MyDrawingClass>
    {
        
        private TSD.Drawing drawingTekla;
        public string TypeDrawing {  get;}
        public int NumberPage { get; }
        public int NumberRevisionLast { get; }
        public string TypeRevisionLast {  get; }
        public string CodeRevisionLast {  get; }
        public string DescriptionRevisionLast { get; }

        public string Shifr { get; }

        public string FirstFilter { get; }
        public string SecondFilter { get; }

        public string TypeRevisionLastFull { get; }

        public string NameDrawing { get; }

        public string NameDrawingWithRev { get; }

        public string NoteRevision { get; }


        private List<MyView> listMyViewModel = new List<MyView>();
        public List<MyView> ListMyViewModel { get=>listMyViewModel; }


        private List<MyView> listMyViewDetail = new List<MyView>();
        public List<MyView> ListMyViewDetail { get=>listMyViewDetail; }


        private List<MyView> listMyViewSection = new List<MyView>();
        public List<MyView> ListMyViewSection { get => listMyViewSection; }

        private List<MyView> listMyViewSectionByDetail = new List<MyView>();
        public List<MyView> ListMyViewSectionByDetail { get=> listMyViewSectionByDetail; }


        public string TittleView { get; }
        public string TittleDetail { get; } 
        public string TittleSection { get; }
        public string TittleSectionByDetail { get; }
        public string TittleAllViews { get; }

        List<Dictionary<string, string>> ListRevision { get; }

        public MyDrawingClass(TSD.Drawing drawingTekla)
        {
            this.drawingTekla = drawingTekla;
            TypeDrawing = getTypeDrawing();
            NumberPage = getNumberPage();
            ListRevision = getListRevision();
            Shifr = getShifr();
            NumberRevisionLast = getNumberRevisionLast();
            TypeRevisionLast = getTypeRevisionLast();
            TypeRevisionLastFull = getTypeRevisionLastFull();
            CodeRevisionLast = getCodeRevisionLast();
            DescriptionRevisionLast = getDescriptionRevisionLast();
            FirstFilter = drawingTekla.Title1;
            SecondFilter = drawingTekla.Title2;

            NameDrawing = getNameDrawing();

            NameDrawingWithRev = getNameDrawingWithRev();

            NoteRevision = getNoteRevision();

            SetListView();

            TittleView = getFullNameViewModel();
            TittleDetail = getFullNameViewDetail();
            TittleSection = getFullNameSection();
            TittleSectionByDetail = getFullNameSectionByDetail();
            TittleAllViews = getFullNameAllViews();
        }

        /// <summary>
        /// Возвращает описаниые последней ревизии чертежа
        /// </summary>
        /// <returns></returns>
        private string getNoteRevision()
        {
            if (NumberRevisionLast == 0)
                return string.Empty;
            else
                return $"Изм. {NumberRevisionLast} ({TypeRevisionLast})";
        }

        /// <summary>
        /// Возвращает тип чертежа
        /// </summary>
        /// <returns></returns>
        private string getTypeDrawing()
        {
            if (drawingTekla is TSD.GADrawing)
                return "G";
            if (drawingTekla is TSD.AssemblyDrawing) return "A";
            if(drawingTekla is TSD.SinglePartDrawing) return "P";
            if(drawingTekla is TSD.CastUnitDrawing) return "C";
            if(drawingTekla is TSD.MultiDrawing) return "M";
            return "UNKNOWN";

        }


        /// <summary>
        /// Возвращает номер листа, либо -1 если номер листа в текле задан неверно
        /// </summary>
        /// <param name="drawingTekla"></param>
        /// <returns></returns>
        private int getNumberPage()
        {
            string numbList = string.Empty;
            drawingTekla.GetUserProperty("ru_list", ref numbList);
            try 
            { 
                return Int32.Parse(numbList);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Возвращает шифр чертежа (UDA ru_shifr_dop)
        /// </summary>
        /// <returns></returns>
        private string getShifr()
        {
            string shifr = string.Empty;
            drawingTekla.GetUserProperty("ru_shifr_dop", ref shifr);
            return shifr;
        }

        /// <summary>
        /// Возвращает список ревизий чертежа
        /// </summary>
        /// <param name="drawingTekla"></param>
        /// <returns></returns>
        private List<Dictionary<string, string>> getListRevision()
        {
            List<Dictionary<string, string>> ListRevision = new List<Dictionary<string, string>>();
            Dictionary<string, string> stringUserProperties = new Dictionary<string, string>();
            drawingTekla.GetStringUserProperties(out stringUserProperties);
            int countRevisions = stringUserProperties.Where(x => x.Key.Contains("mark_")).Select(x => x.Key).ToList().Count();
            for (int i = 0; i < countRevisions; i++)
            {
                Dictionary<string, string> dictRev = new Dictionary<string, string>();
                if (stringUserProperties.Keys.Contains($"mark_{i + 1}"))
                    dictRev.Add("number_revision", stringUserProperties[$"mark_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text1_{i + 1}"))
                    dictRev.Add("description", stringUserProperties[$"text1_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text2_{i + 1}"))
                    dictRev.Add("type_revision", stringUserProperties[$"text2_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text3_{i + 1}"))
                    dictRev.Add("code_revision", stringUserProperties[$"text3_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text4_{i + 1}"))
                    dictRev.Add("completed_by", stringUserProperties[$"text4_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text5_{i + 1}"))
                    dictRev.Add("checked_by", stringUserProperties[$"text5_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text6_{i + 1}"))
                    dictRev.Add("approved_by", stringUserProperties[$"text6_{i + 1}"]);

                if (stringUserProperties.Keys.Contains($"text5_{i + 1}"))
                    dictRev.Add("number_of_plots", stringUserProperties[$"text5_{i + 1}"]);

                ListRevision.Add(dictRev);
                        
            }    


                return ListRevision;
        }

        /// <summary>
        /// Возвращает номер последней ревизии, 0 - если ревизий нету, либо -1, если номер ревизии задан некорректно 
        /// </summary>
        /// <returns></returns>
        private int getNumberRevisionLast()
        {
            try
            {
                if (ListRevision.Count == 0)
                    return 0;
                string numberRevisionLast = ListRevision[ListRevision.Count - 1]["number_revision"];
                return Int32.Parse(numberRevisionLast);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Возвращает тип последней ревизии, либо пустую строку - если ревизий нету либо не заполнено поле типа ревизии
        /// </summary>
        /// <returns></returns>
        private string getTypeRevisionLast()
        {
            try
            {
                if (ListRevision.Count == 0)
                    return string.Empty;
                return ListRevision[ListRevision.Count - 1]["type_revision"];
                
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Возвращает полное описание тип изменения
        /// </summary>
        /// <returns></returns>
        private string getTypeRevisionLastFull()
        {
            try
            {
                if (TypeRevisionLast.Contains("Зам."))
                    return "Лист заменен";
                if (TypeRevisionLast.Contains("Нов."))
                    return "Новый лист";
                if (TypeRevisionLast.Contains("Анн."))
                    return "Лист аннулирован";
                if (TypeRevisionLast.Contains("Изм."))
                    return "Лист изменен";
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Возвращает код последней ревизии, либо пустую строку - если ревизий нету либо не заполнено поле с кодом ревизии
        /// </summary>
        /// <returns></returns>
        private string getCodeRevisionLast()
        {
            try
            {
                if (ListRevision.Count == 0)
                    return string.Empty;
                return ListRevision[ListRevision.Count - 1]["code_revision"];

            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Возвращает описание последней ревизии, либо пустую строку - если ревизий нету либо не заполнено поле с описанием ревизии
        /// </summary>
        /// <returns></returns>
        private string getDescriptionRevisionLast()
        {
            try
            {
                if (ListRevision.Count == 0)
                    return string.Empty;
                return ListRevision[ListRevision.Count - 1]["description"];

            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Возвращает полное имя списков видов модели
        /// </summary>
        /// <returns></returns>
        private string getFullNameViewModel()
        {
            string fullName = string.Empty;
            foreach(MyView myView in listMyViewModel)
            {
                fullName += myView.MarkForPlot + ". ";
            }
            return fullName.TrimEnd();
        }


        /// <summary>
        /// Возвращает полное имя списков видов узлов
        /// </summary>
        /// <returns></returns>
        private string getFullNameViewDetail()
        {            
            if (listMyViewDetail.Count == 0)
                return string.Empty;            
            if (listMyViewDetail.Count == 1)
                return "Узел " + listMyViewDetail[0].MarkForPlot + ".";
            string fullName = "Узлы";
            foreach (MyView view in listMyViewDetail)
            {
                fullName += " " + view.MarkForPlot + ",";
            }
            fullName = fullName.Substring(0, fullName.Length - 1) + ".";
            return fullName.TrimEnd();
        }


        /// <summary>
        /// Возвращает полное имя списков видов разрезов
        /// </summary>
        /// <returns></returns>
        private string getFullNameSection()
        {
            if(listMyViewSection.Count == 0)
                return string.Empty;
            if(listMyViewSection.Count == 1)
                return "Разрез " + listMyViewSection[0].MarkForPlot + "-" + listMyViewSection[0].MarkForPlot + ".";
            string fullName = "Разрезы";
            foreach(MyView view in listMyViewSection)
            {
                fullName += " " + view.MarkForPlot + "-" + view.MarkForPlot + ",";
            }
            fullName = fullName.Substring(0, fullName.Length - 1) + ".";

            return fullName.TrimEnd();
        }


        /// <summary>
        /// Возвращает полное имя списков видов разрезов по узлам
        /// </summary>
        /// <returns></returns>
        private string getFullNameSectionByDetail()
        {
            if (listMyViewSectionByDetail.Count == 0)
                return string.Empty;
            if (listMyViewSectionByDetail.Count == 1)
                return "Разрез по узлу " + listMyViewSectionByDetail[0].MarkForPlot + "-" + listMyViewSectionByDetail[0].MarkForPlot + ".";
            string fullName = "Разрезы по узлам";
            foreach (MyView view in listMyViewSectionByDetail)
            {
                fullName += " " + view.MarkForPlot + "-" + view.MarkForPlot + ",";
            }
            fullName = fullName.Substring(0, fullName.Length - 1) + ".";

            return fullName.TrimEnd();
        }

        /// <summary>
        /// Возвращает полное имя всех видов на чертеже
        /// </summary>
        /// <returns></returns>
        private string getFullNameAllViews()
        {
            string fullName = string.Empty;
            if (listMyViewModel.Count > 0)
                fullName += getFullNameViewModel();
            if (listMyViewSection.Count > 0)
                fullName += " " + getFullNameSection();
            if(listMyViewDetail.Count > 0)
                fullName += " " + getFullNameViewDetail();
            if (listMyViewSectionByDetail.Count > 0)
                fullName += " " + getFullNameSectionByDetail();
            return fullName;
        }

        public override string ToString()
        {
            return $"Рев: {NumberRevisionLast}; Тип чертежа {TypeDrawing}; {NumberPage}";
        }


        /// <summary>
        /// Возвращает имя чертежа
        /// </summary>
        /// <returns></returns>
        private string getNameDrawing()
        {
            string name1 = string.Empty;
            drawingTekla.GetUserProperty("ru_naz_chert_1", ref name1);

            string name2 = string.Empty;
            drawingTekla.GetUserProperty("ru_naz_chert_2", ref name2);

            string name3 = string.Empty;
            drawingTekla.GetUserProperty("ru_naz_chert_3", ref name3);

            string name4 = string.Empty;
            drawingTekla.GetUserProperty("ru_naz_chert_4", ref name4);
            
            List<string> list = new List<string>() { name1, name2, name3, name4};
            return string.Join(" ", list);
        }

        /// <summary>
        /// Возвращает имя чертежа с ревизией
        /// </summary>
        /// <returns></returns>
        private string getNameDrawingWithRev()
        {
            string name = getNameDrawing();
            int lastRev = getNumberRevisionLast();
            if(lastRev == 0)
                return name;
            else
                return name + " Рев." + lastRev.ToString();
        }

        /// <summary>
        /// Устанававливает списки 
        /// listMyViewModel
        /// listMyViewDetail
        /// listMyViewSection
        /// listMyViewSectionByDetail
        /// </summary>
        private void SetListView()
        {
            List<MyView> listSectionAll = new List<MyView>();
            List<string> namesSectionByDetail = new List<string>();

            TSD.ContainerView sheet = drawingTekla.GetSheet();
            TSD.DrawingObjectEnumerator drawingObjectEnumerator = sheet.GetViews();
            while (drawingObjectEnumerator.MoveNext())
            {
                TSD.View view = drawingObjectEnumerator.Current as TSD.View;
                if (view.ViewType is TSD.View.ViewTypes.ModelView)
                {
                    listMyViewModel.Add(new MyView(view));
                }
                else if(view.ViewType  is TSD.View.ViewTypes.DetailView)
                {
                    MyView myView = new MyView(view);
                    listMyViewDetail.Add(myView);
                    List<string> listNamesSectionCurrentDetail = myView.ListSectionMarkStr;
                    namesSectionByDetail.AddRange(listNamesSectionCurrentDetail);
                }
                else if(view.ViewType is TSD.View.ViewTypes.SectionView)
                {
                    listSectionAll.Add(new MyView(view));
                }
            }

            foreach(MyView view in listSectionAll)
            {
                if (namesSectionByDetail.Contains(view.Name))
                {
                    view.setViewSectionByDetail();
                    listMyViewSectionByDetail.Add(view);
                }
                else
                    listMyViewSection.Add(view);

                listMyViewModel.Sort();
                listMyViewDetail.Sort();
                listMyViewSection.Sort();
                listMyViewSectionByDetail.Sort();

            }
        }

        /// <summary>
        /// Сортировка по номеру чертежа
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(MyDrawingClass other)
        {           
            return NumberPage.CompareTo(other.NumberPage);            
        }
    }


    /// <summary>
    /// Класс для сортировки объектов MyDrawingClass
    /// 1. По номеру последней ревизии чертежа
    /// 2. По ключу сортировки типа чертежа . Например G, A, P
    /// 3. По номеру листа чертежа
    /// </summary>
    public class MyDrawingSort__LastRev_TypeDrw_NumberPage : IComparer<MyDrawingClass>
    {

        List<string> ListKey { get; }

        public MyDrawingSort__LastRev_TypeDrw_NumberPage(List<string> listKey)
        {
            ListKey = listKey;
        }



        public int Compare(MyDrawingClass drw1, MyDrawingClass drw2)
        {
            int comparisonFirst = drw1.NumberRevisionLast.CompareTo(drw2.NumberRevisionLast);
            int comparisonSecond = ListKey.IndexOf(drw1.TypeDrawing) - ListKey.IndexOf(drw2.TypeDrawing);

            if (comparisonFirst!=0)
                return comparisonFirst;
            else if(comparisonSecond!=0) return comparisonSecond;
            else
                return drw1.NumberPage.CompareTo(drw2.NumberPage);

        }
    }


    /// <summary>
    /// Класс для сортировки объектов MyDrawingClass по номеру листа
    /// </summary>
    public class MyDrawingComparerByNumberPage : IComparer<MyDrawingClass>
    {
        public int Compare(MyDrawingClass drw1, MyDrawingClass drw2)
        {
            return drw1.NumberPage.CompareTo(drw2.NumberPage);
        }
    }

    /// <summary>
    /// Класс для сортировки объектов MyDrawingClass
    /// 1. По номеру последеней ревизии
    /// 2. По номеру листа чертежа
    /// </summary>
    public class MyDrawingSort_LastRev_NumberPage : IComparer<MyDrawingClass>
    {
        public int Compare(MyDrawingClass drw1, MyDrawingClass drw2)
        {
            int comparisonFirst = drw1.NumberRevisionLast.CompareTo(drw2.NumberRevisionLast);
            if (comparisonFirst != 0)
                return comparisonFirst;
            else return drw1.NumberPage.CompareTo(drw2.NumberPage);
        }
    }


}
