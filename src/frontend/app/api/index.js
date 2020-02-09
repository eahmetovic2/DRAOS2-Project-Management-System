import Vue from 'vue';
import config from 'config';

export default {
  init () {
    return new Promise(resolve => {
      Vue.http.options.root = config.SERVICE_ROOT;
      resolve();
    });
  },

  onProgress (start, finish) {
    Vue.http.interceptors.push((request, next) => {
      start();

      next((response) => {
        setTimeout(() => {
          finish(response.ok);
        }, 50);

        return response;
      });
    });
  },

  setToken (token) {
    Vue.http.headers.common['X-AUTH-TOKEN'] = null;
    if (token && token.id)
      Vue.http.headers.common['X-AUTH-TOKEN'] = token.id;
  },

  removeToken () {
    this.setToken(null);
  }
};
