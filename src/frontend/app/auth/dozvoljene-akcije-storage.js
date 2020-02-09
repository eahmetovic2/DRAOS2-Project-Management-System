import config from 'config';
import Store from 'store';

export default {
  getDozvoljeneAkcije() {
    return Store.get(config.DOZVOLJENE_AKCIJE_NAME);
  },

  setDozvoljeneAkcije(dozvoljeneAkcije) {
    Store.set(config.DOZVOLJENE_AKCIJE_NAME, dozvoljeneAkcije);
  },

  removeDozvoljeneAkcije () {
    Store.remove(config.DOZVOLJENE_AKCIJE_NAME);
  }
};
