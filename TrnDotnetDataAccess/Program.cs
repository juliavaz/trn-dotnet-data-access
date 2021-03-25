using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using TrnDotnetDataAccess.Entidades;

namespace TrnDotnetDataAccess
{
    class Program
    {
        private static SqlConnection sqlConnection;
        static void Main(string[] args)
        {
            //----- Cliente -----\\
            //GravarNovoCliente();
            //ListarClientes();
            //ExcluirCliente

            //----- Produto -----\\
            //GravarNovoProduto();
            //ListarProdutos();
            ExcluirProduto();

            Console.ReadKey();
        }
        private static void IniciarConexao()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbLoja;Integrated Security=True;Connect Timeout=20;";

            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString;

        }
        private static void GravarNovoCliente()
        {
            IniciarConexao();
            sqlConnection.Open();

            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "insert into Cliente values(@id,@nome,@email,@senha)";

            //var cliente = new Cliente("Maria da Silva", "marias274@gmail.com", "123456");
            var cliente = new Cliente("Júlia Vaz", "julia@gmail.com", "123456");

            sqlCommand.Parameters.Add(new SqlParameter("@id", cliente.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@nome", cliente.Nome));
            sqlCommand.Parameters.Add(new SqlParameter("@email", cliente.Email));
            sqlCommand.Parameters.Add(new SqlParameter("@senha", cliente.Senha));

            var qtdRows = sqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Cliente cadastrado com sucesso");
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
        }
        private static void ListarClientes()
        {
            IniciarConexao();
            sqlConnection.Open();

            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select Id,Nome,Email from Cliente";

            var sqlDataReader = sqlCommand.ExecuteReader();

            List<Cliente> listaClientes = new List<Cliente>();

            while (sqlDataReader.Read())
            {
                Guid id = Guid.Parse(sqlDataReader[0].ToString());
                var cliente = new Cliente(id);
                cliente.Atualizar(sqlDataReader[1].ToString(), sqlDataReader[2].ToString());
                listaClientes.Add(cliente);
            }

            sqlDataReader.Close();
            sqlConnection.Close();
            sqlConnection.Dispose();

            foreach (var item in listaClientes)
            {
                Console.WriteLine($"ID: {item.Id} \n- Nome: {item.Nome}  - Email: {item.Email}");
            }


        }
        private static void ExcluirCliente()
        {
            IniciarConexao();
            sqlConnection.Open();
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "delete from Cliente where id=@id";

            var clienteId = "c180493e-21a7-44a3-84b8-7f59ae629e4f";
            sqlCommand.Parameters.Add(new SqlParameter("@id", clienteId));

            var qtdRows = sqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Cliente excluído com sucesso");
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        private static void GravarNovoProduto()
        {
            IniciarConexao();
            sqlConnection.Open();

            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "INSERT INTO Produto values(@id, @nome,@precounitario,@quantidadeestoque)";

            var produto = new Produto("Refrigerante", 15, 10);

            sqlCommand.Parameters.Add(new SqlParameter("@id", produto.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@nome", produto.Nome));
            sqlCommand.Parameters.Add(new SqlParameter("@precounitario", produto.PrecoUnitario));
            sqlCommand.Parameters.Add(new SqlParameter("@quantidadeestoque", produto.QuantidadeEstoque));

            var qtdRows = sqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Produto cadastrado com sucesso");
            }
        }
        private static void ListarProdutos()
        {
            IniciarConexao();
            sqlConnection.Open();

            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT Id,Nome,PrecoUnitario,QuantidadeEstoque FROM Produto";

            var sqlDataReader = sqlCommand.ExecuteReader();

            List<Produto> listaProdutos = new List<Produto>();

            while (sqlDataReader.Read())
            {
                Guid id = Guid.Parse(sqlDataReader[0].ToString());
                var produto = new Produto(id);
                produto.Atualizar(sqlDataReader[1].ToString(), decimal.Parse(sqlDataReader[2].ToString()), int.Parse(sqlDataReader[3].ToString()));
                listaProdutos.Add(produto);
            }

            sqlDataReader.Close();
            sqlConnection.Close();
            sqlConnection.Dispose();

            foreach (var item in listaProdutos)
            {
                Console.WriteLine($"ID: {item.Id} \n" +
                    $"- Nome: {item.Nome}  - Preço Unitário: {item.PrecoUnitario.ToString("F2", CultureInfo.InvariantCulture)} - Quantidade em Estoque: {item.QuantidadeEstoque.ToString(CultureInfo.InvariantCulture)}");
            }
        }
        private static void ExcluirProduto()
        {
            IniciarConexao();
            sqlConnection.Open();

            var SqlCommand = new SqlCommand();
            SqlCommand.Connection = sqlConnection;
            SqlCommand.CommandText = "DELETE FROM Produto WHERE id=@id";

            var produtoId = "bc66e8d0-d8be-4d25-b9cc-4eb8024cafba";
            SqlCommand.Parameters.Add(new SqlParameter("@id", produtoId));

            var qtdRows = SqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Produto excluído com sucesso!");
            }

            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}