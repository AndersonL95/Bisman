using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bisman.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O Tamanho do {0} deve ser entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [Range(1.0, 5000.0, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]        
        public double Valor { get; set; }

        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Produto()
        {
        }

        public Produto(int id, string nome, DateTime data, double valor, Usuario usuario)
        {
            Id = id;
            Nome = nome;
            Data = data;
            Valor = valor;
            Usuario = usuario;
        }


        public void AddProdutos(Produto produto)
        {
            Produtos.Add(produto);
        }

        public void RemoveProdutos(Produto produto)
        {
            Produtos.Remove(produto);
        }

        public double TotalProdutos(DateTime initial, DateTime final)
        {
            return Produtos.Where(x => x.Data >= initial && x.Data <= final).Sum(x => x.Valor);
        }
    }

}
