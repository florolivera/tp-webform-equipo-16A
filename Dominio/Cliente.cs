using System;

namespace dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Documento { get; set; }   // DNI
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int CP { get; set; }
        public override string ToString() => $"{Apellido}, {Nombre} - {Documento}";
    }
}
