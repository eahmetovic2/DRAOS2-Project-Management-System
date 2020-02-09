export default resource => {
  return resource('dashboard', null, {
    osnovno: { method: 'GET', url: 'dashboard/osnovno' },
    header: { method: 'GET', url: 'dashboard/header' },
    statistika: {method:'GET', url: 'dashboard/statistika'},
    zavrseniZahtjeviPoDanimaProslaSedmica: {method: 'GET', url: 'dashboard/zavrseniZahtjeviPoDanimaProslaSedmica'},
    kreiraniZahtjeviPoDanimaProslaSedmica: {method: 'GET', url: 'dashboard/kreiraniZahtjeviPoDanimaProslaSedmica'},
    brojZahtjevaPoProjektu: {method:'GET', url:'dashboard/brojZahtjevaPoProjektu'},
    zahtjeviNajduzeUPotrebnoUraditi : {method: 'GET', url: 'dashboard/zahtjeviNajduzeUPotrebnoUraditi'},
    zahtjeviZadnjiDodatiKojeJePotrebnoUraditi : {method: 'GET', url: 'dashboard/zahtjeviZadnjiDodatiKojeJePotrebnoUraditi'},
    sviProjektiKorisnika: {method:'GET', url:'dashboard/sviProjektiKorisnika'},
    zahtjeviPoDanimaAktivnaGodina: {method: 'GET', url: 'dashboard/zahtjeviPoDanimaAktivnaGodina'},
    zahtjeviPoMjesecima:{method: 'GET', url: 'dashboard/zahtjeviPoMjesecima'},
    zahtjeviZadnjiDodatiNisuRijeseni : {method: 'GET', url: 'dashboard/zahtjeviZadnjiDodatiNisuRijeseni'},
    zahtjeviZadnjiDodatiRijeseni: {method: 'GET', url: 'dashboard/zahtjeviZadnjiDodatiRijeseni'},
    zahtjeviPoSedmicama: {method: 'GET', url: 'dashboard/zahtjeviPoSedmicama'}

    
    
    
  });
};
