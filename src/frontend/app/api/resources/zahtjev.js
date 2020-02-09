export default resource => {
    return resource('zahtjev', null, {
        vratiSveZahtjeve: { method: 'GET', url: 'zahtjevi' },
        vratiSveZahtjeveProjekta: { method: 'GET', url: 'zahtjevi/projekat/{projekatId}'},
        dodajNoviZahtjev:{method: 'POST', url: 'zahtjevi/projekat/{projekatId}'},
        vratiZahtjev:{method: 'GET', url: 'zahtjevi/{zahtjevId}'},
        azurirajZahtjev: {method: 'PUT', url: 'zahtjevi/{zahtjevId}'},
        dodajKomentar:{method: 'POST', url: 'zahtjevkomentari/zahtjev/{zahtjevId}'},
        vratiKomentareZahtjeva:{method: 'GET', url: 'zahtjevkomentari/zahtjev/{zahtjevId}'},
        azurirajStatusZahtjeva: {method: 'PUT', url: 'zahtjevi/{zahtjevId}/zahtjevStatus'},
        vratiPrilogZahtjeva: {method: 'GET', url:'prilogZahtjeva/{prilogId}'},
        vratiIzmjeneStatusaZahtjeva: {method: 'GET', url:'zahtjevi/{zahtjevId}/zahtjevStatusi'},
        brisanjeZahtjeva: {method: 'PUT', url:'zahtjevi/{zahtjevId}/brisanje'}
        
        

        
        

        /*onemogucen: { method: 'PUT', url: 'korisnici/{korisnickoIme}/onemogucen' },   
        aktiviraj: { method: 'GET', url: 'korisnici/aktiviraj' },
        azurirajLicneDetalje: { method: 'PUT', url: 'korisnici/{korisnickoIme}/licni-detalji' }*/
    });
};