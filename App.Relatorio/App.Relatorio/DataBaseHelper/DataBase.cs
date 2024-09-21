
using App.Relatorio.Models;
using Java.Util.Functions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Agenda.DataBaseHelper
{
    public  class DataBase
    {
        string pasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CriarBancoDeDados()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    conexao.CreateTable<Publicador>();
                    conexao.CreateTable<App.Relatorio.Models.Relatorio>();
                    conexao.CreateTable<App.Relatorio.Models.AnotacoesDoMEs>();
                    conexao.CreateTable<App.Relatorio.Models.Relogio>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                
                return false;
            }
        }

        public bool InserirRelatorio(App.Relatorio.Models.Relatorio relatorio)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    conexao.Insert(relatorio);
                    
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
              
                return false;
            }
        }

        public bool InserirRelogio(App.Relatorio.Models.Relogio relogio)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    conexao.Insert(relogio);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }


        public bool InserirAnotacoes(App.Relatorio.Models.AnotacoesDoMEs anotacao)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    conexao.Insert(anotacao);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }

        public bool InseriPublicador(Publicador publicador)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    conexao.Insert(publicador);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }

        public List<App.Relatorio.Models.Relatorio> GetRelatorios()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    return conexao.Table<App.Relatorio.Models.Relatorio>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
              
                return null;
            }
        }

        public List<App.Relatorio.Models.AnotacoesDoMEs> GetAnotacoes()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    return conexao.Table<App.Relatorio.Models.AnotacoesDoMEs>().ToList();
                }
            }
            catch (SQLiteException ex)
            {

                return null;
            }
        }

        public List<App.Relatorio.Models.Relogio> GetCronometro()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    return conexao.Table<App.Relatorio.Models.Relogio>().ToList();
                }
            }
            catch (SQLiteException ex)
            {

                return null;
            }
        }

        public string GetPublicador()
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    Publicador publicador = new Publicador();
                   publicador = conexao.Table<Publicador>().ToList().FirstOrDefault();
                    if(publicador != null)
                     return publicador.Nome; 
                    else
                    return null;
                }
            }
            catch (SQLiteException ex)
            {

                return null;
            }
        }

        public bool AtualizarPublicador(Publicador publicador)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Alunos.db")))
                {
                    conexao.Query<Publicador>("UPDATE Publicador set Nome=? Where Id=?", publicador.Nome);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
           
                return false;
            }
        }

        public bool DeletarAluno(App.Relatorio.Models.Relatorio relatorio)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                    conexao.Delete(relatorio);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                
                return false;
            }
        }

        public bool DeletarCronometro(App.Relatorio.Models.Relogio relogio)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                 int linhas =   conexao.Delete(relogio);
                    if (linhas > 0)
                        return true;
                    else 
                    return false;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }

        public App.Relatorio.Models.Relatorio GetRelatorio(int Id)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                  return  conexao.Query<App.Relatorio.Models.Relatorio>("SELECT * FROM Relatorio Where Id=?", Id).FirstOrDefault();
                    //conexao.Update(aluno);
                  
                }
            }
            catch (SQLiteException ex)
            {
                
                return null;
            }
        }

        public bool  Update(App.Relatorio.Models.Relatorio relatorio)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {
                   
                   int linhas = conexao.Update(relatorio);
                    if (linhas > 0)
                        return true;
                    else { return false; }
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }

        public bool UpdateCronometro(App.Relatorio.Models.Relogio relogio)
        {
            try
            {
                using (var conexao = new SQLiteConnection(System.IO.Path.Combine(pasta, "Relatorio.db")))
                {

                    int linhas = conexao.Update(relogio);
                    if (linhas > 0)
                        return true;
                    else { return false; }
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }

    }
}
