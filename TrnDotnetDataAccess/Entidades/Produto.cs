﻿using System;

namespace TrnDotnetDataAccess.Entidades
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        public Produto(string nome, decimal precoUnitario, int quantidadeEstoque)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            PrecoUnitario = precoUnitario;
            QuantidadeEstoque = quantidadeEstoque;
        }

        public Produto(Guid id)
        {
            Id = id;
        }

        public void Atualizar(string nome, decimal precoUnitario, int quantidadeEstoque)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            QuantidadeEstoque = quantidadeEstoque;
        }
    }
}
