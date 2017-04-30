using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PO
{
    class Storage
    {
        List<double> x1_value = new List<double>();
        List<double> x2_value = new List<double>();
        List<double> correct = new List<double>();

        public double[] this[int index]
        {
            set
            {
                x1_value.Add(value[0]);
                x2_value.Add(value[1]);
                correct.Add(value[2]);
            }
            get
            {
                double[] array = new double[3];
                array[0] = x1_value[index];
                array[1] = x2_value[index];
                array[2] = correct[index];
                return array;
            }
        }

        public int Count
        {
            get
            {
                return correct.Count;
            }
        }

        public Storage()
        {
            List<double> x1_value = new List<double>();
            List<double> x2_value = new List<double>();
            List<double> correct = new List<double>();
        }

        public void Add(double x1, double x2, double res)
        {
            x1_value.Add(x1);
            x2_value.Add(x2);
            correct.Add(res);
        }
    }


    class Method
    {
        double x1, x2;
        double f_x;
        double k1, k2, k3, k4, shag, toch;
        double d_x1, d_x2, d_FX;
        double x1_n = 0, x2_n = 0;
        const double change_p = 0.1;
        Storage stor = new Storage();

        public Method(double x1, double x2, double k1, double k2, double k3, double k4, double shag, double toch)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.k1 = k1;
            this.k2 = k2;
            this.k3 = k3;
            this.k4 = k4;
            this.shag = shag;
            this.toch = toch;
            f_x = F_X(this.x1, this.x2);
            stor.Add(this.x1, this.x2, 1);
            d_x2 = x2;
        }

        protected double F_X(double x1, double x2)
        {

            return k1 * Math.Pow(x1, 2) + k2 * Math.Pow(x2, 2) + k3 * x1 * x2 + Math.Abs(k4 * Math.Sin(x1 * x2));
        }

        protected double plus1()
        {
            d_x1 = x1 + shag;
            d_FX = F_X(d_x1, d_x2);
            return d_FX;
        }

        protected double minus1()
        {
            d_x1 = x1 - shag;
            d_FX = F_X(d_x1, d_x2);
            return d_FX;
        }

        protected double plus2()
        {
            d_x2 = x2 + shag;
            d_FX = F_X(d_x1, d_x2);
            return d_FX;
        }

        protected double minus2()
        {
            d_x2 = x2 - shag;
            d_FX = F_X(d_x1, d_x2);
            return d_FX;
        }

        protected int seach_by_sample()
        {
            double[] mas = new double[3];
            double f_x_min = F_X(x1, x2), f_x_prost = f_x_min;
            int i = 0;
            while (f_x_prost <= f_x_min)
            {
                if (i != 0)
                {
                    i++;
                    x1 += x1_n; x2 += x2_n;
                    f_x_prost = F_X(x1, x2);
                    if (f_x_min > f_x_prost)
                    {
                        stor.Add(x1, x2, 1);
                        f_x_min = f_x_prost;
                    }
                    else
                    {
                        stor.Add(x1, x2, 0);
                        mas = stor[stor.Count - 1];
                        x1 = mas[0];
                        x2 = mas[1];
                        stor.Add(x1, x2, 1);
                    }
                    if (i > 300)
                    {
                        return 0;
                    }
                }
                else
                {
                    f_x_prost = F_X(x1, x2);
                    stor.Add(x1, x2, 1);
                    i++;
                }
            }
            return 1;
        }


        protected void search_x1()
        {
            if (plus1() > f_x)
            {
                stor.Add(d_x1, d_x2, 0);
                if (minus1() < f_x)
                {
                    stor.Add(d_x1, d_x2, 1);
                    f_x = d_FX;
                    x1_n = -change_p;
                }
                else
                {
                    stor.Add(d_x1, d_x2, 0);
                    x1_n = 0;
                }
            }
            else
            {
                stor.Add(d_x1, d_x2, 1);
                f_x = d_FX;
                x1_n = +change_p;
            }
        }

        protected void search_x2()
        {
            if (plus2() > f_x)
            {
                stor.Add(d_x1, d_x2, 0);
                if (minus2() < f_x)
                {
                    stor.Add(d_x1, d_x2, 1);
                    f_x = d_FX;
                    x2_n = -change_p;
                }
                else
                {
                    stor.Add(d_x1, d_x2, 0);
                    x2_n = 0;
                }
            }
            else
            {
                stor.Add(d_x1, d_x2, 1);
                f_x = d_FX;
                x2_n = +change_p;
            }
        }

        public Storage Method_Start()
        {
        Link: do
            {
                search_x1();
                search_x2();
                if (shag < toch)
                {
                    return stor;
                }
                if (x1_n == 0 && x2_n == 0)
                {
                    shag--;
                }
            }
            while (x1_n == 0 && x2_n == 0);

            if (seach_by_sample() == 0)
            {
                return stor;
            }
            else
            {
                goto Link;
            }
        }
    }
}