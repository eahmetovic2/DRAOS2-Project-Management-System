export default resource => {
  return resource('korisnici/{korisnickoIme}', null, {
    promijeniLozinku: { method: 'PUT', url: 'korisnici/{korisnickoIme}/lozinka' },
    postaviLozinku: { method: 'POST', url: 'korisnici/{korisnickoIme}/lozinka' },
    onemogucen: { method: 'PUT', url: 'korisnici/{korisnickoIme}/onemogucen' },   
    aktiviraj: { method: 'GET', url: 'korisnici/aktiviraj' },
    azurirajLicneDetalje: { method: 'PUT', url: 'korisnici/{korisnickoIme}/licni-detalji' },
    azurirajProjekteKorisnika: {method:'POST', url: 'korisnici/{korisnickoIme}/projekti'},
    vratiKategorijeKorisnika: {method:'GET', url:'zahtjevkategorija/{korisnickoIme}'},
    vratiSupportKorisnikeZaZahtjevKategoriju: {method:'GET', url:'korisnici/zahtjevKategorija/{zahtjevKategorijaId}'}
  });
};
