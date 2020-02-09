import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from 'vuex-persistedstate';
import home from '../modules/home/store';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    home
  },
  plugins: [createPersistedState({
    key: 'tmsVuex'
  })]
});