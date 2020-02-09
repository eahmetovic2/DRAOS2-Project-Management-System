// import * as types from './mutation-types'
// initial state
const state = {
  naslovSistema: null,
  odabraniJezik: null,
  navigationbarComponent: null,
  drawer: null,
};

// getters
const getters = {
  naslovSistema: (state) => {
    return state.naslovSistema;
  },
  odabraniJezik: (state) => {
    return state.odabraniJezik;
  },
  navigationbarComponent: (state) => {
    return state.navigationbarComponent
  },
  drawer: (state) => {
    return state.drawer
  },
};

// mutations
const mutations = {
  setNaslovSistema (state, naslovSistema) {
    state.naslovSistema = naslovSistema;
  },
  setOdabraniJezik (state, odabraniJezik) {
    state.odabraniJezik = odabraniJezik;
  },
  setNavigationbarComponent(state, component) {
    console.log(state,component,'component')
    state.navigationbarComponent = component
  },
  setDrawer(state, drawer) {
    state.drawer = drawer
  },

};

export default {
  namespaced: true,
  state,
  getters,
  mutations
};