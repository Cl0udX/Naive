using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive
{
    class calculo
    {
        private category[] catego;
        private List<twitt> evalue;
        private List<int> salidaOfEvalue;


        public  calculo(category[] catego, List<twitt> evalue, List<int> salidaOfEvalue)
        {
            this.catego = catego;
            this.evalue = evalue;
            this.salidaOfEvalue = salidaOfEvalue;
        }

        public List<int> SalidaOfEvalue { get => salidaOfEvalue; set => salidaOfEvalue = value; }
        internal category[] Catego { get => catego; set => catego = value; }
        internal List<twitt> Evalue { get => evalue; set => evalue = value; }

        public List<String> calclarPro()
        {
            List<String> resultados = new List<string>();
            int totalDatos = 1;
            for (int i = 0; i < catego.Length; i++)
            {
                if(i==0)
                totalDatos = 0;

                totalDatos += catego[i].Twitts.Count;
            }
            List<List<double>> conjuntoDeTriples = new List<List<double>>();

            for (int i = 0; i < catego.Length; i++)
            {
                List<double> categoria = new List<double>();
                double probaCatego = (double)catego[i].Twitts.Count / totalDatos;
                for (int j = 0; j < evalue.Count; j++)
                {
                    double wordDadoCatego = probaCatego;

                    for (int k = 0; k < evalue.ElementAt(j).PureTwitt.Count; k++)
                    {
                        int index = catego.ElementAt(i).AllWord.FindLastIndex(inde => inde.Equals(evalue.ElementAt(j).PureTwitt.ElementAt(k)));
                        if (index != -1)
                        {
                            double exp = Math.Exp(-Math.Pow((evalue.ElementAt(j).WorldsCount.ElementAt(k) - catego.ElementAt(i).Promedio.ElementAt(index)), 2));
                            double tota = (double)((1 / Math.Pow(2 * Math.PI * catego.ElementAt(i).VarianzaS.ElementAt(index), 1 / 2)) * exp);
                            if (tota != 0)
                            {
                            wordDadoCatego *=tota;
                            }

                        }
                    }

                    categoria.Add(wordDadoCatego);

                }
                conjuntoDeTriples.Add(categoria);
            }
            for (int i = 0; i < conjuntoDeTriples.ElementAt(0).Count; i++)
            {
                double max = 0.0;
                int index = -1;
                for (int j = 0; j < conjuntoDeTriples.Count; j++)
                {
                    if (conjuntoDeTriples.ElementAt(j).ElementAt(i) > max)
                    {
                        max = conjuntoDeTriples.ElementAt(j).ElementAt(i);
                        index = j;
                    }
                }
                index--;
                switch (index)
                {
                    case category.CATEGORY_BAD:
                        resultados.Add("Bad");
                        break;
                    case category.CATEGORY_MID:
                        resultados.Add("Neutral");
                        break;
                    default:
                        resultados.Add("Good");
                    
                        break;
                }

            }

            return resultados;
            
        }
        
    }
}
