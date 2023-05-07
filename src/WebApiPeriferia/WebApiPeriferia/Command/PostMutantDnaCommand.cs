using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApiPeriferia.Validations;

namespace WebApiPeriferia.Command
{
    public class PostMutantDnaCommand: IRequest<bool>
    {
        [DnaValidation(ErrorMessage ="Ha enviado un formato de caracteres no admitidos")]
        public string[] dna { get; set; }   
    }
}
