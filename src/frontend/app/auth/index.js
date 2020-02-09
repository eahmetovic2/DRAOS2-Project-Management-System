import { TokenResource } from 'api/resources';

import Identity from './identity';

export default {
  init () {
    return new Promise(resolve => {
      var tokenStorage = Identity.getStorage();
      if (!tokenStorage.hasToken()) {
        resolve();
        return;
      }

      var storedToken = tokenStorage.getToken();
      var promise = TokenResource().get(storedToken);

      // Procitaj dozvoljene akcije iz localstorage
      var dozvoljeneAkcijeStorate = Identity.getDozvoljeneAkcijeStorage();
      var storedDozvoljeneAkcije = dozvoljeneAkcijeStorate.getDozvoljeneAkcije();
      Identity.setDozvoljeneAkcije(storedDozvoljeneAkcije);

      promise.then(response => {
        Identity.setToken(response.body);
        resolve();
      }, () => {
        Identity.removeToken();
        resolve();
      });
    });
  },

  signIn (korisnickoIme, lozinka, ulogaId) {
    console.log("SIGN IN")
    var promise = TokenResource().save({ korisnickoIme: korisnickoIme }, {
      lozinka: lozinka,
      ulogaId: ulogaId
    });

    return promise.then(response => {
      console.log("RES", response)
      var listaDozvoljenih = response.body.vlasnik.dozvoljeneAkcije.items.map(a => a.sifra);
      console.log("doz", listaDozvoljenih)
      Identity.setDozvoljeneAkcije(listaDozvoljenih);

      console.log("doz2", listaDozvoljenih)
      Identity.setToken(response.body);
      return response.body;
    }, response => {
      Identity.removeToken();
      return Promise.reject(response);
    });
  },

  signOut () {
    var token = Identity.getToken();
    if (!token)
      return;

    TokenResource().remove({
      korisnickoIme: token.vlasnik.korisnickoIme,
      tokenId: token.id
    });

    Identity.removeToken();
  },

  isAuthenticated () {
    return Identity.isAuthenticated();
  }
};
