using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public enum StanjeRezervacije {[EnumMember] POTREBNO_PLATITI, [EnumMember] PLACENA }

    [DataContract]
    public class Rezervacija
    {
        int idR;
        int idKoncerta;
        DateTime vremeRezervacije;
        int kolicinaKarata;
        StanjeRezervacije stanjeRezervacije;

        public Rezervacija(int idR, int idKoncerta, DateTime vremeRezervacije, int kolicinaKarata, StanjeRezervacije stanjeRezervacije)
        {
            this.idR = idR;
            this.idKoncerta = idKoncerta;
            this.vremeRezervacije = vremeRezervacije;
            this.kolicinaKarata = kolicinaKarata;
            this.stanjeRezervacije = stanjeRezervacije;
        }

        [DataMember]
        public int IdR { get => idR; set => idR = value; }

        [DataMember]
        public int IdKoncerta { get => idKoncerta; set => idKoncerta = value; }

        [DataMember]
        public DateTime VremeRezervacije { get => vremeRezervacije; set => vremeRezervacije = value; }

        [DataMember]
        public int KolicinaKarata { get => kolicinaKarata; set => kolicinaKarata = value; }

        [DataMember]
        public StanjeRezervacije StanjeRezervacije { get => stanjeRezervacije; set => stanjeRezervacije = value; }

        public override string ToString()
        {
            return String.Format("Id rezervacije : {0}, id koncerta : {1}, vreme rezervacije : {2}, kolicina karata : {3} , stanje rezervacije : {4}", IdR, IdKoncerta, VremeRezervacije, KolicinaKarata, StanjeRezervacije);
        }
    }
}
