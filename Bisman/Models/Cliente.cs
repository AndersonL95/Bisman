using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bisman.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O Tamanho do {0} deve ser entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Preencha o campo {0}")]
        [EmailAddress(ErrorMessage = "Digite um Email Válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
   

        public Cliente()
        {
        }

        public Cliente(int id, string nome, string endereco, string email)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Email = email;
            Senha = senha;
        }
              
        
    }
}
