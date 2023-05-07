using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApiPeriferia.Validations
{
    public class DnaValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] atributte = (string[])value;
            Regex regex = new Regex("^[ACGT]+$", RegexOptions.IgnoreCase);
            bool response = true;
            for (int i = 0; i < atributte.Length; i++)
            {
                if (!regex.IsMatch(atributte[i]) || atributte[i].Length != atributte.FirstOrDefault().Length)
                {
                    response = false;
                    break;

                }
            }
            return response;
        }
    }
}
