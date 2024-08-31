using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSC = Tekla.Structures.Catalogs;
using TSM = Tekla.Structures.Model;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Geometry3d;

namespace Jungle_CustomProperties.Extension
{
    public static class ToolsModel
    {
        /// <summary>
        /// Return perimetr of cross section of part
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static double GetPerimetr(TSM.Part part)
        {
            double perimetr = 0;
            string PROFILE_TYPE = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref PROFILE_TYPE);

            string PROFILE_SUBTYPE = string.Empty;
            part.GetReportProperty("PROFILE.SUBTYPE", ref PROFILE_SUBTYPE);

            TSC.ProfileItem.ProfileItemSubTypeEnum subType_profile = GetSubTypeProfile(part);
            if (PROFILE_TYPE == "I")
            {
                if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_I_HOT_ROLLED)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double b = 0;
                    double s = 0;
                    double t = 0;
                    double r1 = 0;
                    double r2 = 0;
                    double fs = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b")
                            b = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t")
                            t = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r1")
                            r1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r2")
                            r2 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "fs")
                            fs = libraryItemParameter.Value;
                    }
                    if (fs == 0)
                        return 2 * b + 2 * (h - 2 * t - 2 * r1) + 2 * (b - s - 2 * r1 - 2 * r2) +
                            4 * (t - r2) +
                            2 * Math.PI * r1 +
                            2 * Math.PI * r2;
                    else
                    {
                        double k = t + (b / 4 - s / 2 - r1) * fs + r1;
                        double h_inner = h - 2 * k;

                        double delta_X = b / 2 - s / 2 - r1 - r2;
                        double delta_Y = delta_X * fs;
                        double length = Math.Sqrt(delta_Y * delta_Y + delta_X * delta_X);

                        double kk = t - (b / 4 - r2) * fs;
                        double t_outer = kk - r2;

                        return 2 * b + 2 * h_inner + 2 * Math.PI * r1 + 4 * length
                            + 2 * Math.PI * r2 + 4 * t_outer;
                    }
                }
                else if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_I_WELDED_SYMMETRICAL)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double b = 0;
                    double s = 0;
                    double t = 0;
                    double r = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b")
                            b = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t")
                            t = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r")
                            r = libraryItemParameter.Value;

                    }
                    return 2 * b + 2 * (h - 2 * t - 2 * r) + 2 * (b - s - 2 * r) +
                        2 * Math.PI * r;
                }
                else if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_I_WELDED_UNSYMMETRICAL)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double b1 = 0;
                    double b2 = 0;
                    double s = 0;
                    double t1 = 0;
                    double t2 = 0;
                    double r = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t1")
                            t1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b1")
                            b1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t2")
                            t2 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b2")
                            b2 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r")
                            r = libraryItemParameter.Value;
                    }
                    return b1 + b2 + 2 * (h - t1 - t2 - 2 * r) +
                        (b1 - s - 2 * r) + (b2 - s - 2 * r) + 2 * Math.PI * r;
                }
            }
            else if (PROFILE_TYPE == "M")
            {
                TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                libraryProfileItem.Select(part.Profile.ProfileString);
                double h = 0;
                double b = 0;
                double t = 0;
                double r = 0;
                foreach (TSC.ProfileItemParameter libraryItemParameter in
                    libraryProfileItem.aProfileItemParameters)
                {
                    if (libraryItemParameter.Symbol == "h")
                        h = libraryItemParameter.Value;
                    else if (libraryItemParameter.Symbol == "b")
                        b = libraryItemParameter.Value;
                    else if (libraryItemParameter.Symbol == "t")
                        t = libraryItemParameter.Value;
                    else if (libraryItemParameter.Symbol == "r")
                        r = libraryItemParameter.Value;
                }
                if (b == 0)
                    return 4 * h + 2 * Math.PI * r - 8 * r;
                else
                    return 2 * b + 2 * h + 2 * Math.PI * r - 8 * r;

            }
            else if (PROFILE_TYPE == "U")
            {
                if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_U_HOT_ROLLED
                    || subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_C_HOT_ROLLED)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);

                    double h = 0;
                    double b = 0;
                    double s = 0;
                    double t = 0;
                    double r1 = 0;
                    double r2 = 0;
                    double fs = 0;

                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")

                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b")
                            b = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t")
                            t = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r1")
                            r1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r2")
                            r2 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "fs")
                            fs = libraryItemParameter.Value;
                    }
                    if (fs == 0)
                        return h + 2 * b + (h - 2 * t) + 2 * (b - s) + 2 * t +
                            Math.PI * r2 + Math.PI * r1 - 4 * r1 - 4 * r2;
                    else
                    {
                        double k = t + (b / 2 - s - r1) * fs + r1;
                        double h_inner = h - 2 * k;

                        double delta_X = (b / 2 - s - r1) + (b / 2 - r2);
                        double delta_Y = delta_X * fs;
                        double length = Math.Sqrt(delta_Y * delta_Y + delta_X * delta_X);

                        double kk = t - (b / 2 - r2) * fs;
                        double t_outer = kk - r2;

                        return h + 2 * b + h_inner + Math.PI * r1 + 2 * length + Math.PI * r2 + 2 * t_outer;
                    }

                }
            }
            else if (PROFILE_TYPE == "L")
            {
                if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_L_HOT_ROLLED)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double b = 0;
                    double t1 = 0;
                    double t2 = 0;
                    double r1 = 0;
                    double r2 = 0;


                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b")
                            b = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t1")
                            t1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t2")
                            t2 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r1")
                            r1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r2")
                            r2 = libraryItemParameter.Value;
                    }

                    return h + b + (h - t1) + (b - t2) + t1 + t2 - 4 * r2
                        - 2 * r1 + Math.PI * r2 + Math.PI * r1 / 2;
                }
            }
            else if (PROFILE_TYPE == "RO")
            {
                if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_PD_CIRCULAR)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double d = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "d")
                            d = libraryItemParameter.Value;
                    }
                    return Math.PI * d;
                }
            }
            else if (PROFILE_TYPE == "RU")
            {
                if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_D_CIRCULAR)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double d = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "d")
                            d = libraryItemParameter.Value;
                    }
                    return Math.PI * d;
                }
            }
            else if (PROFILE_TYPE == "T")
            {
                if (subType_profile == TSC.ProfileItem.ProfileItemSubTypeEnum.PROFILE_T_HOT_ROLLED)
                {
                    TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                    libraryProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double b = 0;
                    double s = 0;
                    double t = 0;
                    double r1 = 0;
                    double r2 = 0;
                    double fs = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        libraryProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b")
                            b = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t")
                            t = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r1")
                            r1 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "r2")
                            r2 = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "fs")
                            fs = libraryItemParameter.Value;
                    }
                    if (fs == 0)
                        return b + (b - s) + s + 2 * t - 4 * r1 + Math.PI * r1;
                }
            }
            else if (PROFILE_TYPE == "Z")
            {
                ///Параметрический Сварной симметричный двутавр из среды Russia
                if (PROFILE_SUBTYPE == "A.HWS")
                {
                    TSC.ParametricProfileItem parametricProfileItem = new TSC.ParametricProfileItem();
                    parametricProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double b = 0;
                    double s = 0;
                    double t = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        parametricProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "b")
                            b = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "t")
                            t = libraryItemParameter.Value;
                    }
                    return 2 * (h - 2 * t) + 2 * b + 2 * (b - s) + 4 * t;

                }
                ///Параметрический Сварной несимметричный двутавр из среды Russia
                else if (PROFILE_SUBTYPE == "A.UHWS")
                {
                    TSC.ParametricProfileItem parametricProfileItem = new TSC.ParametricProfileItem();
                    parametricProfileItem.Select(part.Profile.ProfileString);
                    double h = 0;
                    double bo = 0;
                    double bu = 0;
                    double s = 0;
                    double to = 0;
                    double tu = 0;

                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        parametricProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "h")
                            h = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "bo")
                            bo = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "bu")
                            bu = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "s")
                            s = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "to")
                            to = libraryItemParameter.Value;
                        else if (libraryItemParameter.Symbol == "tu")
                            tu = libraryItemParameter.Value;
                    }
                    return bo + bu + 2 * (h - to - tu) + 2 * to + 2 * tu + (bo - s) + (bu - s);

                }
                ///Квадратный профиль из среды RUSSIA
                else if (PROFILE_SUBTYPE == "SQR")
                {
                    TSC.ParametricProfileItem parametricProfileItem = new TSC.ParametricProfileItem();
                    parametricProfileItem.Select(part.Profile.ProfileString);
                    double a = 0;
                    foreach (TSC.ProfileItemParameter libraryItemParameter in
                        parametricProfileItem.aProfileItemParameters)
                    {
                        if (libraryItemParameter.Symbol == "a")
                            a = libraryItemParameter.Value;
                    }
                    return 4 * a;
                }
            }
            return perimetr;
        }

        /// <summary>
        /// Return reduced thickness whick equal A/P
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static double Get_Reduced_Thickness(TSM.Part part)
        {

            double cross_section_area = -1;
            double perimetr = GetPerimetr(part);
            part.GetReportProperty("PROFILE.CROSS_SECTION_AREA", ref cross_section_area);
            if (cross_section_area == -1)
                return 0;
            if (perimetr == 0)
                return 0;
            return cross_section_area / perimetr;
        }

        /// <summary>
        /// set flag set nut1 in bolt group
        /// </summary>
        /// <param name="boltGroup"></param>
        /// <returns></returns>
        public static bool SetNut1(TSM.BoltGroup boltGroup)
        {
            return boltGroup.Nut1;
        }


        /// <summary>
        /// set flag set nut2 in bolt group
        /// </summary>
        /// <param name="boltGroup"></param>
        /// <returns></returns>
        public static bool SetNut2(TSM.BoltGroup boltGroup)
        {
            return boltGroup.Nut2;
        }

        /// <summary>
        /// set flag set washer1 in bolt group
        /// </summary>
        /// <param name="boltGroup"></param>
        /// <returns></returns>
        public static bool SetWasher1(TSM.BoltGroup boltGroup)
        {
            return boltGroup.Washer1;
        }

        /// <summary>
        /// set flag set washer2 in bolt group
        /// </summary>
        /// <param name="boltGroup"></param>
        /// <returns></returns>
        public static bool SetWasher2(TSM.BoltGroup boltGroup)
        {
            return boltGroup.Washer2;
        }

        /// <summary>
        /// set flag set washer3 in bolt group
        /// </summary>
        /// <param name="boltGroup"></param>
        /// <returns></returns>
        public static bool SetWasher3(TSM.BoltGroup boltGroup)
        {
            return boltGroup.Washer3;
        }


        public static double GetWasher3NumberInOneBolt(TSM.BoltGroup boltGroup)
        {
            int bolt_count = -1;
            boltGroup.GetReportProperty("NUMBER", ref bolt_count);

            int washer3_count = -1;
            boltGroup.GetReportProperty("WASHER.NUMBER3", ref washer3_count);

            return washer3_count / bolt_count;
        }

        private static TSC.ProfileItem.ProfileItemSubTypeEnum GetSubTypeProfile(TSM.Part part)
        {
            string profile = part.Profile.ProfileString;
            TSC.LibraryProfileItem newProfileItem = new TSC.LibraryProfileItem();
            newProfileItem.Select(profile);
            return newProfileItem.ProfileItemSubType;
        }


        private static TSC.ProfileItem.ProfileItemTypeEnum GetTypeProfile(TSM.Part part)
        {
            string profile = part.Profile.ProfileString;
            TSC.LibraryProfileItem newProfileItem = new TSC.LibraryProfileItem();
            newProfileItem.Select(profile);
            return newProfileItem.ProfileItemType;
        }

        /// <summary>
        /// Return name profile KM
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static string GetProfileKM(TSM.Part part)
        {
            string PROFILE_TYPE = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref PROFILE_TYPE);

            string PROFILE_NAME = string.Empty;

            if (PROFILE_TYPE == "B")
            {
                string ru_tip_elementa = string.Empty;
                part.GetReportProperty("ru_tip_elementa", ref ru_tip_elementa);
                if (ru_tip_elementa == "Настил")
                {
                    part.GetReportProperty("ru_proektnoe_imya", ref PROFILE_NAME);
                    return PROFILE_NAME;
                }
                else
                {
                    string profile = part.Profile.ProfileString;

                    int numberSymbolX = profile.IndexOf('*');

                    if (numberSymbolX == -1)
                    {
                        if (profile.Contains("—"))
                            PROFILE_NAME = "t" + profile.Substring(1);
                        else
                            PROFILE_NAME = "t" + profile.Substring(2);
                    }
                    else
                    {
                        if (profile.Contains("—"))
                        {
                            double a = Convert.ToDouble(profile.Substring(1, numberSymbolX - 1));
                            double b = Convert.ToDouble(profile.Substring(numberSymbolX + 1));
                            PROFILE_NAME = "t" + Math.Min(a, b).ToString();
                        }
                        else
                        {
                            double a = Convert.ToDouble(profile.Substring(2, numberSymbolX - 2));
                            double b = Convert.ToDouble(profile.Substring(numberSymbolX + 1));
                            PROFILE_NAME = "t" + Math.Min(a, b).ToString();
                        }
                    }
                }

            }
            else if (PROFILE_TYPE == "Z")
            {
                return "Это профиль типа Z. Еще не сделан";
            }
            else //профили у которых прописан UDA TPL_NAME_FULL
            {
                part.GetReportProperty("PROFILE.TPL_NAME_FULL", ref PROFILE_NAME);
                return PROFILE_NAME;
            }

            return PROFILE_NAME;

        }


        public static string GetProfileKM_Symbol(TSM.Part part)
        {
            string PROFILE_TYPE = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref PROFILE_TYPE);

            string PROFILE_NAME = string.Empty;

            if (PROFILE_TYPE == "B")
            {
                string ru_tip_elementa = string.Empty;
                part.GetReportProperty("ru_tip_elementa", ref ru_tip_elementa);
                if (ru_tip_elementa == "Настил")
                {
                    part.GetReportProperty("ru_proektnoe_imya", ref PROFILE_NAME);
                    return PROFILE_NAME;
                }
                else
                {
                    string profile = part.Profile.ProfileString;

                    int numberSymbolX = profile.IndexOf('*');

                    if (numberSymbolX == -1)
                    {
                        if (profile.Contains("—"))
                            PROFILE_NAME = "t" + profile.Substring(1);
                        else
                            PROFILE_NAME = "t" + profile.Substring(2);
                    }
                    else
                    {
                        if (profile.Contains("—"))
                        {
                            double a = Convert.ToDouble(profile.Substring(1, numberSymbolX - 1));
                            double b = Convert.ToDouble(profile.Substring(numberSymbolX + 1));
                            PROFILE_NAME = "t" + Math.Min(a, b).ToString();
                        }
                        else
                        {
                            double a = Convert.ToDouble(profile.Substring(2, numberSymbolX - 2));
                            double b = Convert.ToDouble(profile.Substring(numberSymbolX + 1));
                            PROFILE_NAME = "t" + Math.Min(a, b).ToString();
                        }
                    }
                }

            }
            else if (PROFILE_TYPE == "Z")
            {
                return "Это профиль типа Z. Еще не сделан";
            }
            else //профили у которых прописан UDA TPL_NAME_FULL
            {
                part.GetReportProperty("PROFILE.SYMBOL_NAME", ref PROFILE_NAME);
                return PROFILE_NAME;
            }

            return PROFILE_NAME;

        }


        /// <summary>
        /// ПРЕОБРАЗУЕТ КООРДИНАТЫ part В ГЛОБАЛЬНУЮ СИСТЕМУ КООРДИНАТ
        /// Возвращает 1 если ось X детали параллельна вектору vector (не считая пластин)
        /// Возврашает 1 если плоскость пластины XOY параллельна vector
        /// </summary>
        /// <param name="part"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static int IsParallel_Vector_IN_GLOBAL(TSM.Part part, TSG.Vector vector)
        {
            bool answer = false;
            string profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);
            TSM.TransformationPlane transformationPlane = new TSM.TransformationPlane(new TSG.Point(0, 0, 0), new TSG.Vector(1, 0, 0), new TSG.Vector(0, 1, 0));
            TSG.Matrix matrix = transformationPlane.TransformationMatrixToGlobal;
            if (part is TSM.Beam beam && profileType != "B")
            {
                TSG.Point pt1 = beam.StartPoint;
                TSG.Point pt2 = beam.EndPoint;

                TSG.Point pt1_global = matrix.Transform(pt1);
                TSG.Point pt2_global = matrix.Transform(pt2);
                TSG.Vector vectorX_beam = new TSG.Vector(pt2_global.X - pt1_global.X, pt2_global.Y - pt1_global.Y, pt2_global.Z - pt1_global.Z);


                answer = TSG.Parallel.VectorToVector(vectorX_beam, vector);
            }
            else if (part is TSM.ContourPlate || profileType == "B")
            {
                TSG.CoordinateSystem coordinateSystem = part.GetCoordinateSystem();
                TSG.Point ptOrigin = coordinateSystem.Origin;
                TSG.Vector vectorNormal = coordinateSystem.AxisX.Cross(coordinateSystem.AxisY);
                TSG.Point pointZ = new TSG.Point(vectorNormal.X, vectorNormal.Y, vectorNormal.Z);
                TSG.Point ptOrigin_global = matrix.Transform(ptOrigin);
                TSG.Point pointZ_global = matrix.Transform(pointZ);
                TSG.Point pointStartVector_global = matrix.Transform(new TSG.Point(0, 0, 0));
                TSG.Vector vectorNormal_global = new TSG.Vector(pointZ_global.X - pointStartVector_global.X,
                    pointZ_global.Y - pointStartVector_global.Y,
                    pointZ_global.Z - pointStartVector_global.Z);
                TSG.GeometricPlane geometricPlane = new TSG.GeometricPlane(ptOrigin_global, vectorNormal_global);
                answer = TSG.Parallel.VectorToPlane(vector, geometricPlane);
            }
            return Convert.ToInt32(answer);
        }

        /// <summary>
        /// ПРЕОБРАЗУЕТ КООРДИНАТЫ part В ГЛОБАЛЬНУЮ СИСТЕМУ КООРДИНАТ
        /// Возвращает 1 если ось X детали параллельна плоскости geometricPlane (не считая пластин)
        /// Возврашает 1 если плоскость пластины XOY параллельна geometricPlane
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="geometricPlane"></param>
        /// <returns></returns>
        public static int IsParallel_Plane_IN_GLOBAL(TSM.Part part, TSG.GeometricPlane geometricPlane)
        {
            bool answer = false;
            string profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);
            TSM.TransformationPlane transformationPlane = new TSM.TransformationPlane(new TSG.Point(0, 0, 0), new TSG.Vector(1, 0, 0), new TSG.Vector(0, 1, 0));
            TSG.Matrix matrix = transformationPlane.TransformationMatrixToGlobal;
            if (part is TSM.Beam beam && profileType != "B")
            {
                TSG.Point pt1 = beam.StartPoint;
                TSG.Point pt2 = beam.EndPoint;

                TSG.Point pt1_global = matrix.Transform(pt1);
                TSG.Point pt2_global = matrix.Transform(pt2);
                TSG.Vector vectorX_beam = new TSG.Vector(pt2_global.X - pt1_global.X, pt2_global.Y - pt1_global.Y, pt2_global.Z - pt1_global.Z);


                answer = TSG.Parallel.VectorToPlane(vectorX_beam, geometricPlane);
            }
            else if (part is TSM.ContourPlate || profileType == "B")
            {
                TSG.CoordinateSystem coordinateSystem = part.GetCoordinateSystem();
                TSG.Point ptOrigin = coordinateSystem.Origin;
                TSG.Vector vectorNormal = coordinateSystem.AxisX.Cross(coordinateSystem.AxisY);
                TSG.Point pointZ = new TSG.Point(vectorNormal.X, vectorNormal.Y, vectorNormal.Z);
                TSG.Point ptOrigin_global = matrix.Transform(ptOrigin);
                TSG.Point pointZ_global = matrix.Transform(pointZ);
                TSG.Point pointStartVector_global = matrix.Transform(new TSG.Point(0, 0, 0));
                TSG.Vector vectorNormal_global = new TSG.Vector(pointZ_global.X - pointStartVector_global.X,
                    pointZ_global.Y - pointStartVector_global.Y,
                    pointZ_global.Z - pointStartVector_global.Z);
                TSG.GeometricPlane geometricPlanePlate = new TSG.GeometricPlane(ptOrigin_global, vectorNormal_global);
                answer = TSG.Parallel.PlaneToPlane(geometricPlane, geometricPlanePlate);
            }
            return Convert.ToInt32(answer);
        }


        /// <summary>
        /// ПРЕОБРАЗУЕТ КООРДИНАТЫ part В ГЛОБАЛЬНУЮ СИСТЕМУ КООРДИНАТ
        /// Возвращает 1 если ось X детали перпендикулярна плоскости geometricPlane (не считая пластин)
        /// Возврашает 1 если нормаль плоскость пластины паралелльная нормали geometricPlane
        /// </summary>
        /// <param name="part"></param>
        /// <param name="geometricPlane"></param>
        /// <returns></returns>
        public static int IsPerpendicular_Plane_IN_GLOBAL(TSM.Part part, TSG.GeometricPlane geometricPlane)
        {
            bool answer = false;
            string profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);
            TSM.TransformationPlane transformationPlane = new TSM.TransformationPlane(new TSG.Point(0, 0, 0), new TSG.Vector(1, 0, 0), new TSG.Vector(0, 1, 0));
            TSG.Matrix matrix = transformationPlane.TransformationMatrixToGlobal;
            if (part is TSM.Beam beam && profileType != "B")
            {
                TSG.Point pt1 = beam.StartPoint;
                TSG.Point pt2 = beam.EndPoint;

                TSG.Point pt1_global = matrix.Transform(pt1);
                TSG.Point pt2_global = matrix.Transform(pt2);
                TSG.LineSegment line = new LineSegment(pt1_global, pt2_global);

                TSG.LineSegment lineSegmentIntersection = TSG.Projection.LineSegmentToPlane(line, geometricPlane);
                double length = lineSegmentIntersection.Length();
                if (length <= 0.00001)
                    answer = true;
            }
            else if (part is TSM.ContourPlate || profileType == "B")
            {
                TSG.CoordinateSystem coordinateSystem = part.GetCoordinateSystem();
                TSG.Point ptOrigin = coordinateSystem.Origin;
                TSG.Vector vectorNormal = coordinateSystem.AxisX.Cross(coordinateSystem.AxisY);
                TSG.Point pointZ = new TSG.Point(vectorNormal.X, vectorNormal.Y, vectorNormal.Z);
                TSG.Point ptOrigin_global = matrix.Transform(ptOrigin);
                TSG.Point pointZ_global = matrix.Transform(pointZ);
                TSG.Point pointStartVector_global = matrix.Transform(new TSG.Point(0, 0, 0));
                TSG.Vector vectorNormal_global = new TSG.Vector(pointZ_global.X - pointStartVector_global.X,
                    pointZ_global.Y - pointStartVector_global.Y,
                    pointZ_global.Z - pointStartVector_global.Z);

                answer = TSG.Parallel.VectorToPlane(vectorNormal_global, geometricPlane);
            }



            return Convert.ToInt32(answer);
        }


        /// <summary>
        /// Возвращает 1 если ось X детали параллельна вектору vector (не считая пластин)
        /// Возврашает 1 если плоскость пластины XOY параллельна vector
        /// </summary>
        /// <param name="part"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static int IsParallel_Vector_IN_WORK_PLANE(TSM.Part part, TSG.Vector vector)
        {
            bool answer = false;
            string profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);

            if (part is TSM.Beam beam && profileType != "B")
            {
                TSG.Point pt1 = beam.StartPoint;
                TSG.Point pt2 = beam.EndPoint;
                TSG.Vector vectorX_beam = new TSG.Vector(pt2.X - pt1.X, pt2.Y - pt1.Y, pt2.Z - pt1.Z);
                return Convert.ToInt32(TSG.Parallel.VectorToVector(vectorX_beam, vector));
            }
            else if (part is TSM.ContourPlate || profileType == "B")
            {
                TSG.CoordinateSystem coordinateSystem = part.GetCoordinateSystem();
                TSG.Point ptOrigin = coordinateSystem.Origin;


                TSG.GeometricPlane geometricPlane = new TSG.GeometricPlane(coordinateSystem);
                return Convert.ToInt32(TSG.Parallel.VectorToPlane(vector, geometricPlane));
            }
            return Convert.ToInt32(answer);
        }


        /// <summary>
        /// Возвращает 1 если ось X детали параллельна плоскости geometricPlane (не считая пластин)
        /// Возврашает 1 если плоскость пластины XOY параллельна geometricPlane
        /// </summary>
        /// <param name="part"></param>
        /// <param name="geometricPlane"></param>
        /// <returns></returns>
        public static int IsParallel_Plane_IN_WORK_PLANE(TSM.Part part, TSG.GeometricPlane geometricPlane)
        {
            bool answer = false;
            string profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);
            TSM.TransformationPlane transformationPlane = new TSM.TransformationPlane(new TSG.Point(0, 0, 0), new TSG.Vector(1, 0, 0), new TSG.Vector(0, 1, 0));
            TSG.Matrix matrix = transformationPlane.TransformationMatrixToGlobal;
            if (part is TSM.Beam beam && profileType != "B")
            {
                TSG.Point pt1 = beam.StartPoint;
                TSG.Point pt2 = beam.EndPoint;
                TSG.Vector vectorX_beam = new TSG.Vector(pt2.X - pt1.X, pt2.Y - pt1.Y, pt2.Z - pt1.Z);


                answer = TSG.Parallel.VectorToPlane(vectorX_beam, geometricPlane);
            }
            else if (part is TSM.ContourPlate || profileType == "B")
            {
                TSG.CoordinateSystem coordinateSystem = part.GetCoordinateSystem();

                TSG.GeometricPlane geometricPlanePlate = new TSG.GeometricPlane(coordinateSystem);
                answer = TSG.Parallel.PlaneToPlane(geometricPlane, geometricPlanePlate);
            }
            return Convert.ToInt32(answer);
        }

        /// <summary>
        /// Возвращает 1 если ось X детали перпендикулярна плоскости geometricPlane (не считая пластин)
        /// Возврашает 1 если нормаль плоскость пластины паралелльная нормали geometricPlane
        /// </summary>
        /// <param name="part"></param>
        /// <param name="geometricPlane"></param>
        /// <returns></returns>
        public static int IsPerpendicular_IN_WORK_PLANE(TSM.Part part, TSG.GeometricPlane geometricPlane)
        {
            bool answer = false;
            string profileType = string.Empty;
            part.GetReportProperty("PROFILE_TYPE", ref profileType);
            TSM.TransformationPlane transformationPlane = new TSM.TransformationPlane(new TSG.Point(0, 0, 0), new TSG.Vector(1, 0, 0), new TSG.Vector(0, 1, 0));
            TSG.Matrix matrix = transformationPlane.TransformationMatrixToGlobal;
            if (part is TSM.Beam beam && profileType != "B")
            {
                TSG.Point pt1 = beam.StartPoint;
                TSG.Point pt2 = beam.EndPoint;
                TSG.LineSegment line = new LineSegment(pt1, pt2);

                TSG.LineSegment lineSegmentIntersection = TSG.Projection.LineSegmentToPlane(line, geometricPlane);
                double length = lineSegmentIntersection.Length();
                if (length <= 0.00001)
                    return Convert.ToInt32(true);
            }
            else if (part is TSM.ContourPlate || profileType == "B")
            {
                TSG.CoordinateSystem coordinateSystem = part.GetCoordinateSystem();
                TSG.Point ptOrigin = coordinateSystem.Origin;
                TSG.Vector vectorNormal = coordinateSystem.AxisX.Cross(coordinateSystem.AxisY);
                return Convert.ToInt32(TSG.Parallel.VectorToPlane(vectorNormal, geometricPlane));
            }
            return Convert.ToInt32(answer);
        }
    }
}
