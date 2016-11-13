using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpuskDANIIL
{
    public class Method
    {
        /// <summary>
        /// Таблица результатов
        /// </summary>
        public List<Result> FindResult = new List<Result>();

        public struct A
        {
            public double x1;
            public double x2;
            public A(double a, double b)
            {
                x1 = a;
                x2 = b;
            }
        }


        /// ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        //ЭТО ФУНКЦИЯ ОТ ДВУХ ПЕРЕМЕННЫХ
        public double Func(double x1, double x2)
        {
            double res;
            res = Math.Pow(x1, 2) + Math.Pow(x2, 2)*16;
            return res;
        }

        /// ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        //ЭТО ЧАСТНАЯ ПРОИЗВОДНАЯ ПО ПЕРВОЙ ПЕРЕМЕННОЙ
        public double FuncX1(double x1)
        {
            double res;
            res = 2 * x1;

            return res;
        }

        /// ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        //ЭТО ЧАСТНАЯ ПРОИЗВОДНАЯ ПО ВТОРОЙ ПЕРЕМЕННОЙ
        public double FuncX2(double x2)
        {
            double res;
            res = 32 * x2;
            return res;
        }

        public void MethodS(double x10, double x20, double l, double eps)
        {
            //Исходные данные записываются в нулевую строку.
            int i = 0;
            Result R = new Result();
            R.k = i;
            R.x1 = Math.Round(x10, 4);
            R.x2 = Math.Round(x20, 4);
            R.dx1 = FuncX1(x10);
            R.dx2 = FuncX2(x20);
            R.lambda = Math.Round(l, 4);
            R.fA = Math.Round(Func(x10, x20), 4);
            R.alpha = 0;
            R.cosa = 0;
            FindResult.Add(R);

            A A0 = new A(x10, x20); //предыдущая точка
            A Apoint = new A(x10, x20); //текущая точка
            //double lamb = l;
            double Fx = R.fA; //функция от X(k)
            double Fx1 = 0; //функция от X(k-1)
            double epsnow = 1;
            while (Math.Abs(epsnow) > eps)
            {
                i++;
                A0 = Apoint;
                Fx1 = Fx;
                Apoint.x1 = Math.Round((A0.x1 - l * FuncX1(A0.x1)), 4);
                Apoint.x2 = Math.Round((A0.x2 - l * FuncX2(A0.x2)), 4); 
                Fx = Math.Round(Func(Apoint.x1, Apoint.x2), 4);
                while (Fx > Fx1) //здесь меняется знак (> - минимизация, < - максимизация)
                {
                    l = l / 2;
                    Apoint.x1 = Math.Round((A0.x1 - l * FuncX1(A0.x1)), 4);
                    Apoint.x2 = Math.Round((A0.x2 - l * FuncX2(A0.x2)), 4); ;
                    Fx = Math.Round(Func(Apoint.x1, Apoint.x2), 4);
                }

                //write to list
                R = new Result();
                R.k = i;
                R.x1 = Apoint.x1;
                R.x2 = Apoint.x2;
                R.dx1 = FuncX1(Apoint.x1);
                R.dx2 = FuncX2(Apoint.x2);
                R.fA = Fx;

                double ch = R.dx1 * FindResult[i-1].dx1 + R.dx2 * FindResult[i - 1].dx2;
                double zn = Math.Sqrt(Math.Pow(R.dx1, 2) + Math.Pow(R.dx2, 2)) * Math.Sqrt(Math.Pow(FindResult[i - 1].dx1, 2) + Math.Pow(FindResult[i - 1].dx2, 2));
                double cosalpha = ch / zn;
                 //вычисляем лямбду по полученному значению в зависимости от угла
                 cosalpha = Math.Round(cosalpha, 2);
                double alpha = Math.Acos(cosalpha) * 180 / Math.PI;
                if (alpha < 30)
                {
                    l = l * 2;
                }
                else if (alpha > 90)
                {
                    l = l / 3;
                }
                R.lambda = Math.Round(l, 4);

                epsnow = Math.Sqrt(Math.Pow(R.x1 - FindResult[i - 1].x1, 2) + Math.Pow(R.x2 - FindResult[i - 1].x2, 2));
                R.alpha = Math.Round(alpha, 4);
                R.cosa = Math.Round(cosalpha, 4);
                //записываем R в лист
                FindResult.Add(R);

            }
        }
    }
}
