using System;
using System.IO;
using static System.Console;
using static NotasSSA.String_Handler;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NotasSSA
{
    class Program
    {
        static void Main()
        {
            string SSA2020 = "Notas SSA1_2020.txt";
            string SSA2021 = "Notas SSA2_2021.txt";
            string path = Path.GetFullPath("NotasSSA.exe");
            path = RemoveLastWord(path, @"\");
            IList<string> NotasSSA2020 = Read(path + SSA2020, '1');
            IList<string> NotasSSA2021 = Read(path + SSA2021, '2');

            IList<candidato> Candidatos1 = new Collection<candidato>();
            IList<candidato> Candidatos2 = new Collection<candidato>();
            IList<candidato> CandidatosSoma = new Collection<candidato>();

            foreach (string s in NotasSSA2020)
            {
                candidato c = new();
                string[] dados = s.Split();
                c.inscrição = int.Parse(dados[0]);
                c.nome = Remove1stWord(RemoveLastWord(s)).Trim();
                c.nota = float.Parse(GetLastWord(s));
                Candidatos1.Add(c);
            }

            foreach (string s in NotasSSA2021)
            {
                candidato c = new();
                string[] dados = s.Split();
                c.inscrição = int.Parse(dados[0]);
                c.nome = Remove1stWord(RemoveLastWord(s)).Trim();
                c.nota = float.Parse(GetLastWord(s));
                Candidatos2.Add(c);
            }

            float média1 = 0;
            foreach (var c in Candidatos1)
            {
                média1 += c.nota;
            }
            média1 = média1 / Candidatos1.Count;
            WriteLine(média1);

            float média2 = 0;
            foreach (var c in Candidatos2)
            {
                média2 += c.nota;
            }
            média2 = média2 / Candidatos2.Count;
            WriteLine(média2);

            int i = 0;
            foreach(var c in Candidatos1)
            {
                foreach(var d in Candidatos2)
                {
                    if(c.nome == d.nome)
                    {
                        candidato e = new();
                        e.inscrição = i;
                        e.nome = c.nome;
                        e.nota = c.nota + d.nota;
                        CandidatosSoma.Add(e);
                    }
                }
            }

            float médiaS = 0;
            int num = 0;
            foreach (var e in CandidatosSoma)
            {
                médiaS += e.nota;
                if (e.nota >= 158.5f)
                {
                    num++;
                }
            }
            médiaS = médiaS / CandidatosSoma.Count;
            WriteLine(médiaS);
            WriteLine(num);
            ReadKey(false);
        }

        static IList<string> Read(string file, char initial = char.MinValue)
        {
            IList<string> toReturn = new Collection<string>();
            var sr = new StreamReader(file);
            while(true)
            {
                string read = sr.ReadLine();
                switch (read)
                {
                    case null:
                        return toReturn;
                    default:
                        if (initial == char.MinValue || Verify1stLetter(read, initial))
                        {
                            toReturn.Add(read);
                        }
                        break;
                }
            }
        }
    }

    class candidato
    {
        public int inscrição = 0;
        public string nome = string.Empty;
        public float nota = 0;
    }
}
