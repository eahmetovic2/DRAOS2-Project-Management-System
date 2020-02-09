export default resource => {
  return resource('korisnici/{korisnickoIme}/tokeni{/tokenId}', null, {
    temp: {
      method: 'POST',
      url: 'korisnici/{korisnickoIme}/tokeni/temp'
    },
    provjeriLozinku: {
      method: 'POST',
      url: 'korisnici/{korisnickoIme}/tokeni/provjerilozinku'
    }
  });
};