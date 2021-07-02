using desafioTDSA.Log;
using desafioTDSA.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace desafioTDSA.Action
{
    public class UsuarioAction
    {
        private Conn Connection = new Conn();

        public List<UsuarioModel> Select()
        {
            try
            {
                List<UsuarioModel> retorno = new List<UsuarioModel>();

                string query = $@"
                                SELECT 
	                                CLI_ID,
                                    CLI_NOME,
                                    CLI_DATANASCIMENTO,
                                    CLI_ATIVO
                                FROM CLIENTETDSA";

                DataTable tabela = Connection.SqlDataTable(query);

                if (tabela.Rows.Count <= 0)
                    return null;

                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    UsuarioModel usuarioModel = new UsuarioModel()
                    {
                        CLI_ID = (int)tabela.Rows[i]["CLI_ID"],
                        CLI_NOME = tabela.Rows[i]["CLI_NOME"].ToString(),
                        CLI_DATANASCIMENTO = Convert.ToString(tabela.Rows[i]["CLI_DATANASCIMENTO"]),
                        CLI_ATIVO = Convert.ToInt32(tabela.Rows[i]["CLI_ATIVO"]) == 1 ? "Ativo" : "Inativo"
                    };
                    retorno.Add(usuarioModel);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao retornar Usuarios -- {ex}");
                return null;
            }
        }

        public string Insert(string nome, string data, int ativo)
        {
            try
            {
                if (nome == "" || data == "")
                    return "Os campos são obrigatorios";

                string query = $@"
                                INSERT INTO CLIENTETDSA (
                                    CLI_NOME, 
                                    CLI_DATANASCIMENTO,
                                    CLI_ATIVO) 
                                VALUES (
                                    '{nome}',
                                    '{data}',
                                    {ativo})";

                Connection.Sql(query);
                return "Cadastro realizado com sucesso";
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao cadastrar Usuario -- {ex}");
                return "Erro ao cadastrar cliente";
            }
        }

        public string Update(int id, string nome, string data, int ativo)
        {
            try
            {
                string query = $@"
                                SELECT 
	                                CLI_ID,
                                    CLI_NOME,
                                    CLI_DATANASCIMENTO,
                                    CLI_ATIVO
                                FROM CLIENTETDSA
                                WHERE CLI_ID = {id}";

                DataTable tabela = Connection.SqlDataTable(query);
                if (tabela.Rows.Count <= 0)
                    return "Usuario não existe";

                query = $@"
                        UPDATE CLIENTETDSA SET 
	                        CLI_NOME = '{nome}',
	                        CLI_DATANASCIMENTO = '{data}',
                            CLI_ATIVO = {ativo}    
                        WHERE 
	                        CLI_ID = '{id}'";

                Connection.Sql(query);
                return "Cadastro atualizado com sucesso";
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao atualizar Usuario -- {ex}");
                return "Erro ao atualizar cliente";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = $@"
                                SELECT 
	                                CLI_ID,
                                    CLI_NOME,
                                    CLI_DATANASCIMENTO,
                                    CLI_ATIVO
                                FROM CLIENTETDSA
                                WHERE CLI_ID = {id}";

                DataTable tabela = Connection.SqlDataTable(query);
                if (tabela.Rows.Count <= 0)
                    return "Usuario não existe";

                query = $"DELETE FROM CLIENTETDSA WHERE CLI_ID = {id}";

                Connection.Sql(query);
                return "Usuario deletado com sucesso";
            }
            catch (Exception ex)
            {
                RegistraLog.Log($"Erro ao deletar Usuario -- {ex}");
                return "Erro ao deletar cliente";
            }
        }
    }
}