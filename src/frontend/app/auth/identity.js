import Api from 'api';

import TokenStorage from './token-storage';
import DozvoljeneAkcijeStorage from './dozvoljene-akcije-storage';

export default {
  token: null,
  dozvoljeneAkcije: [],

  getToken() {
    return this.token;
  },

  imaPravo(akcija) {
    if (this.dozvoljeneAkcije) {
      return this.dozvoljeneAkcije.some(a => a === akcija);
    }
    return false;
  },

  dodatnaInformacijaPoSifri(sifra) {
    var informacije = this.token.vlasnik.dodatneInformacije.items.filter(a => a.tipDodatneInformacije.sifra === sifra);
    if (informacije.length > 0) {
      return informacije[0].vrijednost;
    }
    return null;
  },

  frontendModul() {
    return this.token.frontendModul;
  },

  frontendModulNaslov() {
    if (this.token) {
      return this.token.frontendModulNaslov;
    }    
  },

  setDozvoljeneAkcije(dozvoljeneAkcije) {
    this.dozvoljeneAkcije = dozvoljeneAkcije;
    DozvoljeneAkcijeStorage.setDozvoljeneAkcije(dozvoljeneAkcije);
  },

  setToken(token) {
    this.token = token;
    Api.setToken(token);
    TokenStorage.setToken({
      korisnickoIme: token.vlasnik.korisnickoIme,
      tokenId: token.id
    });
  },

  removeToken() {
    this.token = null;
    Api.removeToken();
    TokenStorage.removeToken();
  },

  getStorage() {
    return TokenStorage;
  },

  getDozvoljeneAkcijeStorage() {
    return DozvoljeneAkcijeStorage;
  },

  id() {
    if (this.token && this.token.id)
      return this.token.id;
    return null;
  },

  korisnickoIme() {
    if (this.token && this.token.vlasnik)
      return this.token.vlasnik.korisnickoIme;
    return null;
  },

  uloga() {
    if (this.token && this.token.vlasnik)
      return this.token.uloga;
    return null;
  },

  trenutnaUlogaId() {
    if (this.token && this.token.vlasnik)
      return this.token.ulogaId;
    return null;
  },

  punoIme() {
    if (this.token && this.token.vlasnik)
      return this.token.vlasnik.punoIme;
    return null;
  },

  isAuthenticated() {
    return this.id() != null;
  }
};
