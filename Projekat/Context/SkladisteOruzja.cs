//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projekat.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SkladisteOruzja
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        public int BrojPriznanice { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public System.DateTime VremePriznanice { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public int IDOruzja { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string naziv { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string tip { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public int godinaProizvodnje { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string JMBG { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string statusSkladistenja { get; set; }
    }
}
