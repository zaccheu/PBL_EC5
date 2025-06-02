using System;
using System.Collections.Generic;

namespace PBL_EC5.Models
{
    public class DadosEstufaRequest
    {
        public int IdEstufa { get; set; }
        public List<DadoSensor> Dados { get; set; }
    }

    public class DadoSensor
    {
        public string Id { get; set; }
        public string RecvTime { get; set; }
        public string AttrName { get; set; }
        public string AttrType { get; set; }
        public double AttrValue { get; set; }
    }

    public class FiltroHistoricoRequest
    {
        public int IdEstufa { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
