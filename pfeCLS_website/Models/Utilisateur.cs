//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pfeCLS_website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Utilisateur
    {
        public int id_Uti { get; set; }

        [Required(ErrorMessage = "Veuillez saisir votre nom.")]
        public string Nom_Uti { get; set; }

        [Required(ErrorMessage = "Veuillez saisir votre mot de passe .")]
        public string motPasse { get; set; }
        public string Role { get; set; }
    }
}