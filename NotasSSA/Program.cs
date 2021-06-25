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
        class Candidato
        {
            public int inscrição = 0;
            public string nome = string.Empty;
            public float notaSSA1 = 0;
            public float notaSSA2 = 0;
            public float notaSoma = 0;
        }
        static void Main()
        {
            Write("Escreva no nome de um aluno: ");
            Candidato aluno = new() { nome = ReadLine().ToUpperInvariant() };
            WriteLine();

            string SSA2020 = "Notas SSA1_2020.txt";
            string SSA2021 = "Notas SSA2_2021.txt";
            string path = Path.GetFullPath("NotasSSA.exe");
            path = RemoveLastWord(path, @"\");
            IList<string> NotasSSA2020 = Read(path + SSA2020, '1');
            IList<string> NotasSSA2021 = Read(path + SSA2021, '2');

            IList<Candidato> CandidatosSSA1 = new Collection<Candidato>();
            IList<Candidato> CandidatosSSA2 = new Collection<Candidato>();
            IList<Candidato> CandidatosSoma = new Collection<Candidato>();

            foreach (string s in NotasSSA2020)
            {
                Candidato c = new();
                string[] dados = s.Split();
                c.inscrição = int.Parse(dados[0]);
                c.nome = Remove1stWord(RemoveLastWord(s)).Trim();
                c.notaSSA1 = float.Parse(GetLastWord(s));
                CandidatosSSA1.Add(c);
            }

            foreach (string s in NotasSSA2021)
            {
                Candidato c = new();
                string[] dados = s.Split();
                c.inscrição = int.Parse(dados[0]);
                c.nome = Remove1stWord(RemoveLastWord(s)).Trim();
                c.notaSSA2 = float.Parse(GetLastWord(s));
                CandidatosSSA2.Add(c);
            }

            float média1 = 0;
            float notamin1 = 100f;
            foreach (Candidato c in CandidatosSSA1)
            {
                média1 += c.notaSSA1;
                if (c.notaSSA1 < notamin1) notamin1 = c.notaSSA1;
            }
            média1 /= CandidatosSSA1.Count;
            WriteLine("menor nota do ssa1: "+ notamin1);
            WriteLine("média do ssa1: "+ média1);
            WriteLine();

            float média2 = 0;
            float notamin2 = 100f;
            foreach (Candidato c in CandidatosSSA2)
            {
                média2 += c.notaSSA2;
                if (c.notaSSA2 < notamin2) notamin2 = c.notaSSA2;
            }
            média2 /= CandidatosSSA2.Count;
            WriteLine("menor nota do ssa2: "+ notamin2);
            WriteLine("média do ssa2: "+ média2);
            WriteLine();

            int i = 1;
            foreach(Candidato c in CandidatosSSA1)
            {
                foreach(Candidato d in CandidatosSSA2)
                {
                    if(c.nome == d.nome)
                    {
                        Candidato e = new();
                        e.inscrição = i;
                        i++;
                        e.nome = c.nome;
                        e.notaSoma = c.notaSSA1 + d.notaSSA2;
                        CandidatosSoma.Add(e);
                    }
                }
            }

            float médiaS = 0;
            int num = 0;
            float notaminS = 100;

            foreach (Candidato e in CandidatosSoma)
            {
                médiaS += e.notaSoma;
                if (e.notaSoma < notaminS) notaminS = e.notaSoma;
                if (e.nome == aluno.nome) aluno = e;
            }
            médiaS /= CandidatosSoma.Count;

            foreach (Candidato e in CandidatosSoma)
            {
                if (e.notaSoma >= aluno.notaSoma)
                {
                    num++;
                }
            }

            foreach (Candidato c in CandidatosSSA1)
            {
                if (c.nome == aluno.nome)
                {
                    aluno.notaSSA1 = c.notaSSA1;
                }
            }

            foreach (Candidato c in CandidatosSSA2)
            {
                if (c.nome == aluno.nome)
                {
                    aluno.notaSSA2 = c.notaSSA2;
                }
            }
            WriteLine("menor nota da soma: "+ notaminS);
            WriteLine("média da soma: "+ médiaS);
            WriteLine();
            WriteLine("perfil do aluno:");
            WriteLine("quantidade de alunos com nota igual ou acima: " + num);
            WriteLine($"{aluno.inscrição} {aluno.nome} {aluno.notaSoma}");
            WriteLine($"Nota SSA1: {aluno.notaSSA1}");
            WriteLine($"Nota SSA2: {aluno.notaSSA2}");
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
}
