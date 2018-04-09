using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive

{
    class category
    {
        public  const int CATEGORY_GOOD= 1;
        public const int CATEGORY_BAD = -1;
        public const int CATEGORY_MID = 0;
        private int categoria;
        private List<twitt> twitts;
        private List<String> allWord;
        private List<double> promedio;
        private List<double> varianzaS;
        public category(int category, List<twitt> twitts)
        {
            categoria = category;
            this.twitts = twitts;
            generarPromedios();


        }

        public int Categoria { get => categoria; set => categoria = value; }
        public List<string> AllWord { get => allWord; set => allWord = value; }
        public List<double> Promedio { get => promedio; set => promedio = value; }
        public List<double> VarianzaS { get => varianzaS; set => varianzaS = value; }
        internal List<twitt> Twitts { get => twitts; set => twitts = value; }

        private void generarPromedios()
        {
            allWord = new List<string>();
            promedio = new List<double>();
            varianzaS = new List<double>();
            
            for (int i = 0; i < twitts.Count; i++)
            {
                for (int j = 0; j < twitts.ElementAt(i).PureTwitt.Count; j++)
                {
                    String find = twitts.ElementAt(i).PureTwitt.ElementAt(j);
                    if (allWord.Find(item => item.Equals(find))==null)
                    {
                        int count = twitts.ElementAt(i).WorldsCount.ElementAt(j);
                        int total = 1;
                        List<int> datosVarS = new List<int>(count);
                        allWord.Add(find);
                        for (int x = 0; x < twitts.Count; x++)
                        {
                            int index = -1;
                            if (x != i && (index = twitts.ElementAt(x).PureTwitt.FindLastIndex(item => item.Equals(find))) != -1)
                            {
                                total++;
                                datosVarS.Add(twitts.ElementAt(x).WorldsCount.ElementAt(index));
                                count += twitts.ElementAt(x).WorldsCount.ElementAt(index);
                            }
                        }
                        double promedi = (double)count / total;
                        promedio.Add(promedi);
                        double varS = 0;
                        for (int x = 0; x < datosVarS.Count; x++)
                        {
                            varS += Math.Pow((datosVarS.ElementAt(x) - promedi), 2);
                        }
                        varS = varS / (datosVarS.Count - 1);
                        varianzaS.Add(varS);

                    }



                }
            }

        }

    }
}
