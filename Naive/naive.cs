using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Naive

{
    class Naive
    {
        private double training;
        private calculo probabilidades;

        public double Training { get => training; set => training = value; }
        internal calculo Probabilidades { get => probabilidades; set => probabilidades = value; }

        public Naive(List<String[]> twits, List<int> catego, double training)
        {
            this.training = training;
            List<twitt> evalue= orgnize(twits, catego, out List<twitt> twittoBad, out List<twitt> twittMid, out List<twitt> twittGood,out List<int> salidaOfEvalue);
            category categoriaGood = new category(category.CATEGORY_GOOD, twittGood);
            category categoriaMid = new category(category.CATEGORY_MID, twittMid);
            category categoriaBad = new category(category.CATEGORY_BAD, twittoBad);
            List<category> aux = new List<category> { categoriaBad, categoriaMid, categoriaGood };
            int inici = 0;
            if (categoriaBad.AllWord.Count != 0)
                inici++;

            if(categoriaMid.AllWord.Count != 0)
                inici++;

                  if(categoriaGood.AllWord.Count != 0)
                inici++;

            category[] categorias = new category[inici];
            for (int i = 0, j=0; i < aux.Count; i++)
            {
                if (aux.ElementAt(i).AllWord.Count > 0)
                {
                    categorias[j] = aux.ElementAt(i);
                        j++;
                }
            }
            
            
            probabilidades = new calculo(categorias,  evalue,salidaOfEvalue);
        }

        private List<twitt> orgnize(List<string[]> twits, List<int> catego, out List<twitt> twittoBad, out List<twitt> twittMid, out List<twitt> twittGood, out List<int> salidaOfEvalue)
        {
            salidaOfEvalue = new List<int>();
            List<twitt> retur = new List<twitt>();
            twittoBad = new List<twitt>();
            twittMid = new List<twitt>();
            twittGood = new List<twitt>();
            Random aleatorio = new Random();
            int cantidad =(int)((double) training * twits.Count);
            int d = 0;
            int primerosTraining = 0;
            for (int i = aleatorio.Next(0, cantidad); d <twits.Count; i= aleatorio.Next(0, cantidad+1),d++)
            {
                while (twits.ElementAt(i) == null)
                {
                    i = aleatorio.Next(0, cantidad+1);
                }
                String[] twitWorlds = (String[])twits.ElementAt(i).Clone();
                List<int> count = new List<int>();
                List<String> twitPure = new List<string>();
                for (int j= 0; j < twitWorlds.Length; j ++)
                {
                    if (twits.ElementAt(i)[j] != null)
                    {
                    String palabra = twits.ElementAt(i)[j];
                    twitPure.Add(palabra);
                    twits.ElementAt(i)[j] = null;
                    count.Add(1);
                    for (int x = j+1; x < twitWorlds.Length; x++)
                    {
                        if (twits.ElementAt(i)[x].Equals(palabra))
                        {
                            twits.ElementAt(i)[x] = null;
                                int aux = count.ElementAt(count.Count - 1);
                                count.RemoveAt(count.Count - 1);
                          count.Add(aux+1);
                        }

                    }
                    }
                }
                twits.RemoveAt(i);
                twits.Insert(i, null);
                
                if (catego.ElementAt(i) == category.CATEGORY_BAD&&primerosTraining<cantidad)
                {
                    twittoBad.Add(new twitt(twitWorlds, twitPure, count));
                    primerosTraining++;
                }
                else if (catego.ElementAt(i) == category.CATEGORY_GOOD && primerosTraining < cantidad)
                {
                    twittGood.Add(new twitt(twitWorlds, twitPure, count));
                    primerosTraining++;
                }
                else if(catego.ElementAt(i)==category.CATEGORY_MID && primerosTraining < cantidad)
                {
                    twittMid.Add(new twitt(twitWorlds, twitPure, count));
                    primerosTraining++;
                }
                else
                {
                    salidaOfEvalue.Add(catego.ElementAt(i));
                    retur.Add(new twitt(twitWorlds, twitPure, count));
                }
            }
            return retur;
        }
        public List<String> calcularPro()
        {
            return probabilidades.calclarPro();
        }


        static void Main(string[] args)
        {

            StringReader st = new StringReader(Properties.Resources.test);
            String lina = "";
            List<String[]> twitts = new List<string[]>();
            List<int> catego = new List<int>();
            while ((lina = st.ReadLine()) != null)
            {
                
                int numero = int.Parse(lina);
                String[] twitt;
                List<String[]> listaDeFilas = new List<string[]>();
                lina = st.ReadLine();
                do
                {
                    if (lina.Equals("--"))
                    {
                        break;
                    }
                    listaDeFilas.Add(lina.Split(' '));

                } while ((lina=st.ReadLine())!=null);
                
                
                int palabrasTotales = 0;
                for (int i = 0; i < listaDeFilas.Count; i++)
                {
                    palabrasTotales += listaDeFilas.ElementAt(i).Length;
                }
                twitt = new string[palabrasTotales];
                int pos = 0;
                for (int i = 0; i < listaDeFilas.Count; i++)
                {
                    for (int j = 0; j < listaDeFilas.ElementAt(i).Length; j++)
                    {

                    twitt[pos] = listaDeFilas.ElementAt(i)[j];
                        pos++;
                    }
                }
                twitts.Add(twitt);
                catego.Add(numero);
               

            }
            st.Close();
            try
            {
            if((int)(twitts.Count*0.9)>= twitts.Count||(int)(twitts.Count * 0.9)==0)
            throw new Exception("TIenen que pasar mas de 2 twitts y dar un porcentaje que deje almenos 1 twitt de entrenamiento");
                    List<Naive> g = new List<Naive>();
                Console.WriteLine("Escriba la cantidad de pruebas que quiera ejecutar");
                int numero=int.Parse(Console.ReadLine());
                for (int j = 0; j < numero; j++)
                {
                    g.Add(new Naive(twitts, catego, 0.9));
                }

                for (int i = 0; i < numero; i++)
                {
            List<String> lis = g.ElementAt(i).calcularPro();
                for (int j = 0; j < lis.Count; j++)
                {
                    Console.WriteLine(lis.ElementAt(i));
                    Console.WriteLine(g.ElementAt(i).probabilidades.SalidaOfEvalue.ElementAt(j));
                        Console.WriteLine("--");
                }

                }
            

                
                    
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
