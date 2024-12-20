﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveAqui.Entities
{
    public class Reserva
    {
        public Reserva() {
            ReservaServicoAdicionals = new List<ReservaServicoAdicional>();
            CheckInRealizado = false;
            CheckOutRealizado = false;
            Cancelada = false;  
        }

      
        public Guid Id { get; set; }

        [Required]
        public Guid QuartoId { get; set; }

        [Required]
        public Guid HospedeId { get; set; }

        [Required]
        public int QuantidadePessoas { get; set; }

        [Required]
        public DateTime DataEntrada { get; set; }

        [Required]
        public DateTime DataSaida { get; set; }

        public bool CheckInRealizado { get; set; }

        public bool CheckOutRealizado { get; set; }

        public bool Cancelada { get; set; }

        public List<ReservaServicoAdicional> ReservaServicoAdicionals { get; set; }

        public void CheckIn()
        {
            CheckInRealizado = true;
        }

        public void CheckOut()
        {
            CheckOutRealizado = true;
        }

        public void Cancelar()
        {
            Cancelada = true;
        }
    }
}