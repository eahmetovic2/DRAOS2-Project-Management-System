export default resource => {
    return resource('korisnici/ulogatipovidodatneinformacije', {}, {
      sveZaUlogu: { method: 'GET', url: 'korisnici/ulogatipovidodatneinformacije/zaulogu/{ulogaId}' },
      snimiZaUlogu: { method: 'PUT', url: 'korisnici/ulogatipovidodatneinformacije/zaulogu/{ulogaId}' }
    });
  };
  