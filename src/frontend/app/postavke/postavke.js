import Identity from 'auth/identity';

export default {
  postavke: null,

  getPostavke() {
    return this.postavke;
  },

  setPostavke(postavke) {
    this.postavke = postavke;
  },

  naslovSistema() {
    if (this.postavke)
      return this.postavke.naslovSistema;
    return 'Project Management System';
  },

  trajanjeSesije() {
    if (this.postavke)
      return this.postavke.trajanjeSesije;
    return 5;
  },

  urlKarte() {
    if (this.postavke)
      return this.postavke.urlKarte;
    return 'http://{s}.tile.osm.org/{z}/{x}/{y}.png';
  },

  autorskaPravaKarte() {
    if (this.postavke)
      return this.postavke.autorskaPravaKarte;
    return '';
  }

};
