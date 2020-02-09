export default resource => {
    return resource('projekat', null, {
      vratiSveProjekte: { method: 'GET', url: 'projekat' },
      vratiProjekat: { method: 'GET', url: 'projekat/{projekatId}' },
      azurirajProjekat: { method: 'PUT', url: 'projekat/{projekatId}' },
      vratiZahtjevPrioriteteZaProjekat: {method: 'GET', url:'zahtjevprioritet/projekat/{projekatId}' },
      vratiKonfiguracijuProjekta:{method:'GET', url: 'projekatkonfiguracija/projekat/{projekatId}'},
      vratiTipoveZahtjevaZaProjekat: {method: 'GET', url:'zahtjevtip/projekat/{projekatId}' },
      vratiStatuseZahtjevaZaProjekat: {method: 'GET', url:'zahtjevstatus/projekat/{projekatId}' },
      snimiKonfiguracijuProjekta: {method: 'PUT', url:'projekatkonfiguracija/projekat/{projekatId}' },
      dodajNoviStatusZahtjevaZaProjekat: {method: 'POST', url:'zahtjevstatus/projekat/{projekatId}' },
      dodajNoviPrioritetZahtjevaZaProjekat: {method: 'POST', url:'zahtjevprioritet/projekat/{projekatId}' },
      dodajNoviTipZahtjevaZaProjekat: {method: 'POST', url:'zahtjevtip/projekat/{projekatId}' },
      vratiDijeloveProjekta: {method:'GET',url:'dioprojekta/projekat/{projekatId}'},
      dodajNoviDioProjekta:{method:'POST', url:'dioprojekta/projekat/{projekatId}'},
      azurirajDefaultniZahtjevPrioritetProjekta:{method:'PUT',url:'zahtjevprioritet/projekat/{projekatId}'},
      azurirajDefaultniZahtjevStatusProjekta:{method:'PUT',url:'zahtjevstatus/projekat/{projekatId}'},
      azurirajDefaultniZahtjevTipProjekta:{method:'PUT',url:'zahtjevtip/projekat/{projekatId}'},
      vratiZahtjevKategorijeZaDioProjekta: {method:'GET', url:'zahtjevkategorija/dioprojekta/{dioProjektaId}'},
      dodajNovuZahtjevKategoriju:{method:'POST', url:'zahtjevkategorija/dioprojekta/{dioProjektaId}'},
      vratiKorisnikeProjekta:{method: 'GET', url:'projekat/{projekatId}/korisnici'},
      vratiProjekteZaKorisnikUlogu:{method: 'GET', url:'projekat/korisnik/{korisnickoIme}/uloga/{ulogaId}'},
      azurirajPoredakStatusa:{method:'PUT',url:'zahtjevstatus/projekat/{projekatId}/poredak'}
      
      

      
      
      

      
      
      
      /*onemogucen: { method: 'PUT', url: 'korisnici/{korisnickoIme}/onemogucen' },   
      aktiviraj: { method: 'GET', url: 'korisnici/aktiviraj' },
      azurirajLicneDetalje: { method: 'PUT', url: 'korisnici/{korisnickoIme}/licni-detalji' }*/
    });
  };