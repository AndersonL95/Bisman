using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bisman.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O Tamanho do {0} deve ser entre {2} e {1} caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo {0}")]
        [EmailAddress(ErrorMessage = "Digite um Email Válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Preencha o campo {0}")]
        public string Senha { get; set; }

        public Usuario()
        {
        }

        public Usuario(int id, string nome, string email, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }
              
        
    }
}
