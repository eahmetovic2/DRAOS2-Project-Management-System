export default resource => {
    return resource('korisnici/uloga/{ulogaId}', {}, {
      grupePrava: { method: 'GET', url: 'korisnici/uloga/{ulogaId}/grupeprava' },
      dozvoljeneAkcije: { method: 'GET', url: 'korisnici/uloga/{ulogaId}/dozvoljeneakcije' },
      snimiDozvoljeneAkcije: { method: 'PUT', url: 'korisnici/uloga/{ulogaId}/dozvoljeneakcije' }
    });
  };
  