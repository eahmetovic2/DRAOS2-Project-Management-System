import config from 'config';
import Store from 'store';

export default {
  hasToken () {
    var token = this.getToken();
    if (token)
      return true;
    return false;
  },

  getToken () {
    return Store.get(config.AUTH_TOKEN_NAME);
  },

  setToken (token) {
    Store.set(config.AUTH_TOKEN_NAME, token);
  },

  removeToken () {
    Store.remove(config.AUTH_TOKEN_NAME);
  }
};
