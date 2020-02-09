import Vue from 'vue';
import BS from './bs';
import HR from './hr';
import SR from './sr';
// import EN from './en';
import { MLInstaller, MLCreate, MLanguage } from 'vue-multilanguage';
 
Vue.use(MLInstaller);
 
export default new MLCreate({
  initial:0,
  // save: process.env.NODE_ENV === 'production',
  languages: [
    new MLanguage(0).create(BS), 
    new MLanguage(1).create(HR), 
    new MLanguage(2).create(SR)
    // ,
    // new MLanguage(3).create(EN)
  ]
});