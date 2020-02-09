export default resource => {
    return resource('notifikacije', {}, {
     /* otvori: {
        method: 'PUT',
        url: 'notifikacije/otvori'
      },*/
      vratiNotifikacije: {method: 'GET', url: 'notifikacije'},
      otvori: {method: 'PUT',url:'notifikacije/otvori'},
      otvoriKorisnikoveNotifikacijeZaZahtjev: {method:'PUT', url:'notifikacije/zahtjev/{zahtjevId}/otvori'}
    });
  };

  