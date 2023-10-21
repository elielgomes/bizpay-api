using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bizpay_api.Services
{
    public static class Format { 
        public static string FormatCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new DataValidationException("CPF não pode estar em branco");

            cpf = cpf.Replace(".", "").Replace("-", ""); // Remove pontos e traços
            if (cpf.Length != 11)
                throw new DataValidationException("CPF deve conter 11 dígitos");

            /*if (!IsValidCPF(cpf))
                throw new DataValidationException("CPF inválido");*/

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9)}";
        }

        public static string FormatRG(string rg)
        {
            if (string.IsNullOrEmpty(rg))
                throw new DataValidationException("RG não pode estar em branco");

            rg = rg.Replace(".", "").Replace("-", ""); // Remove pontos e traços
            /*if (rg.Length != 9)
                throw new DataValidationException("RG deve conter 9 dígitos");*/

            // Você pode adicionar mais validações específicas do RG aqui, se necessário

            return $"{rg.Substring(0, 2)}.{rg.Substring(2, 3)}.{rg.Substring(5, 3)}";
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new DataValidationException("Número de telefone não pode estar em branco");

            phoneNumber = Regex.Replace(phoneNumber, @"[^\d]", ""); // Remove caracteres não numéricos
            if (phoneNumber.Length != 10 && phoneNumber.Length != 11)
                throw new DataValidationException("Número de telefone deve conter 10 ou 11 dígitos");

            // Adicione outras validações específicas do telefone, se necessário

            if (phoneNumber.Length == 10)
                return $"({phoneNumber.Substring(0, 2)}) {phoneNumber.Substring(2, 4)}-{phoneNumber.Substring(6)}";
            else
                return $"({phoneNumber.Substring(0, 2)}) {phoneNumber.Substring(2, 5)}-{phoneNumber.Substring(7)}";
        }

        public static string FormatCellNumber(string cellPhoneNumber)
        {
            if (string.IsNullOrEmpty(cellPhoneNumber))
                throw new DataValidationException("Número de celular não pode estar em branco");

            cellPhoneNumber = Regex.Replace(cellPhoneNumber, @"[^\d]", ""); // Remove caracteres não numéricos
            if (cellPhoneNumber.Length != 11)
                throw new DataValidationException("Número de celular deve conter 11 dígitos");

            // Adicione outras validações específicas do celular, se necessário

            return $"({cellPhoneNumber.Substring(0, 2)}) {cellPhoneNumber.Substring(2, 5)}-{cellPhoneNumber.Substring(7)}";
        }
    }
}
